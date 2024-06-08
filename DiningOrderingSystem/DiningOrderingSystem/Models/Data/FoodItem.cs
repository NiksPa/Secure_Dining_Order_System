using System.ComponentModel.DataAnnotations;

namespace DiningOrderingSystem.Models.Data
{
    public class FoodItem
    {
        [StringLength(100)]
        [Key]
        [Required]
        public string Name { get; set; }

        [Required]
        public int Calorie { get; set; }


        [StringLength(1000)]
        public string Contents { get; set; }
    }
}
