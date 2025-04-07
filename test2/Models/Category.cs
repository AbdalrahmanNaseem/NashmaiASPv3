using System.ComponentModel.DataAnnotations;

namespace test2.Models
{

    public class Category
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Description is required")]
        public string Description { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public ICollection<SubCategory>? SubCategories { get; set; }

    }

}
