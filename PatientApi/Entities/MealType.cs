namespace PatientApi.Entities;

/// <summary>
/// Classification of a meal delivery. Stored as a string in the database
/// (see ApplicationDbContext) so the raw table data stays readable.
/// </summary>
public enum MealType
{
    Breakfast,
    Lunch,
    Dinner,
    Snack,
    Dessert
}
