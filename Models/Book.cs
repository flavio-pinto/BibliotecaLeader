namespace BibliotecaLeader.Models;

namespace BibliotecaLeader;

internal class Book
{
    private static int _nextId = 1;
    private string _bookId;
    private string _title;
    private int _year;
    private string _authors;
    private string _publisher;
    private string _genre;
    private string _language;
    private int _quantity;
    private int _availability;
    private string _edition;

    public string BookId { get => _bookId; }
    public string Title { get => _title; set => _title = value; }
    public int Year { get => _year; set => _year = value; }
    public string Authors { get => _authors; set => _authors = value; }
    public string Publisher { get => _publisher; set => _publisher = value; }
    public string Genre { get => _genre; set => _genre = value; }
    public string Language { get => _language; set => _language = value; }
    public int Quantity { get => _quantity; set => _quantity = value; }
    public int Availability { get => _availability; set => _availability = value; }
    public string Edition { get => _edition; set => _edition = value; }

    public Book(string title, int year, string authors, string publisher, string genre, string language, int quantity, int availability, string edition)
    {
        _bookId = _nextId.ToString();
        _nextId++;
        _title = title;
        _year = year;
        _authors = authors;
        _publisher = publisher;
        _genre = genre;
        _language = language;
        _quantity = quantity;
        _availability = availability;
        _edition = edition;
    }
}