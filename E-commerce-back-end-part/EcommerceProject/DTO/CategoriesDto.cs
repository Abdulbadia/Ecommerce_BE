using System.Collections.Generic;

namespace EcommerceProject.DTO
{
    public class CategoriesDto
    {
        public int ID { get; set; }
        public string CatName { get; set; }
        public virtual List<ProductDTO>? Products { get; set; }
        = new List<ProductDTO>();
    }
}
