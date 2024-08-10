namespace CostAccounting.Models
{
    public class Lot
    {
        public int Shares { get; set; }
        public double PricePerShare { get; set; }
        public DateTime PurchaseDate { get; set; } 

        public Lot(int shares, double pricePerShare, DateTime purchaseDate)
        {
            Shares = shares;
            PricePerShare = pricePerShare;
            PurchaseDate = purchaseDate;
        }
    }
}
