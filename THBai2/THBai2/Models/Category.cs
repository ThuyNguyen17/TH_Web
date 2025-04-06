using System.ComponentModel.DataAnnotations;

namespace THBai2.Models
{
    public class Category
    {
        public int Id { get; set; }
        [Required, StringLength(50)]
        public string? Name { get; set; }
        //public virtual List<Product> Products { get; set; }   // có thể được EF gán tự động từ database không cần include
        public List<Product>? Products { get; set; }
    }
}
