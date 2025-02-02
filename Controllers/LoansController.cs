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
        Console.WriteLine("Work in progress...");
    }

    internal void EditLoan()
    {
        Console.WriteLine("Work in progress...");
    }

    internal void EndLoan()
    {
        Console.WriteLine("Work in progress...");
    }

    internal void DeleteLoan()
    {
        Console.WriteLine("Work in progress...");
    }
}