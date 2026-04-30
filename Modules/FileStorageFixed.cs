namespace ReviewSamples.Modules;

public class FileStorageFixed
{
    public void Save(string path, string content)
    {
        if (string.IsNullOrWhiteSpace(path))
            throw new ArgumentException("Путь к файлу не указан.", nameof(path));

        var directory = Path.GetDirectoryName(path);

        if (!string.IsNullOrWhiteSpace(directory))
            Directory.CreateDirectory(directory);

        File.WriteAllText(path, content ?? string.Empty);
    }

    public string Load(string path)
    {
        if (string.IsNullOrWhiteSpace(path))
            throw new ArgumentException("Путь к файлу не указан.", nameof(path));

        if (!File.Exists(path))
            return string.Empty;

        try
        {
            return File.ReadAllText(path);
        }
        catch (IOException ex)
        {
            throw new InvalidOperationException("Не удалось прочитать файл.", ex);
        }
    }
}
