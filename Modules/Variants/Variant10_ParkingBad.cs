namespace ReviewSamples.Modules.Variants;

public class Variant10_ParkingTicket
{
    public DateTime In;
    public DateTime Out;
    public string Tariff;
}

public class Variant10_ParkingBad
{
    public double Calc(Variant10_ParkingTicket t)
    {
        var min = (t.Out - t.In).TotalMinutes;

        if (min < 15)
        {
            return 0;
        }

        var hours = min / 60;

        double rate = 100;

        if (t.Tariff == "day")
        {
            rate = 100;
        }
        else if (t.Tariff == "night")
        {
            rate = 50;
        }
        else if (t.Tariff == "weekend")
        {
            rate = 80;
        }

        return hours * rate;
    }
}
