namespace ReviewSamples.Modules.Variants;

public class Variant22_Doctor
{
    public string N;
    public string Spec;
}

public class Variant22_Slot
{
    public Variant22_Doctor D;
    public DateTime Time;
    public string Patient;
}

public class Variant22_ClinicBad
{
    private List<Variant22_Slot> slots = new();

    public string Issue(Variant22_Doctor d, DateTime time, string patient)
    {
        for (int i = 0; i < slots.Count; i++)
        {
            if (slots[i].D.N == d.N && slots[i].Time == time)
            {
                if (slots[i].Patient != null)
                {
                    return "bad";
                }
                slots[i].Patient = patient;
                return "ok";
            }
        }

        var s = new Variant22_Slot { D = d, Time = time, Patient = patient };
        slots.Add(s);
        return "ok";
    }
}
