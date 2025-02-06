using Spectre.Console;
using BibliotecaLeader.Controllers;
using static BibliotecaLeader.Enums;
using BibliotecaLeader.Data;

namespace BibliotecaLeader;

internal class UserInterface
{
    private readonly BooksController _booksController;
    private readonly UsersController _usersController;
    private readonly LoansController _loansController;

    // Modificato per accettare il Database Context
    public UserInterface(BooksController booksController, UsersController usersController, LoansController loansController)
    {
        _booksController = booksController;
        _usersController = usersController;
        _loansController = loansController;
    }

    internal void MainMenu()
    {
        while (true)
        {
            Console.Clear();

            var actionChoice = AnsiConsole.Prompt(
                new SelectionPrompt<MainMenuOptions>()
                .Title("[yellow]Benvenuto nel gestionale della Biblioteca Leader![/]\n" +
                    "[blue]Per una migliore esperienza consigliamo di allargare la finestra in full screen.[/]\n" +
                    "[magenta]Seleziona un'opzione:[/]")
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
                    ManageUsersMenu();
                    break;
                case MainMenuOptions.Loans:
                    ManageLoansMenu();
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
                    FilterBooksMenu();
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

    private void FilterBooksMenu()
    {
        var filterBookAction = AnsiConsole.Prompt(
            new SelectionPrompt<FilterBooksOptions>()
            .Title("Scegli se vuoi mostrare tutti i libri o preferisci filtrarli:")
            .AddChoices(Enum.GetValues<FilterBooksOptions>())
            .UseConverter(option => option switch
            {
                FilterBooksOptions.ShowAll => "📖 Mostra Tutti",
                FilterBooksOptions.FilterByAuthor => "👨 Filtra per Autore",
                FilterBooksOptions.FilterByTitle => "📚 Filtra per Titolo",
                FilterBooksOptions.FilterByYear => "📅 Filtra per Anno",
                FilterBooksOptions.Back => "⬅️ Torna Indietro",
                _ => throw new ArgumentOutOfRangeException()
            }));

        switch (filterBookAction)
        {
            case FilterBooksOptions.ShowAll:
                _booksController.ViewBooks();
                break;
            case FilterBooksOptions.FilterByAuthor:
                string author = AnsiConsole.Ask<string>("Inserisci il nome dell'autore:");
                _booksController.ViewBooks("Author", author);
                break;
            case FilterBooksOptions.FilterByTitle:
                string title = AnsiConsole.Ask<string>("Inserisci il titolo del libro:");
                _booksController.ViewBooks("Title", title);
                break;
            case FilterBooksOptions.FilterByYear:
                string year = AnsiConsole.Ask<string>("Inserisci l'anno di pubblicazione:");
                _booksController.ViewBooks("Year", year);
                break;
            case FilterBooksOptions.Back:
                return;
        }
    }

    private void ManageUsersMenu()
    {
        while (true)
        {
            Console.Clear();

            var userAction = AnsiConsole.Prompt(
                new SelectionPrompt<UserMenuOptions>()
                .Title("Seleziona un'opzione per la gestione degli utenti:")
                .AddChoices(Enum.GetValues<UserMenuOptions>())
                .UseConverter(option => option switch
                {
                    UserMenuOptions.ViewUsers => "📖 Visualizza Utenti",
                    UserMenuOptions.AddUser => "➕ Aggiungi Utente",
                    UserMenuOptions.EditUser => "✏️ Modifica Utente",
                    UserMenuOptions.DeleteUser => "🗑️ Elimina Utente",
                    UserMenuOptions.Back => "⬅️ Torna Indietro",
                    _ => throw new ArgumentOutOfRangeException()
                }));

            switch (userAction)
            {
                case UserMenuOptions.ViewUsers:
                    FilterUsersMenu();
                    break;
                case UserMenuOptions.AddUser:
                    _usersController.AddUser();
                    break;
                case UserMenuOptions.EditUser:
                    _usersController.EditUser();
                    break;
                case UserMenuOptions.DeleteUser:
                    _usersController.DeleteUser();
                    break;
                case UserMenuOptions.Back:
                    return;
            }
        }
    }

    private void FilterUsersMenu()
    {
        var filterUserAction = AnsiConsole.Prompt(
            new SelectionPrompt<FilterUsersOptions>()
            .Title("Scegli se vuoi mostrare tutti gli utenti o preferisci filtrarli:")
            .AddChoices(Enum.GetValues<FilterUsersOptions>())
            .UseConverter(option => option switch
            {
                FilterUsersOptions.ShowAll => "👥 Mostra Tutti",
                FilterUsersOptions.FilterByName => "👤 Filtra per Nome e Cognome",
                FilterUsersOptions.FilterByTaxCode => "🆔 Filtra per Codice Fiscale",
                FilterUsersOptions.FilterByEmail => "📧 Filtra per Email", 
                FilterUsersOptions.Back => "⬅️ Torna Indietro",
                _ => throw new ArgumentOutOfRangeException()
            }));

        switch (filterUserAction)
        {
            case FilterUsersOptions.ShowAll:
                _usersController.ViewUsers();
                break;
            case FilterUsersOptions.FilterByName:
                string name = AnsiConsole.Ask<string>("Inserisci il nome e/o il cognome dell'utente:");
                _usersController.ViewUsers("Name", name);
                break;
            case FilterUsersOptions.FilterByTaxCode:
                string taxCode = AnsiConsole.Ask<string>("Inserisci il codice fiscale dell'utente:");
                _usersController.ViewUsers("Tax Code", taxCode);
                break;
            case FilterUsersOptions.FilterByEmail:
                string email = AnsiConsole.Ask<string>("Inserisci l'indirizzo email dell'utente:");
                _usersController.ViewUsers("Email", email);
                break;
            case FilterUsersOptions.Back:
                return;
        }
    }

    private void ManageLoansMenu()
    {
        while (true)
        {
            Console.Clear();

            var loanAction = AnsiConsole.Prompt(
                new SelectionPrompt<LoanMenuOptions>()
                .Title("Seleziona un'opzione per la gestione dei prestiti:")
                .AddChoices(Enum.GetValues<LoanMenuOptions>())
                .UseConverter(option => option switch
                {
                    LoanMenuOptions.ViewLoans => "📖 Visualizza Prestiti",
                    LoanMenuOptions.NewLoan => "➕ Nuovo Prestito",
                    LoanMenuOptions.EditLoan => "✏️ Modifica Prestito",
                    LoanMenuOptions.EndLoan => "🔚 Termina Prestito",
                    LoanMenuOptions.DeleteLoan => "🗑️ Elimina Prestito",
                    LoanMenuOptions.Back => "⬅️ Torna Indietro",
                    _ => throw new ArgumentOutOfRangeException()
                }));

            switch (loanAction)
            {
                case LoanMenuOptions.ViewLoans:
                    FilterLoansMenu();
                    break;
                case LoanMenuOptions.NewLoan:
                    _loansController.NewLoan();
                    break;
                case LoanMenuOptions.EditLoan:
                    _loansController.EditLoan();
                    break;
                case LoanMenuOptions.EndLoan:
                    _loansController.EndLoan();
                    break;
                case LoanMenuOptions.DeleteLoan:
                    _loansController.DeleteLoan();
                    break;
                case LoanMenuOptions.Back:
                    return;
            }
        }
    }

    private void FilterLoansMenu()
    {
        var filterLoansAction = AnsiConsole.Prompt(
            new SelectionPrompt<FilterLoansOptions>()
            .Title("Scegli se vuoi mostrare tutti gli utenti o preferisci filtrarli:")
            .AddChoices(Enum.GetValues<FilterLoansOptions>())
            .UseConverter(option => option switch
            {
                FilterLoansOptions.ShowAll => "👥 Mostra Tutti",
                FilterLoansOptions.FilterByIsActive => "🔍 Filtra per Attivi",
                FilterLoansOptions.FilterByUserId => "👤 Filtra per ID utente",
                FilterLoansOptions.Back => "⬅️ Torna Indietro",
                _ => throw new ArgumentOutOfRangeException()
            }));

        switch (filterLoansAction)
        {
            case FilterLoansOptions.ShowAll:
                _loansController.ViewLoans();
                break;
            case FilterLoansOptions.FilterByIsActive:
                _loansController.ViewLoans("IsActive");
                break;
            case FilterLoansOptions.FilterByUserId:
                string userId = AnsiConsole.Ask<string>("Inserisci l'ID dell'utente:");
                _loansController.ViewLoans("UserId", userId);
                break;
            case FilterLoansOptions.Back:
                return;
        }
    }
}