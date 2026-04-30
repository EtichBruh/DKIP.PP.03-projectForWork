namespace ReviewSamples.Modules.Variants;

public class Variant04_Service
{
    public string N;
    public double P;
}

public class Variant04_OrderForm
{
    public string Car;
    public List<Variant04_Service> Items = new();
    public string Type;
}

public class Variant04_ServiceCenterBad
{
    public double Calc(Variant04_OrderForm f)
    {
        double total = 0;

        for (int i = 0; i < f.Items.Count; i++)
        {
            total = total + f.Items[i].P;
        }

        if (f.Type == "regular")
        {
            total = total - total * 0.05;
        }
        else if (f.Type == "vip")
        {
            total = total - total * 0.15;
        }

        if (total > 10000)
        {
            total = total - 500;
        }

        Console.WriteLine("Order total = " + total);
        return total;
    }
}
