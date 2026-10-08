namespace PatientApi.Entities;

public class MealConsumption
{
    private MealConsumption() { } // EF Core

    internal MealConsumption(MealDelivery deliveredMeal, int caloriesEaten, DateTime dateEaten)
    {
        DeliveredMeal = deliveredMeal;
        DeliveredMealId = deliveredMeal.MealDeliveryId;
        CaloriesEaten = caloriesEaten;
        DateEaten = dateEaten;
    }

    public int MealConsumptionId { get; private set; }
    public int DeliveredMealId { get; private set; }
    public MealDelivery DeliveredMeal { get; private set; } = null!;

    public int CaloriesEaten { get; private set; }
    public DateTime DateEaten { get; private set; }

    // The ONLY mutation allowed after creation
    public void Correct(int caloriesEaten, DateTime dateEaten)
    {
        CaloriesEaten = caloriesEaten;
        DateEaten = dateEaten;
    }
}