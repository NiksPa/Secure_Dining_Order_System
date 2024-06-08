using System.ComponentModel.DataAnnotations;

namespace DiningOrderingSystem.Models.Data
{
    public class NoticeItem
    {
        [Required]
        [Key]
        public string NoticeTitle { get; set; }

        [Required]
        public string NoticeContent { get; set; }

        [Required]
        public DateTime NoticeDate { get; set; }
    }
}
