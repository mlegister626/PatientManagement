// ============================================================================
// PatientRepository.cs
//
// JAVA COMPARISON: This is the implementing class, equivalent to:
//
//     @Repository
//     public class PatientRepositoryImpl implements PatientRepository {
//         private final EntityManager entityManager; // or a JPA repository
//
//         public PatientRepositoryImpl(EntityManager entityManager) {
//             this.entityManager = entityManager;
//         }
//         // ... method bodies go here
//     }
//
// This class is where the ACTUAL database work happens, using Entity
// Framework Core (EF Core) - .NET's equivalent of JPA/Hibernate. EF Core's
// DbContext is directly analogous to JPA's EntityManager: it tracks
// entities, builds SQL, and talks to the database for you.
// ============================================================================

using Microsoft.EntityFrameworkCore;
using PatientApi.Data;
using PatientApi.Entities;

namespace PatientApi.Repositories
{
    // "public class PatientRepository : IPatientRepository"
    //                                  ^
    // In C#, a single colon is used for BOTH "extends" and "implements".
    // Here it means "implements IPatientRepository" (Java would write
    // "implements PatientRepository"). If this class also needed to
    // extend a base class, that would come first, e.g.:
    //     class PatientRepository : SomeBaseClass, IPatientRepository
    public class PatientRepository : IPatientRepository
    {
        // ---------------------------------------------------------------
        // DEPENDENCY INJECTION - same pattern as Java/Spring.
        // ---------------------------------------------------------------
        // "private readonly" ≈ Java's "private final".
        // "readonly" means this field can only be assigned once - in the
        // constructor - exactly like "final" in Java.
        private readonly ApplicationDbContext _context;

        // CONSTRUCTOR INJECTION - identical concept to Spring's
        // @Autowired / constructor injection in Java:
        //
        //     @Autowired
        //     public PatientRepositoryImpl(EntityManager entityManager) {
        //         this.entityManager = entityManager;
        //     }
        //
        // In ASP.NET Core, there's no annotation needed - the built-in DI
        // container automatically sees this constructor and supplies an
        // ApplicationDbContext instance when it builds a PatientRepository.
        // That wiring is registered in Program.cs
        // (builder.Services.AddScoped<IPatientRepository, PatientRepository>();)
        // which is roughly equivalent to a Spring @Bean / @Configuration
        // registration, or component scanning with @Component.
        //
        // Naming note: the leading underscore "_context" is a C# convention
        // for private fields, similar to how some Java shops use "this.x"
        // everywhere to disambiguate - it's just a style habit, not required.
        public PatientRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        // =================================================================
        // GetAllAsync() - ≈ Java: findAll()
        // =================================================================
        // "public async Task<IEnumerable<Patient>> GetAllAsync()"
        //   - "async" marks this method as asynchronous, meaning it can use
        //     "await" inside it. Loosely comparable to a Java method
        //     returning CompletableFuture, but the "async/await" keywords
        //     make the code READ like normal synchronous code even though
        //     it isn't blocking the thread underneath.
        public async Task<IEnumerable<Patient>> GetAllAsync()
        {
            // _context.Patients is like a JPA Repository's findAll() query
            // source, or writing entityManager.createQuery("FROM Patient").
            //
            // .AsNoTracking() tells EF Core "don't bother tracking changes
            // to these objects" - this is a read-only fetch. The JPA/
            // Hibernate equivalent is a query hint like
            // session.setDefaultReadOnly(true) or using a read-only
            // transaction - it's a performance optimization for data you
            // won't be saving back.
            //
            // .ToListAsync() executes the query and materializes results
            // into a List, similar to calling query.getResultList() in
            // JPA, but asynchronously.
            //
            // "await" pauses this method (without blocking the thread)
            // until the database call finishes - similar in spirit to
            // "future.get()" in Java, but non-blocking, more like
            // chaining .thenApply() on a CompletableFuture, except the
            // compiler lets you write it top-to-bottom like normal code.
            return await _context.Patients
                .AsNoTracking()
                .ToListAsync();
        }

