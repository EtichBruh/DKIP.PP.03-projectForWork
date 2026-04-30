namespace ReviewSamples.Modules.Variants;

public class Variant28_Tool
{
    public string N;
    public double Deposit;
    public double PerDay;
}

public class Variant28_ToolRental
{
    public Variant28_Tool T;
    public int Days;
    public bool Damaged;
}

public class Variant28_RentalBad
{
    public double Calc(Variant28_ToolRental r)
    {
        double total = r.Days * r.T.PerDay;

        if (r.Days > 7)
        {
            total = total - total * 0.1;
        }

        if (r.Damaged == true)
        {
            total = total + r.T.Deposit;
        }

        return total;
    }
}
