using System.ComponentModel.DataAnnotations;

namespace PatientApi.Dtos
{
    /// <summary>
    /// DTO used when creating a new patient. No PatientId - the database generates it.
    /// </summary>
    public class CreatePatientDto
    {
        [Required]
        [MaxLength(100)]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [MaxLength(100)]
        public string LastName { get; set; } = string.Empty;

        [Required]
        public DateTime DOB { get; set; }

        [Required]
        [MaxLength(50)]
        public int FacilityId { get; set; } = 0;
    }
}