        // =================================================================
        // GetByIdAsync(int patientId) - ≈ Java: findById(int id)
        // =================================================================
        public async Task<Patient?> GetByIdAsync(int patientId)
        {
            // FirstOrDefaultAsync ≈ JPA's findById(id).orElse(null), or
            // Stream<Patient>.filter(...).findFirst().orElse(null) in
            // plain Java. It returns the first match, or null (default
            // value for a reference type) if nothing matches - hence the
            // "Patient?" nullable return type in the interface.
            //
            // "p => p.PatientId == patientId" is a LAMBDA EXPRESSION,
            // exactly like Java's lambda syntax:
            //     p -> p.getPatientId() == patientId
            // C# lambdas use "=>" ("goes to") instead of Java's "->".
            //
            // This whole chain is LINQ (Language Integrated Query) -
            // C#'s built-in query language, comparable to Java Streams:
            //   Java:  patients.stream().filter(p -> p.getId() == id).findFirst()
            //   C#:    _context.Patients.FirstOrDefaultAsync(p => p.PatientId == patientId)
            // The difference is LINQ here is translated into actual SQL
            // (a WHERE clause) rather than filtering in-memory - similar
            // to how a JPA Criteria/Specification query gets translated
            // into SQL rather than iterating a Java collection.
            return await _context.Patients
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.PatientId == patientId);
        }

        // =================================================================
        // AddAsync(Patient patient) - ≈ Java: save(Patient patient)
        // =================================================================
        public async Task<Patient> AddAsync(Patient patient)
        {
            // .Add(...) stages the new entity for insertion - like calling
            // entityManager.persist(patient) in JPA. Nothing hits the
            // database yet at this point.
            _context.Patients.Add(patient);

            // SaveChangesAsync() is the moment SQL is actually generated
            // and executed - this is EF Core's equivalent of JPA's
            // entityManager.flush() / the commit that happens at the end
            // of a @Transactional method. It's also when the database
            // fills in the auto-generated PatientId, and EF Core copies
            // that generated value back onto your "patient" object.
            await _context.SaveChangesAsync();

            // Return the same object, now with PatientId populated -
            // exactly like JPA's save() returning the managed entity
            // with its generated ID.
            return patient;
        }

        // =================================================================
        // UpdateAsync(Patient patient) - ≈ Java: update(Patient patient)
        // =================================================================
        public async Task<bool> UpdateAsync(Patient patient)
        {
            // First, look up the EXISTING tracked row by id - similar to
            // entityManager.find(Patient.class, patient.getId()) in JPA,
            // which gives you back a "managed" entity that Hibernate is
            // watching for changes (this is NOT using AsNoTracking(),
            // because we need EF Core to track and later save changes).
            var existing = await _context.Patients
                .FirstOrDefaultAsync(p => p.PatientId == patient.PatientId);

            // "is null" ≈ Java's "== null". Returning early here is the
            // same idea as a JPA layer returning false / throwing
            // EntityNotFoundException when find() comes back empty.
            if (existing is null)
            {
                return false;
            }

            // Copy the incoming field values onto the tracked entity.
            // Because "existing" is a tracked entity (not a fresh, no
            // tracking read), EF Core is watching these property changes -
            // this is conceptually identical to Hibernate's "dirty
            // checking": you mutate a managed entity's fields, and the
            // ORM figures out what changed and builds the right UPDATE
            // statement for you - you never write SQL by hand.
            existing.FirstName = patient.FirstName;
            existing.LastName = patient.LastName;
            existing.DOB = patient.DOB;
            existing.Facility = patient.Facility;

            // Same as in AddAsync - this is where the actual UPDATE SQL
            // gets sent, analogous to the transaction commit / flush in
            // a JPA @Transactional service method.
            await _context.SaveChangesAsync();
            return true;
        }

        // =================================================================
        // DeleteAsync(int patientId) - ≈ Java: deleteById(int id)
        // =================================================================
        public async Task<bool> DeleteAsync(int patientId)
        {
            // Same "find first, check for null" pattern as above - like
            // entityManager.find(Patient.class, id) before calling remove().
            var existing = await _context.Patients
                .FirstOrDefaultAsync(p => p.PatientId == patientId);

            if (existing is null)
            {
                return false;
            }

            // .Remove(...) stages the entity for deletion - like
            // entityManager.remove(existing) in JPA. Again, no SQL runs
            // until SaveChangesAsync().
            _context.Patients.Remove(existing);
            await _context.SaveChangesAsync();
            return true;
        }

        // =================================================================
        // ExistsAsync(int patientId) - ≈ Java: existsById(int id)
        // =================================================================
        public async Task<bool> ExistsAsync(int patientId)
        {
            // .AnyAsync(...) ≈ Java Streams' .anyMatch(...), but again
            // translated into an efficient SQL "SELECT EXISTS(...)"
            // rather than pulling every row into memory first - the same
            // efficiency win that Spring Data's existsById() gives you
            // over "findById(id).isPresent()".
            return await _context.Patients
                .AnyAsync(p => p.PatientId == patientId);
        }
    }
}
