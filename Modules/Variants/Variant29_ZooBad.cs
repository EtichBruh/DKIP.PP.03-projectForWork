namespace ReviewSamples.Modules.Variants;

public class Variant29_Animal
{
    public string Species;
    public string N;
    public string Diet;
}

public class Variant29_Feeding
{
    public Variant29_Animal A;
    public DateTime Time;
    public double Portion;
}

public class Variant29_ZooBad
{
    private List<Variant29_Animal> animals = new();
    private List<Variant29_Feeding> schedule = new();

    public string AddAnimal(Variant29_Animal a)
    {
        for (int i = 0; i < animals.Count; i++)
        {
            if (animals[i].N == a.N)
            {
                return "bad";
            }
        }
        animals.Add(a);
        return "ok";
    }

    public string Feed(Variant29_Animal a, DateTime time)
    {
        double portion = 0;

        if (a.Species == "lion")
        {
            portion = 5;
        }
        else if (a.Species == "tiger")
        {
            portion = 4;
        }
        else if (a.Species == "wolf")
        {
            portion = 2;
        }

        schedule.Add(new Variant29_Feeding { A = a, Time = time, Portion = portion });
        Console.WriteLine("Fed " + a.N + " (" + a.Species + ") with " + portion + "kg");
        return "ok";
    }
}
