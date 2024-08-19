using CostAccounting.Core.AccountingStrategy;
using CostAccounting.Models;

namespace CostAccounting.Core
{
    public class ShareCalculator : IShareCalculator
    {
        private IAccountingStrategy _strategy = new FIFOStrategy();
        private IEnumerable<Lot>? _lots;
        private readonly IStockData _stockData;

        public ShareCalculator(IStockData stockData)
        {
            _stockData = stockData;
        }

        public void SetStrategy(IAccountingStrategy strategy)
        {
            _strategy = strategy;
        }

        public int GetRemainingShares(int amountForSale)
        {
            if (amountForSale <= 0){
                throw new ArgumentOutOfRangeException(nameof(amountForSale), "amountForSale should be positive");
            }

            _lots ??= _stockData.GetStockLots();

            int currentAmount = _lots.Sum(lot => lot.Shares);

            if (currentAmount < amountForSale)
            {
                throw new TransactionException($"Unable to sell {amountForSale} shares. Current amount {currentAmount}");
            }

            return currentAmount - amountForSale;
        }

        public double GetCostBasisOfSoldShares(int amountForSale)
        {
            ValidateAmount(amountForSale);

            IEnumerable<AccountingLot> accountingLots = _strategy.WrapLots(amountForSale, _lots);

            return accountingLots.Where(l => l.AmountSold > 0)
                              .WeightedAverage(l => l.Lot.PricePerShare, l => l.AmountSold);
        }

        public double GetCostBasisOfRemainingShares(int amountForSale)
        {
            if (GetRemainingShares(amountForSale) == 0){
                return 0;
            }

            IEnumerable<AccountingLot> accountingLots = _strategy.WrapLots(amountForSale, _lots);

            return accountingLots.Where(l => l.Lot.Shares - l.AmountSold > 0)
                              .WeightedAverage(l => l.Lot.PricePerShare, l => l.Lot.Shares - l.AmountSold);
        }
        public double GetProfitOrLoss(int amountForSale, double price)
        {
            return (price - GetCostBasisOfSoldShares(amountForSale)) * amountForSale;
        }
        private void ValidateAmount(int amountForSale)
        {
            GetRemainingShares(amountForSale);
        }
    }
}