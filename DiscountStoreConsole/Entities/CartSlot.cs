namespace DiscountStoreConsole.Entities
{
    public class CartSlot : ICartSlot
    {
        public CartSlot(Item item) : this(item, 1) { }

        public CartSlot(Item item, uint itemQuantity)
        {
            Item = item;
            ItemQuantity = itemQuantity;
        }

        public uint ChangeQuantity(int quantity)
        {
            var newQuantity = ItemQuantity + quantity;
            ItemQuantity = newQuantity >= 0 ? (uint)newQuantity : 0;
            return ItemQuantity;
        }

        public uint GetItemsQuantity() => ItemQuantity;
        public double SlotValue() => (double)((decimal)RegularPriceItemsValue + (decimal)DiscountedItemsValue);

        private Item Item { get; }
        private uint ItemQuantity { get; set; }

        private uint GetRegularPriceQuantity() => Item.DiscountSetQuantityQualifier > 0 ? ItemQuantity % Item.DiscountSetQuantityQualifier : ItemQuantity;

        private uint DiscountedQuantity => ItemQuantity - GetRegularPriceQuantity();

        private double RegularPriceItemsValue => GetRegularPriceQuantity() * Item.UnitPrice;

        private uint SetsQuantity =>  Item.DiscountSetQuantityQualifier != 0 ? DiscountedQuantity / Item.DiscountSetQuantityQualifier : 0;

        private double DiscountedItemsValue => SetsQuantity * Item.DiscountedSetPrice;
        
    }
}
