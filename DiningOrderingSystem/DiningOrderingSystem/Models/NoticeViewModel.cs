using System.ComponentModel.DataAnnotations;

namespace DiningOrderingSystem.Models
{
    public class NoticeViewModel
    {
        public NoticeViewModel()
        {

        }
        [Required(ErrorMessage = "Please enter the notice title.")]
        [Key]
        public string NoticeTitle { get; set; }

        [Required(ErrorMessage ="Please enter the notice content.")]
        public string NoticeContent { get; set; }


        [Required]
        public DateTime Date { get; set; }
    }
}
