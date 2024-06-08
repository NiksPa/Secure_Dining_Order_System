using System.ComponentModel.DataAnnotations;

namespace DiningOrderingSystem.Models.Data
{
    public class FoodOrder
    {
        [Required]
        [Key]
        public string OrderId { get; set; }

        [Required]
        public string StudentId { get; set; }

        [Required]
        public string StudentName { get; set; }

        [Required]
        public DateTime DeliveryDate { get; set; }

        [MaxLength(1000)]
        [Required]
        public string OrderedItems { get; set; }
    }
}
