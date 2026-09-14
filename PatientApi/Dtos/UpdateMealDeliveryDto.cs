using System.ComponentModel.DataAnnotations;
using PatientApi.Entities;

namespace PatientApi.Dtos;

/// <summary>
/// DTO used when updating an existing meal delivery record.
/// </summary>
public class UpdateMealDeliveryDto
{

    [Required]
    public int FoodId { get; set; }

    [Required]
    [Range(0.01, double.MaxValue, ErrorMessage = "PortionGiven must be greater than 0.")]
    public double PortionGiven { get; set; }

    [Required]
    public MealType MealType { get; set; }

    [Required]
    public DateTime DateDelivered { get; set; }
}
