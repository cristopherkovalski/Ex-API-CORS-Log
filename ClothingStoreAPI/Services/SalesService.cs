using ClothingStoreAPI.DTO;
using ClothingStoreAPI.Models;
using static ClothingStoreAPI.Services.SalesService;

namespace ClothingStoreAPI.Services
{
    public class SalesService
    {
        private readonly List<Sale> _sales;
        private readonly List<SaleReturn> _salesReturn;
        private readonly List<SaleExchange> _exchanges;

        public SalesService()
        {
            _sales = new List<Sale>();
            _salesReturn = new List<SaleReturn>();
            _exchanges = new List<SaleExchange>();
        }


        public void AddSampleData()
        {
          
            var product1 = new Product { Id = "1", Name = "Product 1", Price = 10.0m };
            var product2 = new Product { Id = "2", Name = "Product 2", Price = 20.0m };
            var product3 = new Product { Id = "3", Name = "Product 3", Price = 30.0m };
            var product4 = new Product { Id = "4", Name = "Product 4", Price = 40.0m };
            var product5 = new Product { Id = "5", Name = "Product 5", Price = 50.0m };

            _sales.Add(new Sale { Id = 1, Products = new List<Product> { product1, product2 }, SaleDate = DateTime.Now, hasExchange = false, hasReturn = false });
            _salesReturn.Add(new SaleReturn { Id = 1, SaleId = 1, SaleReturnDate = DateTime.Now });
            _exchanges.Add(new SaleExchange { Id = 1, SaleId = 1, Reason = "Exchange reason", ExchangeDate = DateTime.Now, ExchangedProduct = product3, NewProduct = product4 });

            _sales.Add(new Sale { Id = 2, Products = new List<Product> { product3, product4 }, SaleDate = DateTime.Now, hasExchange = true, hasReturn = true });
            _salesReturn.Add(new SaleReturn { Id = 2, SaleId = 2, SaleReturnDate = DateTime.Now });
            _exchanges.Add(new SaleExchange { Id = 2, SaleId = 2, Reason = "Another exchange reason", ExchangeDate = DateTime.Now, ExchangedProduct = product1, NewProduct = product5 });
        }

        public IEnumerable<Sale> GetSales()
        {
            try
            {
                return _sales;
            }
            catch
            {
                throw new InvalidOperationException("Ocorreu um erro ao buscar as vendas.");
            }

        }

        public Sale AddSales(SaleDTO saleDTO)
        {
            try
            {
                Sale sale = new Sale();
                if (saleDTO.Products != null)
                {
                   
                    sale.Products = new List<Product>();
                    

                    foreach (var productDTO in saleDTO.Products)
                    {

                        Product product = new Product
                        {
                            Id = productDTO.Id,
                            Name = productDTO.Name,
                            Price = productDTO.Price
                        };


                        sale.Products.Add(product);
                    }
                }
                sale.Id = _sales.Count + 1;
                sale.SaleDate = DateTime.Now;
                _sales.Add(sale);
                return sale;
            }
            catch
            {
                throw new InvalidOperationException("Ocorreu um erro ao adicionar a venda.");
            }
        }

        public SaleReturn ProcessSaleReturn(int saleId)
        {
            try
            {
                Sale sale = _sales.FirstOrDefault(s => s.Id == saleId);
                sale.hasReturn = true;
                SaleReturn saleReturn = new SaleReturn();
                saleReturn.Id = _salesReturn.Count + 1;
                saleReturn.SaleId = saleId;
                saleReturn.SaleReturnDate = DateTime.Now;
                _salesReturn.Add(saleReturn);
                return saleReturn;
            }
            catch
            {
                throw new InvalidOperationException("Ocorreu um erro ao processar a devolução de venda.");
            }
        }

        public SaleExchange ProcessSaleExchange(SaleExchangeDTO saleExchangeDTO)
        {
            try
            {
                if (VerifyProductList(saleExchangeDTO.SaleId, saleExchangeDTO.ExchangedProduct))
                {
                    SaleExchange saleExchanged = new SaleExchange();
                    saleExchanged.Id = _exchanges.Count + 1;
                    saleExchanged.SaleId = saleExchangeDTO.SaleId;
                    saleExchanged.ExchangedProduct = saleExchangeDTO.ExchangedProduct;
                    saleExchanged.NewProduct = saleExchangeDTO.NewProduct;
                    saleExchanged.Reason = saleExchangeDTO.Reason;
                    saleExchanged.ExchangeDate = DateTime.Now;
                    _exchanges.Add(saleExchanged);
                    return saleExchanged;
                }
                else
                {
                    throw new InvalidOperationException("A venda não possui o produto a ser substituído.");
                }
            }
            catch
            {
                throw new InvalidOperationException("Ocorreu um erro ao processar a troca de produto da venda.");

            }
        }

        public IEnumerable<SaleExchange> GetExchanges()
        {
            try
            {
                return _exchanges;
            }
            catch
            {
                throw new InvalidOperationException("Ocorreu um erro ao buscar as trocas.");
            }
        }

        public IEnumerable<SaleReturn> GetSaleReturns()
        {
            try
            {
                return _salesReturn;
            }
            catch
            {
                throw new InvalidOperationException("Ocorreu um erro ao buscar as trocas.");
            }
        }

        public Sale GetSaleById(int saleId)
        {
            try
            {
                return _sales.FirstOrDefault(s => s.Id == saleId);
            }
            catch
            {
                throw new InvalidOperationException("Ocorreu um erro ao buscar a venda.");
            }
        }

        public SaleExchange GetExchangeBySaleId(int saleId)
        {
            try
            {
                return _exchanges.FirstOrDefault(s => s.SaleId == saleId);
            }
            catch
            {
                throw new InvalidOperationException("Ocorreu um erro ao buscar a troca.");
            }
        }

        public SaleReturn GetSaleReturnBySaleId(int saleId)
        {
            try
            {
                return _salesReturn.FirstOrDefault(s => s.SaleId == saleId);
            }
            catch
            {
                throw new InvalidOperationException("Ocorreu um erro ao buscar a devolução.");
            }
        }
        public IEnumerable<Sale> SearchSalesByProductName(string productName)
        {
            try
            {
                return _sales.Where(sale => sale.Products.Any(product => product.Name.Contains(productName)));
            }
            catch
            {
                throw new InvalidOperationException("Ocorreu um erro ao buscar as vendas pelo nome do produto.");
            }
        }

        public IEnumerable<SaleExchange> SearchExchangesByProductName(string productName)
        {
            try
            {
                return _exchanges.Where(exchange => exchange.ExchangedProduct.Name.Contains(productName) || exchange.NewProduct.Name.Contains(productName));
            }
            catch
            {
                throw new InvalidOperationException("Ocorreu um erro ao buscar as trocas pelo nome do produto.");
            }
        }



        private bool VerifyProductList(int saleId, Product product)
        {
            try
            {
                var sale = _sales.FirstOrDefault(s => s.Id == saleId);

                if (sale == null)
                {
                    return false; 
                }

                foreach (var p in sale.Products)
                {
                    if (p.Id == product.Id)
                    {
                        return true; 
                    }
                }
                return false; 
            }
            catch
            {
                return false; 
            }
        }



        
    }
}


