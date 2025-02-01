using Spectre.Console;
using BibliotecaLeader.Models;

namespace BibliotecaLeader.Controllers;

internal class BooksController
{
    internal void ViewBooks(string? filterType = null, string? filterValue = null)
    {
        var table = new Table();
        table.Border(TableBorder.Rounded);
        table.AddColumn("[yellow]ID[/]");
        table.AddColumn("[yellow]Titolo[/]");
        table.AddColumn("[yellow]Anno[/]");
        table.AddColumn("[yellow]Autori[/]");
        table.AddColumn("[yellow]Editore[/]");
        table.AddColumn("[yellow]Genere[/]");
        table.AddColumn("[yellow]Lingua[/]");
        table.AddColumn("[yellow]Quantità[/]");
        table.AddColumn("[yellow]Disponibilità[/]");
        table.AddColumn("[yellow]Edizione[/]");

        var books = MockDatabase.Books.OfType<Book>();

        // Applica il filtro se presente
        if (!string.IsNullOrEmpty(filterType) && !string.IsNullOrEmpty(filterValue))
        {
            books = filterType switch
            {
                "Author" => books.Where(b => b.Authors.Contains(filterValue, StringComparison.OrdinalIgnoreCase)),
                "Title" => books.Where(b => b.Title.Contains(filterValue, StringComparison.OrdinalIgnoreCase)),
                "Year" => books.Where(b => b.Year.ToString() == filterValue),
                _ => books
            };
        }

        foreach (var book in books)
        {
            table.AddRow(
                book.BookId,
                $"[magenta3_1]{book.Title}[/]",
                $"[blue]{book.Year}[/]",
                $"[magenta3_1]{book.Authors}[/]",
                $"[blue]{book.Publisher}[/]",
                $"[blue]{book.Genre}[/]",
                $"[blue]{book.Language}[/]",
                book.Quantity == 0 ? $"[red3]{book.Quantity}[/]" : $"[green]{book.Quantity}[/]",
                book.Availability == 0 ? "[red3]Non Disponibile[/]" : $"[green]{book.Availability}[/]",
                $"[blue]{book.Edition}[/]"
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

    internal void AddBook()
    {
        var newBook = GetBookData();
        MockDatabase.Books.Add(newBook);
        AnsiConsole.MarkupLine("[green]📘 Libro aggiunto con successo![/]");
        AnsiConsole.MarkupLine("[gray]Premi un tasto per continuare...[/]");
        Console.ReadKey(true);
    }

    internal void EditBook()
    {
        var bookId = AnsiConsole.Ask<string>("Inserisci l'ID del libro da modificare:");
        var book = MockDatabase.Books.FirstOrDefault(b => b.BookId == bookId);

        if (book == null)
        {
            AnsiConsole.MarkupLine("[red]Libro non trovato![/]");
            return;
        }

        var updatedBook = GetBookData(book);
        updatedBook.BookId = book.BookId;
        MockDatabase.Books.Remove(book);
        MockDatabase.Books.Add(updatedBook);
        AnsiConsole.MarkupLine("[green]✏️ Libro modificato con successo![/]");
        AnsiConsole.MarkupLine("[gray]Premi un tasto per continuare...[/]");
        Console.ReadKey(true);
    }

    internal void DeleteBook()
    {
        AnsiConsole.MarkupLine("[bold]🗑️ Elimina Libro[/]");
    }

    private Book GetBookData(Book? existingBook = null)
    {
        string title = AnsiConsole.Ask<string>("Inserisci il titolo del libro:" + (existingBook != null ? $" [gray](attuale: {existingBook.Title})[/]" : ""));
        int year = AnsiConsole.Ask<int>("Inserisci l'anno di pubblicazione:" + (existingBook != null ? $" [gray](attuale: {existingBook.Year})[/]" : ""));
        string authors = AnsiConsole.Ask<string>("Inserisci gli autori:" + (existingBook != null ? $" [gray](attuale: {existingBook.Authors})[/]" : ""));
        string publisher = AnsiConsole.Ask<string>("Inserisci l'editore:" + (existingBook != null ? $" [gray](attuale: {existingBook.Publisher})[/]" : ""));
        string genre = AnsiConsole.Ask<string>("Inserisci il genere:" + (existingBook != null ? $" [gray](attuale: {existingBook.Genre})[/]" : ""));
        string language = AnsiConsole.Ask<string>("Inserisci la lingua:" + (existingBook != null ? $" [gray](attuale: {existingBook.Language})[/]" : ""));
        int quantity = AnsiConsole.Ask<int>("Inserisci la quantità:" + (existingBook != null ? $" [gray](attuale: {existingBook.Quantity})[/]" : ""));
        int availability = AnsiConsole.Ask<int>("Inserisci la disponibilità:" + (existingBook != null ? $" [gray](attuale: {existingBook.Availability})[/]" : ""));
        string edition = AnsiConsole.Ask<string>("Inserisci l'edizione:" + (existingBook != null ? $" [gray](attuale: {existingBook.Edition})[/]" : ""));

        var newBook = new Book(
            title,
            year,
            authors,
            publisher,
            genre,
            language,
            quantity,
            availability,
            edition
        );

        return newBook;
    }
}