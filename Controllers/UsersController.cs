using Spectre.Console;
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
                $"[blue]{user.BirthDate}[/]",
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
        Console.WriteLine("Aggiunta utente");
    }

    internal void EditUser()
    {
        Console.WriteLine("Modifica utente");
    }

    internal void DeleteUser()
    {
        Console.WriteLine("Eliminazione utente");
    }
}