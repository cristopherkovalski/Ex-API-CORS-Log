namespace ClothingStoreAPI.Models
{
    public class SaleExchange
    {
        public int Id { get; set; }
        public int SaleId {  get; set; }
        public string Reason { get; set; }
        public DateTime ExchangeDate { get; set; }
        public Product ExchangedProduct { get; set; }
        public Product NewProduct { get; set; }

    }
}
