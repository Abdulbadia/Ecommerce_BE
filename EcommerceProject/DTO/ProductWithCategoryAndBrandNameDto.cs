namespace EcommerceProject.DTO
{
    public class ProductWithCategoryAndBrandNameDto
    {
        public int ProductId { get; set; } 
        public string ProductName { get; set; }
        public string image { get; set; }
        public string Description { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public int BrandId { get; set; }
        public string BrandName { get; set; }
        public bool Availability { get; set; }
        public float? discountPercentage { get; set; }
    }
}
