using System.ComponentModel.DataAnnotations;

namespace DiningOrderingSystem.Models
{
    public class AddFoodItemViewModel
    {
        [StringLength(100, ErrorMessage = "Name too long")]
        [Key]
        [Required(ErrorMessage ="Please enter name")]
        public string Name { get; set; }

        [Required()]
        public int Calorie { get; set; }

        [Required(ErrorMessage = "Please enter contents")]
        [StringLength(1000, ErrorMessage = "Content too long")]
        public string Contents { get; set; }

        public List<string> orderCheckBox { get; set; }

        public DateTime Date { get; set; }

    }
}

