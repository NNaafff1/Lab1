using System;
using System.Collections.Generic;
using System.Linq;

// Модели данных
public class MenuItem
{
    public string Name { get; set; }
    public decimal Price { get; set; }
    public bool ContainsOnion { get; set; }
}

// Предпочтения клиента
public class CustomerPreferences
{
    public bool NoOnion { get; set; }
    public bool NoSalt { get; set; }
}

// Интерфейсы и классы расчета стоимости
public interface ICostCalculationStrategy
{
    decimal CalculateCost(Order order);
}

public abstract class BaseCostCalculation : ICostCalculationStrategy
{
    protected virtual decimal DeliveryFee => 5.00m;

    public decimal CalculateCost(Order order)
    {
        decimal baseCost = order.Items.Sum(item => item.Price);
        decimal discount = order.DiscountPercentage > 0 ? baseCost * order.DiscountPercentage / 100 : 0;
        decimal tax = baseCost * 0.1m; // 10% налог
        return baseCost - discount + tax + DeliveryFee;
    }
}

public class StandardOrderCostCalculation : BaseCostCalculation { }

public class ExpressOrderCostCalculation : BaseCostCalculation
{
    protected override decimal DeliveryFee => 10.00m;
}

// Интерфейсы и классы состояний
public interface IOrderState
{
    void UpdateOrderStatus(Order order);
}

public class PreparingState : IOrderState
{
    public void UpdateOrderStatus(Order order)
    {
        Console.WriteLine("Заказ в процессе подготовки...");
        order.CurrentState = new DeliveringState();
    }
}

public class DeliveringState : IOrderState
{
    public void UpdateOrderStatus(Order order)
    {
        Console.WriteLine("Заказ в пути...");
        order.CurrentState = new CompletedState();
    }
}

public class CompletedState : IOrderState
{
    public void UpdateOrderStatus(Order order)
    {
        Console.WriteLine("Заказ завершен и доставлен.");
        order.IsCompleted = true;
    }
}

// Основной класс заказа
public class Order
{
    public List<MenuItem> Items { get; set; } = new List<MenuItem>();
    public decimal TotalPrice { get; set; }
    public IOrderState CurrentState { get; set; }
    public ICostCalculationStrategy CostCalculationStrategy { get; set; }
    public decimal DiscountPercentage 
    { 
        get => _discountPercentage; 
        set => _discountPercentage = (value >= 0 && value <= 100) ? value : throw new ArgumentOutOfRangeException(nameof(DiscountPercentage), "Скидка должна быть от 0 до 100%");
    }
    private decimal _discountPercentage;

    public bool IsCompleted { get; set; } = false;
    public CustomerPreferences Preferences { get; set; } = new CustomerPreferences();

    // Фильтруем блюда в соответствии с предпочтениями клиента
    public void ApplyCustomerPreferences()
    {
        Items = Items.Where(item => 
            (!Preferences.NoOnion || !item.ContainsOnion)
        ).ToList();
    }
}

// Фабрика заказов
public class OrderFactory
{
    public static Order CreateOrder(List<MenuItem> items, bool isExpress, decimal discountPercentage = 0)
    {
        var order = new Order { Items = items, DiscountPercentage = discountPercentage };
        order.CostCalculationStrategy = isExpress 
            ? new ExpressOrderCostCalculation() 
            : new StandardOrderCostCalculation();
        order.CurrentState = new PreparingState();
        order.ApplyCustomerPreferences(); // Применение предпочтений клиента
        return order;
    }
}

// Обработка заказа
public class OrderProcessor
{
    public void ProcessOrder(Order order)
    {
        var totalCost = order.CostCalculationStrategy.CalculateCost(order);
        Console.WriteLine($"Общая стоимость заказа: {totalCost:C}");

        // Переводим заказ через состояния
        while (!order.IsCompleted)
        {
            order.CurrentState.UpdateOrderStatus(order);
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        // Пример меню с добавлением предпочтений
        var pizza = new MenuItem { Name = "Пицца", Price = 10.00m, ContainsOnion = true };
        var burger = new MenuItem { Name = "Бургер", Price = 5.00m, ContainsOnion = false };
        var pasta = new MenuItem { Name = "Паста", Price = 7.50m, ContainsOnion = false };

        // Пример создания заказа
        var items = new List<MenuItem> { pizza, burger, pasta };
        var order = OrderFactory.CreateOrder(items, isExpress: false, discountPercentage: 10);  // Стандартный заказ со скидкой 10%

        // Создаем процессор для обработки заказа
        var processor = new OrderProcessor();
        processor.ProcessOrder(order);  // Обработка заказа: расчет стоимости и изменение состояний

        // Пример срочного заказа с предпочтениями клиента (не лук)
        var items2 = new List<MenuItem> { pizza, pasta };
        var expressOrder = OrderFactory.CreateOrder(items2, isExpress: true, discountPercentage: 5);  // Срочный заказ со скидкой 5%
        expressOrder.Preferences.NoOnion = true;  // Применяем предпочтение клиента

        processor.ProcessOrder(expressOrder);  // Обработка срочного заказа
    }
}
