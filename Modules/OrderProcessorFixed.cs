namespace ReviewSamples.Modules;

public record Order(int Quantity, decimal UnitPrice, bool HasDiscount);

public class OrderProcessorFixed
{
    private const decimal DiscountRate = 0.10m;
    private const decimal ExpensiveDeliveryLimit = 1000m;
    private const decimal StandardDeliveryPrice = 100m;
    private const decimal ExpensiveDeliveryPrice = 200m;

    public decimal Process(Order order)
    {
        Validate(order);

        var itemsTotal = order.Quantity * order.UnitPrice;
        var discountedTotal = ApplyDiscount(itemsTotal, order.HasDiscount);
        var deliveryPrice = CalculateDeliveryPrice(discountedTotal);

        return discountedTotal + deliveryPrice;
    }

    private static void Validate(Order order)
    {
        if (order.Quantity <= 0)
            throw new ArgumentOutOfRangeException(nameof(order.Quantity), "Количество должно быть больше нуля.");

        if (order.UnitPrice < 0)
            throw new ArgumentOutOfRangeException(nameof(order.UnitPrice), "Цена не может быть отрицательной.");
    }

    private static decimal ApplyDiscount(decimal total, bool hasDiscount)
    {
        return hasDiscount ? total * (1 - DiscountRate) : total;
    }

    private static decimal CalculateDeliveryPrice(decimal total)
    {
        return total > ExpensiveDeliveryLimit ? ExpensiveDeliveryPrice : StandardDeliveryPrice;
    }
}
