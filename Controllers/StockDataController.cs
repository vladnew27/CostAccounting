using CostAccounting.Core;
using Microsoft.AspNetCore.Mvc;

namespace CostAccounting.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StockDataController : ControllerBase
    {
        private readonly  IStockData _stockData;

        public StockDataController(IStockData stockData)
        {
            _stockData = stockData;
        }

        [HttpGet("GetStockData")]
        public IActionResult GetStockData()
        {

            var stockData = _stockData.GetStockLots();

            return Ok(stockData);
        }
    }
}
