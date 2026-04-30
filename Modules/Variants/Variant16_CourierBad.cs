namespace ReviewSamples.Modules.Variants;

public class Variant16_Parcel
{
    public double Weight;
    public string Zone;
}

public class Variant16_CourierBad
{
    public double Calc(Variant16_Parcel p)
    {
        double price = 0;

        if (p.Zone == "near")
        {
            if (p.Weight <= 1) price = 200;
            else if (p.Weight <= 5) price = 350;
            else if (p.Weight <= 20) price = 600;
            else price = 1000;
        }
        else if (p.Zone == "far")
        {
            if (p.Weight <= 1) price = 400;
            else if (p.Weight <= 5) price = 700;
            else if (p.Weight <= 20) price = 1200;
            else price = 2000;
        }

        return price;
    }
}
