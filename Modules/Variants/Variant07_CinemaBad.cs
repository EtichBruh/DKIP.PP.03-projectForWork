namespace ReviewSamples.Modules.Variants;

public class Variant07_Showtime
{
    public string Movie;
    public string Rating;
    public DateTime Time;
    public bool[,] Seats = new bool[10, 20];
}

public class Variant07_BoxOfficeBad
{
    public string Buy(Variant07_Showtime s, int row, int seat, string cat, int age)
    {
        if (s.Seats[row, seat] == true)
        {
            return "bad";
        }

        double price = 300;

        if (cat == "child")
        {
            price = price * 0.5;
            if (s.Rating == "18+")
            {
                return "bad";
            }
        }
        else if (cat == "student")
        {
            price = price * 0.7;
        }
        else if (cat == "pensioner")
        {
            price = price * 0.6;
        }

        if (s.Time.Hour < 12)
        {
            price = price * 0.8;
        }

        s.Seats[row, seat] = true;
        Console.WriteLine("Ticket sold for " + price);
        return "ok";
    }
}
