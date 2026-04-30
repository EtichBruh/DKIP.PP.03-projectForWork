namespace ReviewSamples.Modules;

public class OrderProcessorBad
{
    public double Process(int c, double p, bool d)
    {
        double total = c * p;

        if (d == true)
        {
            total = total - total * 0.1;
        }

        if (total > 1000)
        {
            total = total + 200;
        }
        else
        {
            total = total + 100;
        }

        Console.WriteLine("Order total = " + total);
        return total;
    }
}
