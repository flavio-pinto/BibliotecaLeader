using Spectre.Console;
using System.Text.RegularExpressions;
using BibliotecaLeader.Models;

namespace BibliotecaLeader.Controllers;

internal class UsersController
{
    internal void ViewUsers(string? filterType = null, string? filterValue = null)
    {
        var table = new Table();
        table.Border(TableBorder.Rounded);
        table.AddColumn("[yellow]ID[/]");
        table.AddColumn("[yellow]Nome e Cognome[/]");
        table.AddColumn("[yellow]Data di Nascita[/]");
        table.AddColumn("[yellow]Codice Fiscale[/]");
        table.AddColumn("[yellow]Email[/]");
        table.AddColumn("[yellow]Telefono[/]");
        table.AddColumn("[yellow]Indirizzo[/]");
        table.AddColumn("[yellow]Città[/]");
        table.AddColumn("[yellow]Provincia[/]");

        var users = MockDatabase.Users.OfType<User>();

        // Applica il filtro se presente
        if (!string.IsNullOrEmpty(filterType) && !string.IsNullOrEmpty(filterValue))
        {
            users = filterType switch
            {
                "Name" => users.Where(u =>
                {
                    var searchTerms = filterValue.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                    return searchTerms.All(term =>
                        u.FirstName.Contains(term, StringComparison.OrdinalIgnoreCase) ||
                        u.LastName.Contains(term, StringComparison.OrdinalIgnoreCase)
                    );
                }),
                "Tax Code" => users.Where(u => u.TaxCode.Contains(filterValue, StringComparison.OrdinalIgnoreCase)),
                "Email" => users.Where(u => u.Email.Contains(filterValue, StringComparison.OrdinalIgnoreCase)),
                _ => users
            };
        }

        foreach (var user in users)
        {
            table.AddRow(
                user.UserId,
                $"[magenta3_1]{$"{user.FirstName} {user.LastName}"}[/]",
                $"[blue]{user.BirthDate:dd/MM/yyyy}[/]",
                $"[blue]{user.TaxCode}[/]",
                $"[blue]{user.Email}[/]",
                $"[blue]{user.Phone}[/]",
                $"[blue]{user.Address}[/]",
                $"[blue]{user.City}[/]",
                $"[blue]{user.Province}[/]"
            );
        }

        if (!users.Any())
        {
            AnsiConsole.MarkupLine("[red]Nessun utente trovato con il filtro selezionato.[/]");
        }
        else
        {
            AnsiConsole.Write(table);
        }

        AnsiConsole.MarkupLine("Premi un tasto per continuare.");
        Console.ReadKey();
    }

    internal void AddUser()
    {
        try
        {
            var newUser = GetUserData();
            MockDatabase.Users.Add(newUser);
            AnsiConsole.MarkupLine("[green]📘 Utente aggiunto con successo![/]");
        }
        catch (ArgumentException ex)
        {
            AnsiConsole.MarkupLine($"[red]{ex.Message}[/]");
        }

        AnsiConsole.MarkupLine("[gray]Premi un tasto per continuare...[/]");
        Console.ReadKey(true);
    }

    internal void EditUser()
    {
        var userId = AnsiConsole.Ask<string>("Inserisci l'ID dell'utente da modificare:");
        var user = MockDatabase.Users.FirstOrDefault(u => u.UserId == userId);

        if (user == null)
        {
            AnsiConsole.MarkupLine("[red]Utente non trovato![/]");
            return;
        }

        try
        {
            var updatedUser = GetUserData(user);
            updatedUser.UserId = user.UserId;
            MockDatabase.Users.Remove(user);
            MockDatabase.Users.Add(updatedUser);
            AnsiConsole.MarkupLine("[green]✏️ Utente modificato con successo![/]");
        }
        catch (ArgumentException ex)
        {
            AnsiConsole.MarkupLine($"[red]{ex.Message}[/]");
        }

        AnsiConsole.MarkupLine("[gray]Premi un tasto per continuare...[/]");
        Console.ReadKey(true);
    }


    internal void DeleteUser()
    {
        Console.WriteLine("Eliminazione utente");
    }

    private User GetUserData(User? existingUser = null)
    {
        string name = AnsiConsole.Ask<string>("Inserisci il nome dell'utente:" + (existingUser != null ? $" [gray](attuale: {existingUser.FirstName})[/]" : ""));
        string lastName = AnsiConsole.Ask<string>("Inserisci il cognome dell'utente:" + (existingUser != null ? $" [gray](attuale: {existingUser.LastName})[/]" : ""));
        DateTime birthDate = AnsiConsole.Ask<DateTime>("Inserisci la data di nascita dell'utente (YYYY-MM-DD):" + (existingUser != null ? $" [gray](attuale: {existingUser.BirthDate})[/]" : ""));
        string taxCode = AnsiConsole.Ask<string>("Inserisci il codice fiscale dell'utente:" + (existingUser != null ? $" [gray](attuale: {existingUser.TaxCode})[/]" : ""));
        string email;
        while (true)
        {
            email = AnsiConsole.Ask<string>("Inserisci l'email dell'utente:" + (existingUser != null ? $" [gray](attuale: {existingUser.Email})[/]" : ""));

            if (Regex.IsMatch(email, @"^[^\s@]+@[^\s@]+\.[^\s@]+$"))
            {
                break; // Email valida, esce dal ciclo
            }

            AnsiConsole.MarkupLine("[red]Formato email non valido. Riprova.[/]");
        }
        string phone = AnsiConsole.Ask<string>("Inserisci il numero di telefono dell'utente:" + (existingUser != null ? $" [gray](attuale: {existingUser.Phone})[/]" : ""));
        string address = AnsiConsole.Ask<string>("Inserisci l'indirizzo dell'utente:" + (existingUser != null ? $" [gray](attuale: {existingUser.Address})[/]" : ""));
        string city = AnsiConsole.Ask<string>("Inserisci la città dell'utente:" + (existingUser != null ? $" [gray](attuale: {existingUser.City})[/]" : ""));
        string province = AnsiConsole.Ask<string>("Inserisci la provincia dell'utente:" + (existingUser != null ? $" [gray](attuale: {existingUser.Province})[/]" : ""));

        var newUser = new User(
            name,
            lastName,
            birthDate,
            taxCode,
            email,
            phone,
            address,
            city,
            province
        );

        return newUser;
    }
}