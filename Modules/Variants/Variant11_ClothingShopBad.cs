namespace ReviewSamples.Modules.Variants;

public class Variant11_Item
{
    public string N;
    public double P;
    public int Q;
}

public class Variant11_Cart
{
    public List<Variant11_Item> Items = new();
}

public class Variant11_ShopBad
{
    public double Total(Variant11_Cart c, string type)
    {
        double sum = 0;

        for (int i = 0; i < c.Items.Count; i++)
        {
            sum = sum + c.Items[i].P * c.Items[i].Q;
        }

        if (type == "regular")
        {
            sum = sum - sum * 0.05;
        }
        else if (type == "vip")
        {
            sum = sum - sum * 0.15;
        }
        else if (type == "gold")
        {
            sum = sum - sum * 0.2;
        }

        if (sum > 5000)
        {
            sum = sum - 200;
        }

        return sum;
    }
}
