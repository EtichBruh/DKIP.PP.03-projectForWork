namespace ReviewSamples.Modules;

public record StatisticsResult(double Average, int Min, int Max, int Count);

public class StatisticsFixed
{
    public StatisticsResult Calculate(IEnumerable<int> numbers)
    {
        if (numbers is null)
            throw new ArgumentNullException(nameof(numbers));

        var values = numbers.ToList();

        if (values.Count == 0)
            throw new ArgumentException("Список чисел не должен быть пустым.", nameof(numbers));

        return new StatisticsResult(
            Average: values.Average(),
            Min: values.Min(),
            Max: values.Max(),
            Count: values.Count);
    }
}
