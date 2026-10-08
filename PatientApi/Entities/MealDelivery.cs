using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PatientApi.Entities;

/// <summary>
/// Database entity recording a single meal delivered to a patient.
/// Maps to the "MealDeliveries" table in MySQL.
/// </summary>
[Table("MealDeliveries")]
public class MealDelivery
{
    [Key]
    [Column("MealDeliveryId")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int MealDeliveryId { get; set; }

    [Required]
    [ForeignKey("PatientId")]
    public int PatientId { get; set; }
    public virtual Patient Patient { get; set; } = null!;

    [Required]
    [ForeignKey("FoodId")]
    public int FoodId { get; set; }
    public virtual Food Food { get; set; } = null!;
    [Required]
    [Column("PortionGiven")]
    public double PortionGiven { get; set; }

    [Required]
    [Column("MealType")]
    public MealType MealType { get; set; }

    [Required]
    [Column("DateDelivered", TypeName = "date")]
    public DateTime DateDelivered { get; set; }

    public MealConsumption? MealConsumption { get; private set; }
    public bool PatientHasEaten => MealConsumption is not null;

    public MealConsumption MarkEaten(int calories, DateTime dateEaten)
    {
        if (MealConsumption is not null)
        {
            throw new InvalidOperationException("Meal has already been marked as eaten.");
        }

        MealConsumption = new MealConsumption(this, calories, dateEaten);
        return MealConsumption;
    }
}
