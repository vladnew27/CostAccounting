namespace CostAccounting.Core
{
    public interface IShareCalculator
    {
        // Sets the accounting strategy (e.g., FIFO, LIFO)
        void SetStrategy(IAccountingStrategy strategy);

        // Returns the remaining shares after a sale
        int GetRemainingShares(int amountForSale);

        // Calculates the cost basis of the sold shares
        double GetCostBasisOfSoldShares(int amountForSale);

        // Calculates the cost basis of the remaining shares after a sale
        double GetCostBasisOfRemainingShares(int amountForSale);

        // Calculates the total profit or loss of a sale
        double GetProfitOrLoss(int amountForSale, double price);
    }
}
