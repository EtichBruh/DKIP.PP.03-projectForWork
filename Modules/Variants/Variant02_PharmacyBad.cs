namespace ReviewSamples.Modules.Variants;

public class Variant02_Medicine
{
    public string N;
    public string S;
    public DateTime D;
    public int Q;
}

public class Variant02_PharmacyBad
{
    public bool Exp(Variant02_Medicine m, DateTime t)
    {
        if (m.D < t)
        {
            return true;
        }
        if ((m.D - t).Days < 30)
        {
            return true;
        }
        return false;
    }

    public string Sell(Variant02_Medicine m, int q)
    {
        if (m.D < DateTime.Now)
        {
            return "bad";
        }

        if (m.Q < q)
        {
            return "bad";
        }

        m.Q = m.Q - q;

        Console.WriteLine("Sold " + m.N + " " + q + " items, left = " + m.Q);
        return "ok";
    }
}
