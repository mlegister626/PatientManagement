using Microsoft.EntityFrameworkCore;
using PatientApi.Data;
using PatientApi.Entities;

namespace PatientApi.Repositories;

public class FacilityRepository : IFacilityRepository
{
    private readonly ApplicationDbContext _context;

    public FacilityRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Facility>> GetAllAsync()
    {
        return await _context.Facilities.ToListAsync();
    }
    public async Task<Facility?> GetByIdAsync(int facilityId)
    {
        return await _context.Facilities.FindAsync(facilityId);
    }
    public async Task<Facility> AddAsync(Facility facility)
    {
        _context.Facilities.Add(facility);
        await _context.SaveChangesAsync();
        return facility;
    }
    public async Task<bool> UpdateAsync(Facility facility)
    {
        _context.Facilities.Update(facility);
        await _context.SaveChangesAsync();
        return true;
    }
    public async Task<bool> DeleteAsync(int facilityId)
    {
        var facility = await _context.Facilities.FindAsync(facilityId);
        if (facility == null)
            return false;

        _context.Facilities.Remove(facility);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> ExistsAsync(int facilityId)
    {
        return await _context.Facilities.AnyAsync(f => f.FacilityId == facilityId);
    }
}