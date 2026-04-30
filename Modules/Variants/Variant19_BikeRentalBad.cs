namespace ReviewSamples.Modules.Variants;

public class Variant19_Bike
{
    public string N;
    public string Type;
}

public class Variant19_Rental
{
    public Variant19_Bike B;
    public DateTime Start;
    public DateTime End;
}

public class Variant19_BikeRentalBad
{
    public double Return(Variant19_Rental r, DateTime ret)
    {
        var hours = (ret - r.Start).TotalHours;

        double rate = 150;

        if (r.B.Type == "mountain")
        {
            rate = 200;
        }
        else if (r.B.Type == "electric")
        {
            rate = 300;
        }

        var price = hours * rate;

        Console.WriteLine("Rental " + r.B.N + " for " + hours + " hours, price = " + price);
        return price;
    }
}
