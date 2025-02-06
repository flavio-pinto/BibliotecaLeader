using BibliotecaLeader.Data;
using BibliotecaLeader.Models;
using Spectre.Console;

namespace BibliotecaLeader.Controllers;

public class BooksController
{
    private readonly LibraryContext _context;

    public BooksController(LibraryContext context)
    {
        _context = context;
    }

    public void ViewBooks(string? filterType = null, string? filterValue = null)
    {
        var table = new Table();
        table.Border(TableBorder.Rounded);
        table.AddColumn("[yellow]ID[/]");
        table.AddColumn("[yellow]Titolo[/]");
        table.AddColumn("[yellow]Autori[/]");
        table.AddColumn("[yellow]Anno[/]");
        table.AddColumn("[yellow]Genere[/]");
        table.AddColumn("[yellow]Lingua[/]");
        table.AddColumn("[yellow]Disponibilità[/]");

        var books = _context.Books.AsQueryable();

        if (!string.IsNullOrEmpty(filterType) && !string.IsNullOrEmpty(filterValue))
        {
            books = filterType switch
            {
                "Author" => books.Where(b => b.Authors.Contains(filterValue)),
                "Title" => books.Where(b => b.Title.Contains(filterValue)),
                "Year" => books.Where(b => b.Year.ToString() == filterValue),
                _ => books
            };
        }

        foreach (var book in books)
        {
            table.AddRow(
                book.Id.ToString(),
                book.Title,
                book.Authors,
                book.Year.ToString(),
                book.Genre,
                book.Language,
                book.Availability.ToString()
            );
        }

        if (!books.Any())
        {
            AnsiConsole.MarkupLine("[red]Nessun libro trovato con il filtro selezionato.[/]");
        }
        else
        {
            AnsiConsole.Write(table);
        }

        AnsiConsole.MarkupLine("Premi un tasto per continuare.");
        Console.ReadKey();
    }

    public void AddBook()
    {
        var title = AnsiConsole.Ask<string>("Inserisci il titolo:");
        var authors = AnsiConsole.Ask<string>("Inserisci gli autori:");
        var year = AnsiConsole.Ask<int>("Inserisci l'anno di pubblicazione:");
        var publisher = AnsiConsole.Ask<string>("Inserisci l'editore:");
        var genre = AnsiConsole.Ask<string>("Inserisci il genere:");
        var language = AnsiConsole.Ask<string>("Inserisci la lingua:");
        var quantity = AnsiConsole.Ask<int>("Inserisci la quantità disponibile:");
        var edition = AnsiConsole.Ask<string>("Inserisci l'edizione:");

        var newBook = new Book
        {
            Title = title,
            Year = year,
            Authors = authors,
            Publisher = publisher,
            Genre = genre,
            Language = language,
            Quantity = quantity,
            Availability = quantity, // La disponibilità iniziale è uguale alla quantità totale
            Edition = edition
        };

        _context.Books.Add(newBook);
        _context.SaveChanges();

        AnsiConsole.MarkupLine("[green]📚 Libro aggiunto con successo![/]");
        Console.ReadKey();
    }

    public void EditBook()
    {
        var bookId = AnsiConsole.Ask<int>("Inserisci l'ID del libro da modificare:");
        var book = _context.Books.Find(bookId);

        if (book == null)
        {
            AnsiConsole.MarkupLine("[red]Libro non trovato![/]");
            Console.ReadKey();
            return;
        }

        book.Title = AnsiConsole.Ask<string>($"Titolo attuale: {book.Title} - Inserisci nuovo titolo (o lascia vuoto per mantenere):", book.Title);
        book.Authors = AnsiConsole.Ask<string>($"Autori attuali: {book.Authors} - Inserisci nuovi autori:", book.Authors);
        book.Year = AnsiConsole.Ask<int>($"Anno attuale: {book.Year} - Inserisci nuovo anno:", book.Year);
        book.Publisher = AnsiConsole.Ask<string>($"Editore attuale: {book.Publisher} - Inserisci nuovo editore:", book.Publisher);
        book.Genre = AnsiConsole.Ask<string>($"Genere attuale: {book.Genre} - Inserisci nuovo genere:", book.Genre);
        book.Language = AnsiConsole.Ask<string>($"Lingua attuale: {book.Language} - Inserisci nuova lingua:", book.Language);
        book.Quantity = AnsiConsole.Ask<int>($"Quantità attuale: {book.Quantity} - Inserisci nuova quantità:", book.Quantity);
        book.Availability = AnsiConsole.Ask<int>($"Disponibilità attuale: {book.Availability} - Inserisci nuova disponibilità:", book.Availability);
        book.Edition = AnsiConsole.Ask<string>($"Edizione attuale: {book.Edition} - Inserisci nuova edizione:", book.Edition);

        _context.SaveChanges();

        AnsiConsole.MarkupLine("[green]✏️ Libro modificato con successo![/]");
        Console.ReadKey();
    }

    public void DeleteBook()
    {
        var bookId = AnsiConsole.Ask<int>("Inserisci l'ID del libro da eliminare:");
        var book = _context.Books.Find(bookId);

        if (book == null)
        {
            AnsiConsole.MarkupLine("[red]Libro non trovato![/]");
            Console.ReadKey();
            return;
        }

        AnsiConsole.MarkupLine($"[yellow]Sei sicuro di voler eliminare il libro '{book.Title}'? (S/N)[/]");
        var confirmation = Console.ReadLine()?.Trim().ToLower();

        if (confirmation != "s")
        {
            AnsiConsole.MarkupLine("[yellow]Operazione annullata.[/]");
            return;
        }

        _context.Books.Remove(book);
        _context.SaveChanges();

        AnsiConsole.MarkupLine("[green]🗑️ Libro eliminato con successo![/]");
        Console.ReadKey();
    }
}
