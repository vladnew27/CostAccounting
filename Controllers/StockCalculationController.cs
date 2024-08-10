using CostAccounting.Core;
using CostAccounting.Core.AccountingStrategy;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace CostAccounting.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StockCalculationController : ControllerBase
    {
        private readonly IShareCalculator _shareCalculator;

        public StockCalculationController(IShareCalculator shareCalculator)
        {
            _shareCalculator = shareCalculator;
        }

        [HttpGet("GetCalculations")]
        public IActionResult GetStockData([FromQuery][Range(0, Int32.MaxValue)] int amountForSale, [FromQuery][Range(0, Double.MaxValue)] double price)
        {
            
            try
            {
                _shareCalculator.SetStrategy(new FIFOStrategy());

                int remaining = _shareCalculator.GetRemainingShares(amountForSale);

                double costBasisOfSoldShare = _shareCalculator.GetCostBasisOfSoldShares(amountForSale);

                double costBasisOfRemainingShares = _shareCalculator.GetCostBasisOfRemainingShares(amountForSale);

                double profitOrLoss = _shareCalculator.GetProfitOrLoss(amountForSale, price);

                var result = new
                {
                    RemainingShares = remaining,
                    CostBasisOfSoldShares = costBasisOfSoldShare,
                    CostBasisOfRemainingShares = costBasisOfRemainingShares,
                    ProfitOrLoss = profitOrLoss
                };

                return Ok(result);
            }
            catch (TransactionException ex)
            {
                return BadRequest(new { error = ex.Message });
            } 
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "An unexpected error occurred.", details = ex.Message });
            }

        }
    }
}
