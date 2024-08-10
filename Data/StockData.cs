using System.Text.Json;
using CostAccounting.Core;
using CostAccounting.Models;

namespace CostAccounting.Data
{
    public class StockData : IStockData
    {
        private readonly string _filePath;

        public StockData(string filePath)
        {
            _filePath = filePath;
        }

        public IEnumerable<Lot> GetStockLots()
        {
            string json = File.ReadAllText(_filePath);
            var lots = JsonSerializer.Deserialize<List<Lot>>(json);

            return lots ?? new List<Lot>();
        }
    }
}
