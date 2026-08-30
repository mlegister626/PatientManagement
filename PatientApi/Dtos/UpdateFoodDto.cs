using System.ComponentModel.DataAnnotations;
using PatientApi.Entities;

public class UpdateFoodDto
{
    [Required]
    public string Name { get; set; } = string.Empty;
    [Required]
    public int Calories { get; set; }
}