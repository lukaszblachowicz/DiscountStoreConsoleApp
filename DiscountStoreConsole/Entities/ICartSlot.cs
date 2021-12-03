namespace DiscountStoreConsole.Entities
{
    public interface ICartSlot
    {
        uint ChangeQuantity(int quantity);
        uint GetItemsQuantity();
        double SlotValue();
    }
}