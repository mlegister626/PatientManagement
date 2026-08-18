using System.ComponentModel.DataAnnotations;

namespace PatientApi.Dtos;

/// <summary>
/// DTO used when updating an existing facility. No FacilityId - the database generates it.
/// </summary>

public class UpdateFacilityDto
{
    [Required]
    [MaxLength(50)]
    public string Name { get; set; } = string.Empty;
}