using Spectre.Console;
using BibliotecaLeader.Models;

namespace BibliotecaLeader.Controllers;

internal class LoansController()
{
    internal void ViewLoans(string? filterType = null, string? filterValue = null)
    {
        var table = new Table();
        table.Border(TableBorder.Rounded);
        table.AddColumn("[yellow]ID[/]");
        table.AddColumn("[yellow]ID Utente[/]");
        table.AddColumn("[yellow]Nome e Cognome[/]");
        table.AddColumn("[yellow]ID Libro[/]");
        table.AddColumn("[yellow]Titolo[/]");
        table.AddColumn("[yellow]Data Inizio Prestito[/]");
        table.AddColumn("[yellow]Deadline Fine Prestito[/]");
        table.AddColumn("[yellow]Data Restituzione[/]");
        table.AddColumn("[yellow]Penale[/]");

        var loans = MockDatabase.Loans.OfType<Loan>();

        // Applica il filtro se presente
        if (!string.IsNullOrEmpty(filterType) && !string.IsNullOrEmpty(filterValue))
        {
            loans = filterType switch
            {
                "UserId" => loans.Where(l => l.UserId == filterValue),
                "IsActive" => loans.Where(l => l.ReturnDate == null),
            };
        }

        foreach (var loan in loans)
        {
            var user = MockDatabase.Users.FirstOrDefault(u => u.UserId == loan.UserId);
            var userName = user != null ? $"{user.FirstName} {user.LastName}" : "[red]Utente non trovato[/]";

            var book = MockDatabase.Books.FirstOrDefault(b => b.BookId == loan.BookId);
            var bookTitle = book != null ? book.Title : "[red]Libro non trovato[/]";

            table.AddRow(
                loan.LoanId,
                loan.UserId,
                userName,
                loan.BookId,
                bookTitle,  // Qui verrà stampato il titolo del libro
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

        AnsiConsole.MarkupLine("Premi un tasto per continuare.");
        Console.ReadKey();
    }

    internal void NewLoan()
    {
        var newLoan = GetLoanData();
        var book = MockDatabase.Books.FirstOrDefault(b => b.BookId == newLoan.BookId);

        if (book == null)
        {
            AnsiConsole.MarkupLine("[red]Libro non trovato![/]");
            return;
        }

        if (book.Availability == 0)
        {
            AnsiConsole.MarkupLine("[red]Il libro non è disponibile per il prestito.[/]");
            return;
        }

        book.Availability--;
        MockDatabase.Loans.Add(newLoan);
        AnsiConsole.MarkupLine("[green]📘 Prestito aggiunto con successo![/]");
        AnsiConsole.MarkupLine("[gray]Premi un tasto per continuare...[/]");
        Console.ReadKey(true);
    }

    internal void EditLoan()
    {
        var loanId = AnsiConsole.Ask<string>("Inserisci l'ID del prestito da modificare:");
        var loan = MockDatabase.Loans.FirstOrDefault(l => l.LoanId == loanId);

        if (loan == null)
        {
            AnsiConsole.MarkupLine("[red]Prestito non trovato![/]");
            return;
        }

        var updatedLoan = GetLoanData(loan);
        updatedLoan.LoanId = loan.LoanId;
        MockDatabase.Loans.Remove(loan);
        MockDatabase.Loans.Add(updatedLoan);
        AnsiConsole.MarkupLine("[green]✏️ Prestito modificato con successo![/]");
        AnsiConsole.MarkupLine("[gray]Premi un tasto per continuare...[/]");
        Console.ReadKey(true);
    }

    internal void EndLoan()
    {
        Console.WriteLine("Work in progress...");
    }

    internal void DeleteLoan()
    {
        Console.WriteLine("Work in progress...");
    }

    private Loan GetLoanData(Loan? existingLoan = null)
    {
        var userId = AnsiConsole.Ask<string>("Inserisci l'ID dell'utente:" + (existingLoan != null ? $" [gray](attuale: {existingLoan.UserId})[/]" : ""));
        var user = MockDatabase.Users.FirstOrDefault(u => u.UserId == userId);
        if (user == null)
        {
            AnsiConsole.MarkupLine("[red]Utente non trovato![/]");
            return null;
        }

        var bookId = AnsiConsole.Ask<string>("Inserisci l'ID del libro:" + (existingLoan != null ? $" [gray](attuale: {existingLoan.BookId})[/]" : ""));
        var book = MockDatabase.Books.FirstOrDefault(b => b.BookId == bookId);
        if (book == null)
        {
            AnsiConsole.MarkupLine("[red]Libro non trovato![/]");
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

        return new Loan(bookId, userId, startDate, endDate);
    }
}