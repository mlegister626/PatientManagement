namespace PatientApi.Dtos;

public record MealConsumptionDto(
    int MealConsumptionId,
    int DeliveredMealId,
    int CaloriesEaten,
    DateTime DateEaten);