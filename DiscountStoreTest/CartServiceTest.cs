using System;
using DiscountStoreConsole;
using DiscountStoreConsole.Entities;
using DiscountStoreConsole.Services;
using Moq;
using Shouldly;
using Xunit;

namespace DiscountStoreTest
{
    public class CartServiceTest
    {
        private readonly Item _vase = new Item("Vase", 1.2, 0, 0);
        private readonly Item _bigMug = new Item("Big Mug", 1, 1.5, 2);
        private readonly Item _napkins = new Item("Napkins Pack", 0.45, 0.9, 3);


        [Fact]
        public void CartService_Add_SingleItem_ShouldTotalSameValueAsSingleItemUnitPrice()
        {
            //Arrange
            const double unitPrice = (double)12.23;

            var sut = new CartService();
            var item = new Item("Test item", unitPrice, (double) 10, 3);

            //Act
            sut.Add(item);

            //Assert
            sut.GetTotal().ShouldBe(unitPrice);

        }


        [Fact]
        public void CartService_Add_SingleItem_ShouldIncreaseValueOfTheCartAppropriately()
        {
            //Arrange
            var sut = new CartService();

            var item1 = new Item("Vase", 1.2, 0, 0);

            //Act
            sut.Add(item1);

            //Assert
            sut.GetTotal().ShouldBe(1.2);

        }

        [Theory]
        [InlineData(1.2, 10, 12)]
        [InlineData(-1.2, 10, -12)] //like a discount code or sth? 
        public void CartService_Add_MultipleItemsWithoutDiscount_ShouldIncreaseValueOfTheCartAppropriately(double unitPrice, uint quantity, double expectedTotal)
        {
            //Arrange
            var sut = new CartService();

            var item1 = new Item("Vase", unitPrice, 0, 0);

            //Act
            sut.Add(item1, quantity);

            //Assert
            sut.GetTotal().ShouldBe(expectedTotal);

        }

        [Theory]
        [InlineData(1.2, 10, 13.2)]
        [InlineData(-1.2, 10, -13.2)] 
        public void CartService_Add_MultipleItemsWithoutDiscountMoreThanOnce_ShouldIncreaseValueOfTheCartAppropriately(double unitPrice, uint quantity, double expectedTotal)
        {
            //Arrange
            var sut = new CartService();

            var item1 = new Item("Vase", unitPrice, 0, 0);

            //Act
            sut.Add(item1, quantity);
            sut.Add(item1);

            //Assert
            sut.GetTotal().ShouldBe(expectedTotal);

        }


        [Theory]
        [InlineData(1, 0, 0, 0, 0, 0, 1.2)]
        [InlineData(1, 0, 0, 1, 0, 0, 2.4)]
        [InlineData(1, 0, 0, 9, 0, 0, 12)]
        [InlineData(0, 1, 0, 0, 0, 0, 1)]
        [InlineData(0, 2, 0, 0, 0, 0, 1.5)]
        [InlineData(0, 1, 0, 0, 1, 0, 1.5)]
        [InlineData(0, 1, 0, 0, 2, 0, 2.5)]
        [InlineData(0, 2, 0, 0, 2, 0, 3)]
        [InlineData(0, 0, 1, 0, 0, 0, 0.45)]
        [InlineData(0, 0, 2, 0, 0, 0, 0.9)]
        [InlineData(0, 0, 3, 0, 0, 0, 0.9)]
        [InlineData(0, 0, 3, 0, 0, 2, 1.8)]
        [InlineData(0, 0, 3, 0, 0, 3, 1.8)]
        [InlineData(1, 1, 1, 0, 0, 0, 2.65)]
        public void CartService_Add_MultipleDifferentItems_ShouldIncreaseValueOfTheCartAppropriately(uint item1StartQuantity, uint  item2StartQuantity, uint item3StartQuantity, uint item1Change, uint item2Change, uint item3Change, double expectedTotal)
        {
            //Arrange
            var sut = new CartService();

            sut.Add(_vase, item1StartQuantity);
            sut.Add(_bigMug, item2StartQuantity);
            sut.Add(_napkins, item3StartQuantity);

            //Act
            sut.Add(_vase, item1Change);
            sut.Add(_bigMug, item2Change);
            sut.Add(_napkins, item3Change);


            //Assert
            sut.GetTotal().ShouldBe(expectedTotal);

        }


        [Theory]
        [InlineData(1, 0, 0, 1, 0, 0, 0)]
        [InlineData(1, 0, 0, 9, 0, 0, 0)]
        [InlineData(0, 1, 0, 1, 0, 0, 1)]
        [InlineData(0, 1, 0, 0, 2, 0, 0)]
        [InlineData(0, 2, 0, 1, 2, 0, 0)]
        [InlineData(0, 0, 3, 0, 0, 2, 0.45)]
        [InlineData(5, 0, 0, 3, 0, 0, 2.4)]
        [InlineData(5, 5, 0, 3, 3, 0, 3.9)]
        [InlineData(0, 0, 7, 0, 0, 3, 1.35)]
        public void CartService_Remove_MultipleDifferentItems_ShouldDecreaseValueOfTheCartAppropriately(uint item1StartQuantity, uint item2StartQuantity, uint item3StartQuantity, uint item1Change, uint item2Change, uint item3Change, double expectedTotal)
        {
            //Arrange
            var sut = new CartService();

            sut.Add(_vase, item1StartQuantity);
            sut.Add(_bigMug, item2StartQuantity);
            sut.Add(_napkins, item3StartQuantity);

            //Act
            sut.Remove(_vase, item1Change);
            sut.Remove(_bigMug, item2Change);
            sut.Remove(_napkins, item3Change);


            //Assert
            sut.GetTotal().ShouldBe(expectedTotal);

        }
    }
}
