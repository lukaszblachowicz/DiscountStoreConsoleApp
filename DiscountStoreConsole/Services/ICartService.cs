using DiscountStoreConsole.Entities;

namespace DiscountStoreConsole.Services
{
    public interface ICartService
    {
        void Add(Item item);
        void Remove(Item item);
        double GetTotal();
    }

}
