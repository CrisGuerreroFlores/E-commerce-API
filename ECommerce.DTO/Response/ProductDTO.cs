using EcommerceAPI.Entities;

namespace ECommerce.DTO.Response
{
    public class ProductDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
        public decimal UnitPrice { get; set; }
        public Category Category { get; set; }
        public string CategoryId { get; set; }
        public bool Active { get; set; }
    }
}
