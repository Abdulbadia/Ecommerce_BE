using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace EcommerceProject.models

{
    public class Product
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
        public string image { get; set; }
        public string Description { get; set; }
        [ForeignKey("Brand")]
        public int? BrandID { get; set; }
        public Brand Brand { get; set; }
        [ForeignKey("Category")]
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public bool Availability { get; set; }
        public float? discountPercentage { get; set; }

    }
}
