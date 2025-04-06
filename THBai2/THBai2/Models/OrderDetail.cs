using Microsoft.EntityFrameworkCore;
using THBai2.Models;

namespace THBai2.Models
{
    public class OrderDetail
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        [Precision(18, 2)]
        public decimal Price { get; set; }
        public Order? Order { get; set; }
        public Product? Product { get; set; }
    }
}
