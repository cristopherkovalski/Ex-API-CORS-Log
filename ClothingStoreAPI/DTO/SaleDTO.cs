using System.ComponentModel.DataAnnotations;
using ClothingStoreAPI.Models;

namespace ClothingStoreAPI.DTO
{
    public class SaleDTO
    {
        [Required]
        public List<Product> Products { get; set; }

        public SaleDTO() { }
    }
}
