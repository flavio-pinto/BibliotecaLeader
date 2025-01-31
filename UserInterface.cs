using Spectre.Console;
using BibliotecaLeader.Controllers;
using static BibliotecaLeader.Enums;

namespace BibliotecaLeader;

internal class UserInterface
{
    private readonly BooksController _booksController = new();

    internal void MainMenu()
    {
        while (true)
        {
            Console.Clear();

            var actionChoice = AnsiConsole.Prompt(
                new SelectionPrompt<MainMenuOptions>()
                .Title("Seleziona un'opzione:")
                .AddChoices(Enum.GetValues<MainMenuOptions>())
                .UseConverter(option => option switch
                {
                    MainMenuOptions.Books => "📚 Gestione Libri",
                    MainMenuOptions.Users => "👥 Gestione Utenti",
                    MainMenuOptions.Loans => "🔄 Gestione Prestiti",
                    MainMenuOptions.Exit => "❌ Esci",
                    _ => throw new ArgumentOutOfRangeException()
                }));

            switch (actionChoice)
            {
                case MainMenuOptions.Books:
                    ManageBooksMenu();
                    break;
                case MainMenuOptions.Users:
                    AnsiConsole.MarkupLine("[bold]👥 Gestione Utenti[/]");
                    break;
                case MainMenuOptions.Loans:
                    AnsiConsole.MarkupLine("[bold]🔄 Gestione Prestiti[/]");
                    break;
                case MainMenuOptions.Exit:
                    AnsiConsole.MarkupLine("[red]Uscita dal programma...[/]");
                    return;
            }
        }
    }

    private void ManageBooksMenu()
    {
        while (true)
        {
            Console.Clear();

            var bookAction = AnsiConsole.Prompt(
                new SelectionPrompt<BookMenuOptions>()
                .Title("Seleziona un'opzione per la gestione dei libri:")
                .AddChoices(Enum.GetValues<BookMenuOptions>())
                .UseConverter(option => option switch
                {
                    BookMenuOptions.ViewBooks => "📖 Visualizza Libri",
                    BookMenuOptions.AddBook => "➕ Aggiungi Libro",
                    BookMenuOptions.EditBook => "✏️ Modifica Libro",
                    BookMenuOptions.DeleteBook => "🗑️ Elimina Libro",
                    BookMenuOptions.Back => "⬅️ Torna Indietro",
                    _ => throw new ArgumentOutOfRangeException()
                }));

            switch (bookAction)
            {
                case BookMenuOptions.ViewBooks:
                    _booksController.ViewBooks();
                    break;
                case BookMenuOptions.AddBook:
                    _booksController.AddBook();
                    break;
                case BookMenuOptions.EditBook:
                    _booksController.EditBook();
                    break;
                case BookMenuOptions.DeleteBook:
                    _booksController.DeleteBook();
                    break;
                case BookMenuOptions.Back:
                    return;
            }
        }
    }
}