namespace ReviewSamples.Modules.Variants;

public class Variant24_Exercise
{
    public string Type;
    public double KcalPerMin;
}

public class Variant24_WorkoutLogBad
{
    private List<(string T, int M, double K)> log = new();

    public void Add(Variant24_Exercise ex, int min)
    {
        var k = ex.KcalPerMin * min;
        log.Add((ex.Type, min, k));
        Console.WriteLine("Added " + ex.Type + " for " + min + " min, " + k + " kcal");
    }

    public double Total()
    {
        double sum = 0;
        for (int i = 0; i < log.Count; i++)
        {
            sum = sum + log[i].K;
        }
        return sum;
    }
}
