namespace ReviewSamples.Modules.Variants;

public class Variant17_Tour
{
    public string Country;
    public int Nights;
    public double P;
}

public class Variant17_AgencyBad
{
    private List<Variant17_Tour> tours = new();

    public Variant17_AgencyBad()
    {
        tours.Add(new Variant17_Tour { Country = "Turkey", Nights = 7, P = 50000 });
        tours.Add(new Variant17_Tour { Country = "Egypt", Nights = 10, P = 70000 });
        tours.Add(new Variant17_Tour { Country = "UAE", Nights = 5, P = 90000 });
    }

    public List<Variant17_Tour> Find(double max, int min)
    {
        var res = new List<Variant17_Tour>();

        for (int i = 0; i < tours.Count; i++)
        {
            if (tours[i].P <= max && tours[i].Nights >= min)
            {
                res.Add(tours[i]);
            }
        }

        Console.WriteLine("Found " + res.Count + " tours");
        return res;
    }
}
