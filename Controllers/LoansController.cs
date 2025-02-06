using BibliotecaLeader.Data;
using BibliotecaLeader.Models;
using Spectre.Console;

namespace BibliotecaLeader.Controllers;

public class LoansController
{
    private readonly LibraryContext _context;

    public LoansController(LibraryContext context)
    {
        _context = context;
    }

    public void ViewLoans(string? filterType = null, string? filterValue = null)
    {
        var table = new Table();
        table.Border(TableBorder.Rounded);
        table.AddColumn("[yellow]ID[/]");
        table.AddColumn("[yellow]Nome e Cognome[/]");
        table.AddColumn("[yellow]Titolo Libro[/]");
        table.AddColumn("[yellow]Data Inizio[/]");
        table.AddColumn("[yellow]Scadenza[/]");
        table.AddColumn("[yellow]Restituzione[/]");
        table.AddColumn("[yellow]Penale[/]");

        var loans = _context.Loans.AsQueryable();

        if (!string.IsNullOrEmpty(filterType) && !string.IsNullOrEmpty(filterValue))
        {
            loans = filterType switch
            {
                "UserId" => loans.Where(l => l.UserId.ToString() == filterValue),
                "IsActive" => loans.Where(l => l.ReturnDate == null),
                _ => loans
            };
        }

        foreach (var loan in loans)
        {
            var user = _context.Users.Find(loan.UserId);
            var book = _context.Books.Find(loan.BookId);

            var userName = user != null ? $"{user.FirstName} {user.LastName}" : "[red]Utente non trovato[/]";
            var bookTitle = book != null ? book.Title : "[red]Libro non trovato[/]";

            table.AddRow(
                loan.Id.ToString(),
                userName,
                bookTitle,
                loan.StartDate.ToString("dd/MM/yyyy"),
                loan.EndDate.ToString("dd/MM/yyyy"),
                loan.ReturnDate?.ToString("dd/MM/yyyy") ?? "[red]Non restituito[/]",
                loan.Penalty > 0 ? $"[red]{loan.Penalty}€[/]" : "[green]Nessuna Penale[/]"
            );
        }

        if (!loans.Any())
        {
            AnsiConsole.MarkupLine("[red]Nessun prestito trovato con il filtro selezionato.[/]");
        }
        else
        {
            AnsiConsole.Write(table);
        }

        Console.ReadKey();
    }

    public void NewLoan()
    {
        var newLoan = GetLoanData();
        if (newLoan == null) return;

        var book = _context.Books.Find(newLoan.BookId);
        if (book == null || book.Availability == 0)
        {
            AnsiConsole.MarkupLine("[red]Il libro non è disponibile per il prestito.[/]");
            return;
        }

        book.Availability--;
        _context.Loans.Add(newLoan);
        _context.SaveChanges();

        AnsiConsole.MarkupLine("[green]📘 Prestito aggiunto con successo![/]");
        Console.ReadKey();
    }

    public void EditLoan()
    {
        var loanId = AnsiConsole.Ask<int>("Inserisci l'ID del prestito da modificare:");
        var loan = _context.Loans.Find(loanId);

        if (loan == null)
        {
            AnsiConsole.MarkupLine("[red]Prestito non trovato![/]");
            Console.ReadKey();
            return;
        }

        var updatedLoan = GetLoanData(loan);
        if (updatedLoan == null) return;

        loan.BookId = updatedLoan.BookId;
        loan.UserId = updatedLoan.UserId;
        loan.StartDate = updatedLoan.StartDate;
        loan.EndDate = updatedLoan.EndDate;

        _context.SaveChanges();

        AnsiConsole.MarkupLine("[green]✏️ Prestito modificato con successo![/]");
        Console.ReadKey();
    }

