namespace ReviewSamples.Modules.Variants;

public class Variant03_Grade
{
    public string Sub;
    public int Val;
}

public class Variant03_Student
{
    public string N;
    public List<Variant03_Grade> Gs = new();
}

public class Variant03_JournalBad
{
    public string Add(Variant03_Student s, string sub, int v)
    {
        if (v > 5 || v < 1)
        {
            return "bad";
        }

        s.Gs.Add(new Variant03_Grade { Sub = sub, Val = v });
        return "ok";
    }

    public int Avg(Variant03_Student s, string sub)
    {
        int sum = 0;
        int n = 0;

        for (int i = 0; i < s.Gs.Count; i++)
        {
            if (s.Gs[i].Sub == sub)
            {
                sum = sum + s.Gs[i].Val;
                n = n + 1;
            }
        }

        return sum / n;
    }
}
