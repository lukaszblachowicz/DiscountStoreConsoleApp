using System.Collections.Generic;
using System.Linq;
using System.Text;
using DiscountStoreConsole.Entities;

namespace DiscountStoreConsole.Services
{
    public class CartService : ICartService
    {

        private  Dictionary<string, CartSlot> CartSlots { get; } = new Dictionary<string, CartSlot>();


        public void Add(Item item)
        {
            Add(item, 1);
        }

        public void Add(Item item, uint quantity)
        {

            var itemName = item.Name;
            if (CartSlots.ContainsKey(itemName))
            {
                CartSlots[itemName].ChangeQuantity((int)quantity);
                return;
            }
            CartSlots.Add(itemName, new CartSlot(item, quantity));
        }

        public void Remove(Item item)
        {
            Remove(item, 1);
        }

        public void Remove(Item item, uint quantity)
        {
            var itemName = item.Name;

            if (!CartSlots.ContainsKey(itemName)) return;

            var slot = CartSlots[itemName];
            if (slot.ChangeQuantity(-(int)quantity) == 0)
            {
                CartSlots.Remove(itemName);
            }
        }
        public double GetTotal()
        {
            return (double)CartSlots.Sum(x => (decimal)x.Value.SlotValue());
        }

        public override string ToString()
        {
            var output = new StringBuilder();
            output.AppendLine($"Items in cart (distinct items types: {CartSlots.Count}):");
            foreach (var cartSlot in CartSlots)
            {
                output.AppendLine(
                    $"{cartSlot.Key} x {cartSlot.Value.GetItemsQuantity()}, Total: {cartSlot.Value.SlotValue()}");
            }

            output.AppendLine($"Total value: {GetTotal()}");

            return output.ToString();
        }
    }
}
