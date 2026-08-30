using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PatientApi.Entities;

[Table("Food")]
public class Food
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Column("FoodId")]
    public int FoodId { get; set; }
    [Required]
    [MaxLength(100)]
    [Column("Name")]
    public string Name { get; set; } = string.Empty;
    [Required]
    [Column("Calories")]
    public int Calories { get; set; }
}