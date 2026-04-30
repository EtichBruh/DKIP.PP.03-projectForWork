namespace ReviewSamples.Modules.Variants;

public class Variant12_DeliveryRequest
{
    public double OrderSum;
    public double Km;
}

public class Variant12_DeliveryBad
{
    public double Calc(Variant12_DeliveryRequest r)
    {
        if (r.OrderSum > 2000)
        {
            return 0;
        }

        double fee = 100;

        if (r.Km > 5)
        {
            fee = fee + (r.Km - 5) * 30;
        }

        if (r.OrderSum < 500)
        {
            fee = fee + 50;
        }

        Console.WriteLine("Delivery fee = " + fee);
        return fee;
    }
}
