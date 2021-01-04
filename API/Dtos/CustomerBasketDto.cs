using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace API.Dtos
{
    public class CustomerBasketDto
    {
        [Required]
        public string Id { get; set; }
        public decimal ShippingAndHandling { get; set; }
        public int DeliveryMethodId { get; set; }
        public IEnumerable<BasketItemDto> Items { get; set; }
    }

    public class BasketItemDto
    {
        [Required]
        public int ProductId { get; set; }
        [Required]
        public string ProductName { get; set; }
        [Required]
        [Range(minimum: 0.1, maximum: 1000, ErrorMessage = "price must be bwtween 0.1 to 1000")]
        public decimal Price { get; set; }
        [Required]
        [Range(minimum: 1, maximum: 100, ErrorMessage = "quantity must be bwtween 1 to 10")]
        public int Quantity { get; set; }
        [Required]
        public string PictureUrl { get; set; }
        [Required]
        public string Brand { get; set; }
        [Required]
        public string Type { get; set; }
    }
}
