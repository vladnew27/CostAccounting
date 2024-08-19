namespace CostAccounting.Models
{
    /*
    A wrapper around Lot for accounting purposes 
    */
    public class AccountingLot
    {
        public Lot Lot {get;}
        public int AmountSold {get;}
        
        public AccountingLot(Lot lot, int amountSold = 0)
        {
            if (amountSold > lot.Shares){
                throw new ArgumentException("amountSold can not be grater than lot.Shares");
            }
            Lot = lot;
            AmountSold = amountSold;
        }
    }
}
