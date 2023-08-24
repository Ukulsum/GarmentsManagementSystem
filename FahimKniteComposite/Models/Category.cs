using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FahimKniteComposite.Models
{
    public class Category
    {
        [Key]
        public int CatId { get; set; }

        [Required]
        [Display(Name = "Category Name")]
        public string CatName { get; set; }
        public int ParentId { get; set; }
        
        //use enum
        public Person Persons { get; set; }
    }
    public class CategoryVM
    {
        [Key]
        public int CatId { get; set; }

        [Required]
        [Display(Name = "Category Name")]
        public string CatName { get; set; }
        public int ParentId { get; set; }
        [NotMapped]
        public string ParentCategory { get; set; }
        //use enum
        public Person Persons { get; set; }
    }
    public enum Person
    {
        Man=1, Woman=2, Baby=3, None=0
    }
}
