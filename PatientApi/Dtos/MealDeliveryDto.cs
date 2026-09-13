using PatientApi.Entities;

namespace PatientApi.Dtos;

/// <summary>
/// DTO returned to clients when reading meal delivery data.
/// </summary>
public class MealDeliveryDto
{
    public int MealDeliveryId { get; set; }
    public int PatientId { get; set; }
    public int FoodId { get; set; }
    public string FoodName { get; set; } = string.Empty;
    public double PortionGiven { get; set; }

    /// <summary>
    /// PortionGiven multiplied by the Food item's base Calories -
    /// computed on read, not stored.
    /// </summary>
    public double TotalCalories { get; set; }
    public MealType MealType { get; set; }
    public DateTime DateDelivered { get; set; }
}
