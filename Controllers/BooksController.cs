using Spectre.Console;
using BibliotecaLeader.Models;

namespace BibliotecaLeader.Controllers;

internal class BooksController
{
    internal void ViewBooks()
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

        foreach (var book in books)
        {
            table.AddRow(
                book.BookId,
                $"[magenta3_1]{book.Title}[/]",
                $"[magenta3_1]{book.Authors}[/]",
                $"[blue]{book.Genre}[/]",
                $"[blue]{book.Publisher}[/]",
                $"[blue]{book.Year}[/]",
                $"[blue]{book.Edition}[/]",
                $"[blue]{book.Language}[/]",
                book.Quantity == 0 ? $"[red3]{book.Quantity}[/]" : $"[green]{book.Quantity}[/]",
                book.Availability == 0 ? $"[red3]Non Disponibile[/]" : $"[green]{book.Availability}[/]"
            );
        }

        AnsiConsole.Write(table);
        AnsiConsole.MarkupLine("Press Any Key to Continue.");
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