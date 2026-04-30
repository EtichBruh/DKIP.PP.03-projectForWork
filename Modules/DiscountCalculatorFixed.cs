namespace ReviewSamples.Modules;

public enum CustomerType
{
    New,
    Regular,
    Vip
}

public class DiscountCalculatorFixed
{
    private const decimal LargePurchaseDiscount = 50m;
    private const decimal LargePurchaseLimit = 1000m;

    public decimal Calculate(decimal amount, CustomerType customerType)
    {
        if (amount < 0)
            throw new ArgumentOutOfRangeException(nameof(amount), "Сумма покупки не может быть отрицательной.");

        var percent = GetDiscountPercent(customerType);
        var discount = amount * percent;

        if (amount > LargePurchaseLimit)
            discount += LargePurchaseDiscount;

        return Math.Max(0, amount - discount);
    }

    private static decimal GetDiscountPercent(CustomerType customerType)
    {
        return customerType switch
        {
            CustomerType.Vip => 0.20m,
            CustomerType.Regular => 0.10m,
            CustomerType.New => 0.05m,
            _ => 0m
        };
    }
}
