using System;
using System.Collections.Generic;
using Xunit;

namespace FoodDeliveryApp.Tests
{
    public class OrderTests
    {
        [Fact]
        public void CalculateCost_ShouldReturnCorrectTotalPrice_ForStandardOrder()
        {
            // Arrange
            var pizza = new MenuItem { Name = "Пицца", Price = 10.00m };
            var burger = new MenuItem { Name = "Бургер", Price = 5.00m };
            var pasta = new MenuItem { Name = "Паста", Price = 7.50m };
            var items = new List<MenuItem> { pizza, burger, pasta };

            var order = OrderFactory.CreateOrder(items, isExpress: false, discountPercentage: 10);

            // Act
            var totalCost = order.CostCalculationStrategy.CalculateCost(order);

            // Assert
            Assert.Equal(27.50m, totalCost); // Ожидаемая стоимость
        }

        [Fact]
        public void ApplyCustomerPreferences_ShouldRemoveItemsWithOnion()
        {
            // Arrange
            var pizza = new MenuItem { Name = "Пицца", Price = 10.00m, ContainsOnion = true };
            var burger = new MenuItem { Name = "Бургер", Price = 5.00m, ContainsOnion = false };
            var pasta = new MenuItem { Name = "Паста", Price = 7.50m, ContainsOnion = false };

            var items = new List<MenuItem> { pizza, burger, pasta };
            var order = OrderFactory.CreateOrder(items, isExpress: false);

            // Устанавливаем предпочтения клиента
            order.Preferences.NoOnion = true;

            // Act
            order.ApplyCustomerPreferences();

            // Assert
            Assert.DoesNotContain(order.Items, item => item.ContainsOnion); // Проверяем, что лук удален
            Assert.Equal(2, order.Items.Count); // Должно остаться два блюда
        }
    }
}
