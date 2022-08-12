using System.Collections.Generic;

namespace EcommerceProject.DTO
{
    public class BrandWithProductsDTO
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public virtual List<ProductBrandAndCategoryDto> products { get; set; }

            = new List<ProductBrandAndCategoryDto>();
    }
}
