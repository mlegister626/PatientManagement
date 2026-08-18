using System.ComponentModel.DataAnnotations;

namespace PatientApi.Dtos

{
    /// <summary>
    /// DTO used when creating a new facility. No FacilityId - the database generates it.
    /// </summary>
    public class CreateFacilityDto
    {
        [Required]
        [MaxLength(150)]

        public string Name { get; set; } = string.Empty;
    }
}