namespace ReviewSamples.Modules.Variants;

public class Variant06_Car
{
    public string Model;
    public double P;
}

public class Variant06_RentalRequest
{
    public Variant06_Car Car;
    public int Days;
    public bool Insurance;
    public bool ChildSeat;
}

public class Variant06_RentalBad
{
    public double Calc(Variant06_RentalRequest r)
    {
        double total = r.Days * r.Car.P;

        if (r.Insurance == true)
        {
            total = total + r.Days * 500;
        }

        if (r.ChildSeat == true)
        {
            total = total + r.Days * 200;
        }

        if (r.Days > 14)
        {
            total = total - total * 0.1;
        }

        return total;
    }
}
