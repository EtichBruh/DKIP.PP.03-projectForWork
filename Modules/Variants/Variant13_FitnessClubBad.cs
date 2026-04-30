namespace ReviewSamples.Modules.Variants;

public class Variant13_Plan
{
    public string Type;
    public int Months;
    public double P;
}

public class Variant13_Membership
{
    public string Customer;
    public Variant13_Plan Plan;
    public DateTime Start;
    public DateTime End;
}

public class Variant13_FitnessBad
{
    public Variant13_Membership Buy(Variant13_Plan p, string customer, DateTime start)
    {
        var m = new Variant13_Membership();
        m.Customer = customer;
        m.Plan = p;
        m.Start = start;

        if (p.Type == "month")
        {
            m.End = start.AddMonths(1);
        }
        else if (p.Type == "halfyear")
        {
            m.End = start.AddMonths(6);
        }
        else if (p.Type == "year")
        {
            m.End = start.AddYears(1);
        }

        return m;
    }
}
