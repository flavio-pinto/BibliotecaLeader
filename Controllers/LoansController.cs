﻿using Spectre.Console;
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

    public void EndLoan()
    {
        var loanId = AnsiConsole.Ask<string>("Inserisci l'ID del prestito da terminare:");
        var loan = MockDatabase.Loans.FirstOrDefault(l => l.LoanId == loanId);

        if (loan == null)
        {
            AnsiConsole.MarkupLine("[red]Prestito non trovato![/]");
            return;
        }

        var user = MockDatabase.Users.FirstOrDefault(u => u.UserId == loan.UserId);
        var book = MockDatabase.Books.FirstOrDefault(b => b.BookId == loan.BookId);

        if (user == null || book == null)
        {
            AnsiConsole.MarkupLine("[red]Errore: utente o libro non trovati.[/]");
            return;
        }

        AnsiConsole.MarkupLine($"[green]Confermi la restituzione del libro '{book.Title}' da parte di {user.FirstName} {user.LastName}? (S/N)[/]");
        var confirmation = Console.ReadLine()?.Trim().ToLower();
        if (confirmation != "s")
        {
            AnsiConsole.MarkupLine("[yellow]Operazione annullata.[/]");
            return;
        }

        // Aggiorna la data di restituzione
        loan.ReturnDate = DateTime.Now;

        // Calcola la penale se la restituzione è in ritardo
        if (loan.ReturnDate > loan.EndDate)
        {
            var overdueDays = (loan.ReturnDate - loan.EndDate)?.Days ?? 0;
            loan.Penalty = overdueDays * 2;
            AnsiConsole.MarkupLine($"[red]Il prestito è in ritardo di {overdueDays} giorni. Penale applicata: {loan.Penalty}€.[/]");
        }
        else
        {
            loan.Penalty = 0;
        }

        // Aumenta la disponibilità del libro
        book.Availability++;

        AnsiConsole.MarkupLine("[green]✅ Restituzione completata con successo![/]");
        AnsiConsole.MarkupLine("[gray]Premi un tasto per continuare...[/]");
        Console.ReadKey(true);
    }


    internal void DeleteLoan()
    {
        var loanId = AnsiConsole.Ask<string>("Inserisci l'ID del prestito da eliminare:");
        var loan = MockDatabase.Loans.FirstOrDefault(l => l.LoanId == loanId);

        if (loan == null)
        {
            AnsiConsole.MarkupLine("[red]Prestito non trovato![/]");
            return;
        }

        var user = MockDatabase.Users.FirstOrDefault(u => u.UserId == loan.UserId);
        var book = MockDatabase.Books.FirstOrDefault(b => b.BookId == loan.BookId);

        if (user == null || book == null)
        {
            AnsiConsole.MarkupLine("[red]Errore: utente o libro associato non trovati.[/]");
            return;
        }

        // Mostra dettagli del prestito e chiede conferma prima di eliminare
        AnsiConsole.MarkupLine($"[yellow]Confermi l'eliminazione del prestito?[/]");
        AnsiConsole.MarkupLine($"📚 [bold]Libro:[/] {book.Title}");
        AnsiConsole.MarkupLine($"👤 [bold]Utente:[/] {user.FirstName} {user.LastName}");
        AnsiConsole.MarkupLine($"📅 [bold]Data inizio:[/] {loan.StartDate:dd/MM/yyyy}");
        AnsiConsole.MarkupLine($"📅 [bold]Scadenza:[/] {loan.EndDate:dd/MM/yyyy}");
        AnsiConsole.MarkupLine($"📅 [bold]Restituzione:[/] {(loan.ReturnDate?.ToString("dd/MM/yyyy") ?? "[red]Non restituito[/]")}");

        var confirmation = AnsiConsole.Confirm("[bold red]Sei sicuro di voler eliminare questo prestito?[/]");

        if (!confirmation)
        {
            AnsiConsole.MarkupLine("[yellow]Operazione annullata.[/]");
            return;
        }

        // Se il prestito non è stato restituito, aumenta la disponibilità del libro
        if (loan.ReturnDate == null)
        {
            book.Availability++;
            AnsiConsole.MarkupLine($"[yellow]📖 La disponibilità del libro '{book.Title}' è stata aumentata a {book.Availability}.[/]");
        }

        // Eliminazione del prestito dalla lista
        MockDatabase.Loans.Remove(loan);
        AnsiConsole.MarkupLine("[green]🗑️ Prestito eliminato con successo![/]");
        AnsiConsole.MarkupLine("[gray]Premi un tasto per continuare...[/]");
        Console.ReadKey(true);
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