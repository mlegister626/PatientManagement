using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PatientApi.Entities
{
    /// <summary>
    /// Database entity representing a Patient record.
    /// This maps directly to the "Patients" table in MySQL.
    /// </summary>
    [Table("Patients")]
    public class Patient
    {
        [Key]
        [Column("PatientId")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PatientId { get; set; }

        [Required]
        [MaxLength(100)]
        [Column("FirstName")]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        [Column("LastName")]
        public string LastName { get; set; } = string.Empty;

        [Required]
        [Column("DOB", TypeName = "date")]
        public DateTime DOB { get; set; }

        [Required]
        [MaxLength(50)]
        [ForeignKey("FacilityId")]
        public int FacilityId { get; set; } = 0;
        public virtual Facility Facility { get; set; } = null!;
    }
}
