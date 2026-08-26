using System.ComponentModel.DataAnnotations;

namespace PatientApi.Dtos
{
    /// <summary>
    /// DTO used when updating an existing patient.
    /// </summary>
    public class UpdatePatientDto
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
        public int FacilityId { get; set; } = 0;
    }
}
