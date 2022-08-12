using System.ComponentModel.DataAnnotations.Schema;

namespace EcommerceProject.models
{
    public class OrderDetials
    {
        [ForeignKey("Product")]
        public int ProductId { get; set; }
        public Product Product { get; set; }

        [ForeignKey("Order")]
        public int OrderId { get; set; }
        public Order Order { get; set; }
        public int Quantity { get; set; }
        public decimal price { get; set; }
    }
}
