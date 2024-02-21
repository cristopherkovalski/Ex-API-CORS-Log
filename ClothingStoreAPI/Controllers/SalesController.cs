using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using ClothingStoreAPI.DTO;
using ClothingStoreAPI.Models;
using ClothingStoreAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace ClothingStoreAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SalesController : Controller
    {
        private readonly SalesService _salesService;

        public SalesController(SalesService saleService)
        {
            _salesService = saleService;
        }

        [HttpGet]
        public IActionResult Get()
        {
                
            var sales = _salesService.GetSales();
                if (sales == null)
                {
                   
                    return StatusCode(500, "Erro ao recuperar as vendas.");
                }
          
                return Ok(sales);
        }
        
        [HttpPost]
        public IActionResult Post([FromBody] SaleDTO saleDTO)
        {

            var newSale = _salesService.AddSales(saleDTO);     
            if (newSale == null)
            {
                return StatusCode(500, "Erro ao registrar a venda");
            }
            else
            {
                return CreatedAtAction(nameof(Get), new { id = newSale.Id }, newSale);
            }
                
        }

        [HttpPost("salereturn/{salesId}")]
        public IActionResult SaleReturn([Required] int salesId)
        {
            var salesReturn = _salesService.ProcessSaleReturn(salesId);

           
            if (salesReturn != null)
            {
               
                return Ok(salesReturn);
            }
            else
            {
                
                return NotFound();
            }
        }

        [HttpPost("exchange/")]
        public IActionResult ExchangeSale([FromBody] SaleExchangeDTO saleExchangeDTO)
        {

            var salesReturn = _salesService.ProcessSaleExchange(saleExchangeDTO);



            if (salesReturn != null)
            {

                return Ok(salesReturn);
            }
            else
            {

                return NotFound();
            }
        }

        [HttpGet("exchanges")]
        public IActionResult GetExchanges()
        {
            try
            {
                var exchanges = _salesService.GetExchanges();
                return Ok(exchanges);
            }
            catch
            {
                return StatusCode(500, "Ocorreu um erro ao buscar as trocas.");
            }
        }

        [HttpGet("returns")]
        public IActionResult GetSaleReturns()
        {
            try
            {
                var returns = _salesService.GetSaleReturns();
                return Ok(returns);
            }
            catch
            {
                return StatusCode(500, "Ocorreu um erro ao buscar as devoluções.");
            }
        }

        [HttpGet("{saleId}")]
        public IActionResult GetSaleById(int saleId)
        {
            try
            {
                var sale = _salesService.GetSaleById(saleId);
                if (sale == null)
                {
                    return NotFound();
                }
                return Ok(sale);
            }
            catch
            {
                return StatusCode(500, "Ocorreu um erro ao buscar a venda.");
            }
        }

        [HttpGet("exchanges/{saleId}")]
        public IActionResult GetExchangeBySaleId(int saleId)
        {
            try
            {
                var exchange = _salesService.GetExchangeBySaleId(saleId);
                if (exchange == null)
                {
                    return NotFound();
                }
                return Ok(exchange);
            }
            catch
            {
                return StatusCode(500, "Ocorreu um erro ao buscar a troca.");
            }
        }

        [HttpGet("returns/{saleId}")]
        public IActionResult GetSaleReturnBySaleId(int saleId)
        {
            try
            {
                var saleReturn = _salesService.GetSaleReturnBySaleId(saleId);
                if (saleReturn == null)
                {
                    return NotFound();
                }
                return Ok(saleReturn);
            }
            catch
            {
                return StatusCode(500, "Ocorreu um erro ao buscar a devolução.");
            }
        }

        [HttpGet("search")]
        public IActionResult SearchSalesByProductName([FromQuery] string productName)
        {
            try
            {
                var sales = _salesService.SearchSalesByProductName(productName);
                if (sales == null || !sales.Any())
                {
                    return NoContent();
                }
                return Ok(sales);
            }
            catch
            {
                return StatusCode(500, "Ocorreu um erro ao buscar as vendas pelo nome do produto.");
            }
        }

        [HttpGet("exchanges/search")]
        public IActionResult SearchExchangesByProductName([FromQuery] string productName)
        {
            try
            {
                var exchanges = _salesService.SearchExchangesByProductName(productName);
                if (exchanges == null || !exchanges.Any())
                {
                    return NoContent();
                }
                return Ok(exchanges);
            }
            catch
            {
                return StatusCode(500, "Ocorreu um erro ao buscar as trocas pelo nome do produto.");
            }
        }

    }
}
