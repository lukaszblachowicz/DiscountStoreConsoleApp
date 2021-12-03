namespace DiscountStoreConsole.Entities
{
    public class Item
    {
        public readonly string Name;
        public readonly double UnitPrice;
        public readonly double DiscountedSetPrice;
        public readonly uint DiscountSetQuantityQualifier;


        public Item(string name, double unitPrice, double discountedSetPrice, uint discountSetQuantityQualifier)
        {
            Name = name;
            UnitPrice = unitPrice;
            DiscountedSetPrice = discountedSetPrice;
            DiscountSetQuantityQualifier = discountSetQuantityQualifier;
        }
    }
}