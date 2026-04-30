namespace ReviewSamples.Modules;

public class StatisticsBad
{
    public int Avg(List<int> a)
    {
        int s = 0;

        for (int i = 0; i < a.Count; i++)
        {
            s = s + a[i];
        }

        return s / a.Count;
    }

    public int Max(List<int> a)
    {
        int m = 0;

        for (int i = 0; i < a.Count; i++)
        {
            if (a[i] > m)
            {
                m = a[i];
            }
        }

        return m;
    }
}
