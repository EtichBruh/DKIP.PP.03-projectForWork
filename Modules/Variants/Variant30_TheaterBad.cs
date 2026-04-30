namespace ReviewSamples.Modules.Variants;

public class Variant30_Performance
{
    public string Title;
    public DateTime Time;
    public bool[,] Seats = new bool[15, 25];
}

public class Variant30_TheaterBad
{
    public double Buy(Variant30_Performance p, int row, int seat, string cat)
    {
        if (row < 0 || row >= 15)
        {
            return -1;
        }

        if (seat < 0 || seat >= 25)
        {
            return -1;
        }

        if (p.Seats[row, seat] == true)
        {
            return -1;
        }

        double price = 0;

        if (cat == "parter")
        {
            price = 2000;
        }
        else if (cat == "balcony")
        {
            price = 1200;
        }
        else if (cat == "loge")
        {
            price = 3500;
        }

        if (row < 5)
        {
            price = price + 500;
        }

        p.Seats[row, seat] = true;
        return price;
    }
}
