using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace test2.Models
{
    public class BloodCategory
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string? Name { get; set; }

        [Required]
        [EmailAddress]
        public string ?Email { get; set; }

        [Required]
        [Phone]
        public string ?Phone { get; set; }

        [ForeignKey("User")]
        public int ?UserId { get; set; }

        public User ?User { get; set; }
    }
}
