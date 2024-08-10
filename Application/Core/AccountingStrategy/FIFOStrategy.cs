using CostAccounting.Models;

namespace CostAccounting.Core.AccountingStrategy
{
    public class FIFOStrategy : IAccountingStrategy
    {
        public IEnumerable<AccountingLot> WrapLots(int amountForSale, IEnumerable<Lot> lots)
        {
            if (amountForSale < 0)
            {
                throw new ArgumentException("amountForSale cannot be negative");
            }

            var sortedLots = lots.OrderBy(lot => lot.PurchaseDate);
            int remainingShares = amountForSale;

            foreach (var lot in sortedLots)
            {
                if (remainingShares == 0)
                {
                    yield return new AccountingLot(lot);
                }
                else if (lot.Shares >= remainingShares)
                {
                    yield return new AccountingLot(lot, remainingShares);
                    remainingShares = 0;
                }
                else
                {
                    yield return new AccountingLot(lot, lot.Shares);
                    remainingShares -= lot.Shares;
                }
            }
        }
    }
}
