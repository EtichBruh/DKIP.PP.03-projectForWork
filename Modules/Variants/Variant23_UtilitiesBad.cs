namespace ReviewSamples.Modules.Variants;

public class Variant23_MeterReading
{
    public double Prev;
    public double Curr;
    public string Type;
}

public class Variant23_BillingBad
{
    public double Calc(Variant23_MeterReading water, Variant23_MeterReading electricity)
    {
        var w = water.Curr - water.Prev;
        var e = electricity.Curr - electricity.Prev;

        double total = 0;

        if (water.Type == "cold")
        {
            total = total + w * 35;
        }
        else if (water.Type == "hot")
        {
            total = total + w * 180;
        }

        if (electricity.Type == "day")
        {
            total = total + e * 5.5;
        }
        else if (electricity.Type == "night")
        {
            total = total + e * 2.5;
        }

        return total;
    }
}
