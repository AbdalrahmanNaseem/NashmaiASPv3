using System.ComponentModel.DataAnnotations;

namespace test2.Models
{

    public class SubCategory
    {
        public int Id { get; set; }
        public string ? Name { get; set; }
        public string ? Description { get; set; }
        [Required(ErrorMessage = "حقل الفئة الرئيسية مطلوب")] 
        public int CategoryId { get; set; } // (Foreign Key)
        public DateTime? CreatedAt { get; set; } = DateTime.Now;

        // ////////////////
        public required Category Category { get; set; }
    }



}
