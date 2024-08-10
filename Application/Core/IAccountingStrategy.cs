using CostAccounting.Models;

namespace CostAccounting.Core
{
    public interface IAccountingStrategy
    {
        public IEnumerable<AccountingLot> WrapLots(int amountForSale, IEnumerable<Lot> lots);
    }
}
