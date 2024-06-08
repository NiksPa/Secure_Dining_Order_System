using System.ComponentModel.DataAnnotations;

namespace DiningOrderingSystem.Models
{
    public class UpdateFoodItemViewModel
    {
        [StringLength(100)]
        [Key]
        [Required]
        public string Name { get; set; }

        [Required(ErrorMessage ="Please enter calories")]
        public int Calorie { get; set; }


        [StringLength(1000,ErrorMessage ="Content too long")]
        [Required(ErrorMessage = "Please enter contents")]
        public string Contents { get; set; }
    }
}
