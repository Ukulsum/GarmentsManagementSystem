using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace FahimKniteComposite.Models
{
    public class Products
    {
        [Key]
        public int ProductID { get; set; }
        [ForeignKey("Category")]
        public int CategoryID { get; set; }

        [Required]
        public string ProductName { get; set; }

        //[AllowHtml]
        [UIHint("tinymce_full")]
        public string Description { get; set; }

        [ValidateNever]
        public string Path { get; set; }

        [NotMapped]
        [Display(Name = "Choose Image")]
        [ValidateNever]
        public IFormFile ImagePath { get; set; }
        [ValidateNever]
        public Category Category { get; set; }

    }
}
