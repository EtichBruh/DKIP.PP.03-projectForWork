namespace ReviewSamples.Modules.Variants;

public enum BookStatus {
    Available,
    Issued
}

public class Variant01_BookFixed {
    public string Title { get; }
    public string Author { get; }
    public string Isbn { get; }
    public BookStatus Status { get; private set; }

    public Variant01_BookFixed(string title, string author, string isbn) {
        Title = title ?? throw new ArgumentNullException(nameof(title));
        Author = author ?? throw new ArgumentNullException(nameof(author));
        Isbn = isbn ?? throw new ArgumentNullException(nameof(isbn));
        Status = BookStatus.Available;
    }

    public void MarkIssued() {
        if (Status == BookStatus.Issued)
            throw new InvalidOperationException("Книга уже выдана.");
        Status = BookStatus.Issued;
    }

    public void MarkAvailable() {
        if (Status == BookStatus.Available)
            throw new InvalidOperationException("Книга не была выдана.");
        Status = BookStatus.Available;
    }
}

public class Variant01_ReaderFixed {
    public string Name { get; }
    public int CardNumber { get; }

    public Variant01_ReaderFixed(string name, int cardNumber) {
        Name = name ?? throw new ArgumentNullException(nameof(name));
        CardNumber = cardNumber > 0 ? cardNumber
            : throw new ArgumentOutOfRangeException(nameof(cardNumber), "Номер читательского билета должен быть положительным.");
    }
}

public record IssueRecord(string Isbn, DateTime DueDate);

public class Variant01_LibraryFixed {
    private const decimal FinePerDay = 10m;
    private readonly List<Variant01_BookFixed> _books = new();
    private readonly Dictionary<int, IssueRecord> _issued = new();

    public void AddBook(Variant01_BookFixed book) {
        ArgumentNullException.ThrowIfNull(book);
        _books.Add(book);
    }

    public IssueRecord Issue(Variant01_ReaderFixed reader, Variant01_BookFixed book, int days) {
        ArgumentNullException.ThrowIfNull(reader);
        ArgumentNullException.ThrowIfNull(book);
        if (days <= 0)
            throw new ArgumentOutOfRangeException(nameof(days), "Количество дней должно быть положительным.");

        book.MarkIssued();
        var dueDate = DateTime.Now.Date.AddDays(days);
        var record = new IssueRecord(book.Isbn, dueDate);
        _issued[reader.CardNumber] = record;
        return record;
    }

    public decimal Return(Variant01_ReaderFixed reader, Variant01_BookFixed book) {
        ArgumentNullException.ThrowIfNull(reader);
        ArgumentNullException.ThrowIfNull(book);

        book.MarkAvailable();

        if (!_issued.TryGetValue(reader.CardNumber, out var record))
            throw new InvalidOperationException("Запись о выдаче не найдена для данного читателя.");

        _issued.Remove(reader.CardNumber);

        if (DateTime.Now.Date > record.DueDate) {
            var overdueDays = (DateTime.Now.Date - record.DueDate).Days;
            var fine = overdueDays * FinePerDay;
            Console.WriteLine($"Просрочка {overdueDays} дн., штраф = {fine:F2} руб.");
            return fine;
        }

        return 0;
    }
}
