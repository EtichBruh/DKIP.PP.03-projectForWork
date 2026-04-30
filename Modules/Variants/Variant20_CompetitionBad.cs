namespace ReviewSamples.Modules.Variants;

public class Variant20_Athlete
{
    public string N;
    public int Year;
    public string Sex;
}

public class Variant20_CompetitionBad
{
    private List<Variant20_Athlete> reg = new();

    public string Register(Variant20_Athlete a, string discipline)
    {
        var age = DateTime.Now.Year - a.Year;

        if (discipline == "junior")
        {
            if (age > 18) return "bad";
        }
        else if (discipline == "adult")
        {
            if (age < 18) return "bad";
            if (age > 60) return "bad";
        }
        else if (discipline == "veteran")
        {
            if (age < 60) return "bad";
        }

        reg.Add(a);
        Console.WriteLine("Registered " + a.N + ", age " + age);
        return "ok";
    }
}
