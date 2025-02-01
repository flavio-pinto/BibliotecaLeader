namespace BibliotecaLeader.Models;

internal class Book
{
    private static int _nextId = 1;

    static Book()
    {
        if (MockDatabase.Books != null && MockDatabase.Books.Any())
        {
            _nextId = MockDatabase.Books.Max(b => int.Parse(b.BookId)) + 1;
        }
    }

    public string BookId { get; set; }
    public string Title { get; set; }
    public int Year { get; set; }
    public string Authors { get; set; }
    public string Publisher { get; set; }
    public string Genre { get; set; }
    public string Language { get; set; }
    public int Quantity { get; set; }
    private int _availability;
    public int Availability
    {
        get => _availability;
        set
        {
            if (value > Quantity)
            {
                throw new ArgumentException("La disponibilità non può essere superiore alla quantità totale.");
            }
            _availability = value;
        }
    }
    public string Edition { get; set; }

    public Book(string title, int year, string authors, string publisher, string genre, string language, int quantity, int availability, string edition)
    {
        if (availability > quantity)
        {
            throw new ArgumentException("La disponibilità non può essere superiore alla quantità totale.");
        }

        BookId = _nextId.ToString();
        _nextId++;
        Title = title;
        Year = year;
        Authors = authors;
        Publisher = publisher;
        Genre = genre;
        Language = language;
        Quantity = quantity;
        _availability = availability;
        Edition = edition;
    }
}
