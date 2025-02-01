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
        AnsiConsole.MarkupLine("[bold]➕ Aggiungi Libro[/]");
    }

    internal void EditBook()
    {
        AnsiConsole.MarkupLine("[bold]✏️ Modifica Libro[/]");
    }

    internal void DeleteBook()
    {
        AnsiConsole.MarkupLine("[bold]🗑️ Elimina Libro[/]");
    }
}