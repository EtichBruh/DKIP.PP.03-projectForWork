namespace ReviewSamples.Modules.Variants;

public class Variant08_Dish
{
    public string N;
    public double P;
}

public class Variant08_OrderItem
{
    public Variant08_Dish D;
    public int Q;
}

public class Variant08_Order
{
    public int Table;
    public List<Variant08_OrderItem> Items = new();
}

public class Variant08_RestaurantBad
{
    public void AddDish(Variant08_Order o, Variant08_Dish d, int q)
    {
        o.Items.Add(new Variant08_OrderItem { D = d, Q = q });
    }

    public double Total(Variant08_Order o)
    {
        double sum = 0;
        for (int i = 0; i < o.Items.Count; i++)
        {
            sum = sum + o.Items[i].D.P * o.Items[i].Q;
        }

        sum = sum + sum * 0.1;

        Console.WriteLine("Table " + o.Table + " total = " + sum);
        return sum;
    }
}
