namespace ClothingStoreAPI.Models
{
    public class Sale
    {
       public int Id { get; set; }
       public List<Product> Products { get; set; }
       public DateTime SaleDate { get; set; }
       public bool hasExchange { get; set; }
       public bool hasReturn { get; set; }
       public Sale() { }

    }
}
