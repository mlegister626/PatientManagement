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

    /// <summary>
    /// Portion multiplier applied to the Food item's base Calories
    /// (e.g. 1.0 = full serving, 0.5 = half serving).
    /// </summary>
    [Required]
    [Column("PortionGiven")]
    public double PortionGiven { get; set; }

    [Required]
    [Column("MealType")]
    public MealType MealType { get; set; }

    [Required]
    [Column("DateDelivered", TypeName = "date")]
    public DateTime DateDelivered { get; set; }
}
