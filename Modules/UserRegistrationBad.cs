namespace ReviewSamples.Modules;

public class UserRegistrationBad
{
    public string Reg(string l, string p, string e)
    {
        if (l == "")
        {
            return "bad";
        }

        if (p.Length < 3)
        {
            return "bad";
        }

        Console.WriteLine("User: " + l);
        Console.WriteLine("Password: " + p);

        if (!e.Contains("@"))
        {
            return "bad";
        }

        return "ok";
    }
}
