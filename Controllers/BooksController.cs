using Spectre.Console;
using BibliotecaLeader.Models;

namespace BibliotecaLeader.Controllers;

internal class BooksController
{
    internal void ViewBooks()
    {
        AnsiConsole.MarkupLine("[bold]📖 Visualizza Libri[/]");
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