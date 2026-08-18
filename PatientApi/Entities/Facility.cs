using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PatientApi.Entities;

[Table("Facilities")]
public class Facility
{
    [Key]
    [MaxLength(10)]
    [Column("FacilityId")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int FacilityId { get; set; }

    [Required]
    [MaxLength(150)]
    [Column("Name")]
    public string Name { get; set; } = string.Empty;

    // Navigation property for related patients
    public ICollection<Patient> Patients { get; set; } = new List<Patient>();
}