    public void EndLoan()
    {
        var loanId = AnsiConsole.Ask<int>("Inserisci l'ID del prestito da chiudere:");
        var loan = _context.Loans.Find(loanId);

        if (loan == null)
        {
            AnsiConsole.MarkupLine("[red]Prestito non trovato![/]");
            Console.ReadKey();
            return;
        }

        var user = _context.Users.Find(loan.UserId);
        var book = _context.Books.Find(loan.BookId);

        if (user == null || book == null)
        {
            AnsiConsole.MarkupLine("[red]Errore: utente o libro non trovati.[/]");
            Console.ReadKey();
            return;
        }

        AnsiConsole.MarkupLine($"[green]Confermi la restituzione del libro '{book.Title}' da parte di {user.FirstName} {user.LastName}? (S/N)[/]");
        var confirmation = Console.ReadLine()?.Trim().ToLower();
        if (confirmation != "s")
        {
            AnsiConsole.MarkupLine("[yellow]Operazione annullata.[/]");
            Console.ReadKey();
            return;
        }

        loan.ReturnDate = DateTime.Now;
        if (loan.ReturnDate > loan.EndDate)
        {
            var overdueDays = (loan.ReturnDate - loan.EndDate).Value.Days;
            loan.Penalty = overdueDays * 2;
            AnsiConsole.MarkupLine($"[red]Il prestito è in ritardo di {overdueDays} giorni. Penale applicata: {loan.Penalty}€.[/]");
        }
        else
        {
            loan.Penalty = 0;
        }

        book.Availability++;
        _context.SaveChanges();

        AnsiConsole.MarkupLine("[green]✅ Restituzione completata con successo![/]");
        Console.ReadKey();
    }

    public void DeleteLoan()
    {
        var loanId = AnsiConsole.Ask<int>("Inserisci l'ID del prestito da eliminare:");
        var loan = _context.Loans.Find(loanId);

        if (loan == null)
        {
            AnsiConsole.MarkupLine("[red]Prestito non trovato![/]");
            Console.ReadKey();
            return;
        }

        var user = _context.Users.Find(loan.UserId);
        var book = _context.Books.Find(loan.BookId);

        if (user == null || book == null)
        {
            AnsiConsole.MarkupLine("[red]Errore: utente o libro non trovati.[/]");
            Console.ReadKey();
            return;
        }

        AnsiConsole.MarkupLine($"[yellow]Sei sicuro di voler eliminare il prestito del libro '{book.Title}' da parte di {user.FirstName} {user.LastName}? (S/N)[/]");
        var confirmation = Console.ReadLine()?.Trim().ToLower();
        if (confirmation != "s")
        {
            AnsiConsole.MarkupLine("[yellow]Operazione annullata.[/]");
            Console.ReadKey();
            return;
        }

        if (loan.ReturnDate == null)
        {
            book.Availability++;
        }

        _context.Loans.Remove(loan);
        _context.SaveChanges();

        AnsiConsole.MarkupLine("[green]🗑️ Prestito eliminato con successo![/]");
        Console.ReadKey();
    }

    private Loan? GetLoanData(Loan? existingLoan = null)
    {
        var userId = AnsiConsole.Ask<int>("Inserisci l'ID dell'utente:");
        var user = _context.Users.Find(userId);
        if (user == null)
        {
            AnsiConsole.MarkupLine("[red]Utente non trovato![/]");
            Console.ReadKey();
            return null;
        }

        var bookId = AnsiConsole.Ask<int>("Inserisci l'ID del libro:");
        var book = _context.Books.Find(bookId);
        if (book == null)
        {
            AnsiConsole.MarkupLine("[red]Libro non trovato![/]");
            Console.ReadKey();
            return null;
        }

        AnsiConsole.MarkupLine($"[green]Confermi il prestito del libro '{book.Title}' a {user.FirstName} {user.LastName}? (S/N)[/]");
        var confirmation = Console.ReadLine()?.Trim().ToLower();
        if (confirmation != "s")
        {
            AnsiConsole.MarkupLine("[yellow]Operazione annullata.[/]");
            return null;
        }

        var startDate = existingLoan?.StartDate ?? DateTime.Now;
        var endDate = existingLoan?.EndDate ?? startDate.AddDays(30);

        return new Loan { BookId = bookId, UserId = userId, StartDate = startDate, EndDate = endDate };
    }
}
