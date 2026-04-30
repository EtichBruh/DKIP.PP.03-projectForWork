namespace ReviewSamples.Modules.Variants;

public class Variant26_Gear
{
    public string N;
    public double Weight;
}

public class Variant26_Hiker
{
    public string N;
    public List<Variant26_Gear> Gear = new();
}

public class Variant26_HikingBad
{
    public string AddGear(Variant26_Hiker h, Variant26_Gear g)
    {
        double total = 0;
        for (int i = 0; i < h.Gear.Count; i++)
        {
            total = total + h.Gear[i].Weight;
        }

        if (total + g.Weight > 25)
        {
            return "bad";
        }

        h.Gear.Add(g);
        return "ok";
    }

    public double Total(Variant26_Hiker h)
    {
        double sum = 0;
        for (int i = 0; i < h.Gear.Count; i++)
        {
            sum = sum + h.Gear[i].Weight;
        }
        return sum;
    }
}
