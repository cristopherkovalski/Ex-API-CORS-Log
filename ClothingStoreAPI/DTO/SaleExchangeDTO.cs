using System.ComponentModel.DataAnnotations;
using ClothingStoreAPI.Models;

namespace ClothingStoreAPI.DTO
{
    public class SaleExchangeDTO
    {
        [Required]
        public int SaleId { get; set; }
        [Required]
        public string Reason { get; set; }
        [Required]
        public Product ExchangedProduct { get; set; }
        [Required]
        public Product NewProduct { get; set; }

        public SaleExchangeDTO() { }
    }
}
