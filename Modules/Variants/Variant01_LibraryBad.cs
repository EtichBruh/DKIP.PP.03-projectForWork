namespace ReviewSamples.Modules.Variants;

public class Variant01_Book
{
    public string T;
    public string A;
    public string I;
    public bool On;
}

public class Variant01_Reader
{
    public string N;
    public int C;
}

public class Variant01_LibraryBad
{
    private List<Variant01_Book> books = new();
    private Dictionary<int, string> issued = new();

    public string Issue(Variant01_Reader r, Variant01_Book b, int d)
    {
        if (b.On == true)
        {
            return "bad";
        }

        b.On = true;
        var dt = DateTime.Now.AddDays(d);
        issued[r.C] = b.I + ";" + dt.ToString();

        Console.WriteLine("Issued " + b.T + " to " + r.N + " for " + d + " days");
        return "ok";
    }

    public string Return(Variant01_Reader r, Variant01_Book b)
    {
        if (b.On == false)
        {
            return "bad";
        }

        b.On = false;
        var rec = issued[r.C];
        var parts = rec.Split(";");
        var due = DateTime.Parse(parts[1]);

        if (DateTime.Now > due)
        {
            var days = (DateTime.Now - due).Days;
            Console.WriteLine("Overdue by " + days + " days, fine = " + days * 10);
        }

        return "ok";
    }
}
