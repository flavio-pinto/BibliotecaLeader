namespace BibliotecaLeader.Models;

internal class Book
{
    private static int _nextId = 1;

    static Book()
    {
        if (MockDatabase.Books.Any())
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
    public int Availability { get; set; }
    public string Edition { get; set; }

    public Book(string title, int year, string authors, string publisher, string genre, string language, int quantity, int availability, string edition)
    {
        BookId = _nextId.ToString();
        _nextId++;
        Title = title;
        Year = year;
        Authors = authors;
        Publisher = publisher;
        Genre = genre;
        Language = language;
        Quantity = quantity;
        Availability = availability;
        Edition = edition;
    }
}
