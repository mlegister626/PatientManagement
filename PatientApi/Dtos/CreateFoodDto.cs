using System.ComponentModel.DataAnnotations;
using PatientApi.Entities;

public class CreateFoodDto
{
    [Required]
    [MaxLength(100)]
    public string Name { get; set; } = string.Empty;
    [Required]
    public int Calories { get; set; }
}