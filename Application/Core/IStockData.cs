using CostAccounting.Models;

namespace CostAccounting.Core
{
    public interface IStockData
    {
        IEnumerable<Lot> GetStockLots();
    }
}
