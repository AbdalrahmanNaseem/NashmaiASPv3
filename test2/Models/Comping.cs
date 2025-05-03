using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace test2.Models
{
    public class Comping
    {
        public int Id { get; set; }

        [Required]
        public string ?Title { get; set; }

        public string? ShortDesc { get; set; }

        public string? LongDesc { get; set; }


        [Display(Name = "الهدف")]
        [Range(1, int.MaxValue, ErrorMessage = "الهدف يجب أن يكون رقم موجب")]
        public int? Goal { get; set; }

        [Display(Name = "المبلغ المُجمّع")]
        [Range(0, double.MaxValue, ErrorMessage = "المبلغ يجب أن يكون 0 أو أكبر")]
        public int? Amount { get; set; }

        [ForeignKey("Category")]
        [Display(Name = "Category")]
        public int? CategoryId { get; set; }

        public Category? Category { get; set; }

        [ForeignKey("User")]
        [Display(Name = "User")]
        public int? UserId { get; set; }

        public User ?User { get; set; }
    }
}
