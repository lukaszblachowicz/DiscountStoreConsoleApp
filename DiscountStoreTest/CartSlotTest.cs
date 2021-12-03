using DiscountStoreConsole.Entities;
using Shouldly;
using Xunit;

namespace DiscountStoreTest
{
    public class CartSlotTest
    {


        [Theory]
        [InlineData(1, 2, 3)]
        [InlineData(5, -2, 3)]
        [InlineData(5, -5, 0)]
        [InlineData(5, -6, 0)]
        [InlineData(5, 3, 8)]
        [InlineData(0, 3, 3)]
        [InlineData(0, -5, 0)]

        public void CartSlot_ChangeQuantity_ShouldBeReflectedInItemsQuantity(uint startingQuantity, int quantityChange, uint expectedQuantity)
        {
            //Arrange
            var item = new Item("TestItem", (double)13.37, (double)4.2, 3);
            var sut = new CartSlot(item, startingQuantity);

            //Act
            sut.ChangeQuantity(quantityChange);

            //Assert
            sut.GetItemsQuantity().ShouldBe(expectedQuantity);

        }
        

        [Theory]
        [InlineData(1, 5.5, 0, 0, 5.5)]
        [InlineData(3, 1.2, 0, 0, 3.6)]
        [InlineData(2, 5, 0, 0, 10)]
        [InlineData(5, 5, 0, 0, 25)]
        [InlineData(5, 5, 3, 0, 25)]
        [InlineData(5, 5, 8, 2, 21)]
        [InlineData(5, 5, 20, 5, 20)]
        [InlineData(10, 10, 3, 5, 6)]
        [InlineData(118, 343, 117.5, 0, (118*343))]
        public void CartSlot_SlotValue_ShouldReflectSumOfSetsAndNotDiscountedItems(uint quantity, double unitPrice, double setPrice, uint discountQualifier, double expectedValue)
        {
            //Arrange
            var item = new Item("TestItem", unitPrice, setPrice, discountQualifier);
            var sut = new CartSlot(item, quantity);

            
            //Assert & Assert
            sut.SlotValue().ShouldBe(expectedValue);

        }
    }
}
