using BibliotecaLeader.Data;
using BibliotecaLeader.Models;
using Spectre.Console;

namespace BibliotecaLeader.Controllers;

public class UsersController
{
    private readonly LibraryContext _context;

    public UsersController(LibraryContext context)
    {
        _context = context;
    }

    public void ViewUsers(string? filterType = null, string? filterValue = null)
    {
        var table = new Table();
        table.Border(TableBorder.Rounded);
        table.AddColumn("[yellow]ID[/]");
        table.AddColumn("[yellow]Nome[/]");
        table.AddColumn("[yellow]Cognome[/]");
        table.AddColumn("[yellow]Codice Fiscale[/]");
        table.AddColumn("[yellow]Email[/]");
        table.AddColumn("[yellow]Telefono[/]");
        table.AddColumn("[yellow]Città[/]");

        var users = _context.Users.AsQueryable();

        if (!string.IsNullOrEmpty(filterType) && !string.IsNullOrEmpty(filterValue))
        {
            users = filterType switch
            {
                "Name" => users.Where(u => u.FirstName.Contains(filterValue) || u.LastName.Contains(filterValue)),
                "Tax Code" => users.Where(u => u.TaxCode.Contains(filterValue)),
                "Email" => users.Where(u => u.Email.Contains(filterValue)),
                _ => users
            };
        }

        foreach (var user in users)
        {
            table.AddRow(
                user.Id.ToString(),
                user.FirstName,
                user.LastName,
                user.TaxCode,
                user.Email,
                user.Phone,
                user.City
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

    public void AddUser()
    {
        var firstName = AnsiConsole.Ask<string>("Inserisci il nome:");
        var lastName = AnsiConsole.Ask<string>("Inserisci il cognome:");
        var birthDate = AnsiConsole.Ask<DateTime>("Inserisci la data di nascita (yyyy-mm-dd):");
        var taxCode = AnsiConsole.Ask<string>("Inserisci il codice fiscale:");
        var email = AnsiConsole.Ask<string>("Inserisci l'email:");
        var phone = AnsiConsole.Ask<string>("Inserisci il numero di telefono:");
        var address = AnsiConsole.Ask<string>("Inserisci l'indirizzo:");
        var city = AnsiConsole.Ask<string>("Inserisci la città:");
        var province = AnsiConsole.Ask<string>("Inserisci la provincia:");

        var newUser = new User
        {
            FirstName = firstName,
            LastName = lastName,
            BirthDate = birthDate,
            TaxCode = taxCode,
            Email = email,
            Phone = phone,
            Address = address,
            City = city,
            Province = province
        };

        _context.Users.Add(newUser);
        _context.SaveChanges();

        AnsiConsole.MarkupLine("[green]👤 Utente aggiunto con successo![/]");
        Console.ReadKey();
    }

    public void EditUser()
    {
        var userId = AnsiConsole.Ask<int>("Inserisci l'ID dell'utente da modificare:");
        var user = _context.Users.Find(userId);

        if (user == null)
        {
            AnsiConsole.MarkupLine("[red]Utente non trovato![/]");
            Console.ReadKey();
            return;
        }

        user.FirstName = AnsiConsole.Ask<string>($"Nome attuale: {user.FirstName} - Inserisci nuovo nome (o lascia vuoto per mantenere):", user.FirstName);
        user.LastName = AnsiConsole.Ask<string>($"Cognome attuale: {user.LastName} - Inserisci nuovo cognome:", user.LastName);
        user.BirthDate = AnsiConsole.Ask<DateTime>($"Data di nascita attuale: {user.BirthDate:yyyy-MM-dd} - Inserisci nuova data:", user.BirthDate);
        user.TaxCode = AnsiConsole.Ask<string>($"Codice fiscale attuale: {user.TaxCode} - Inserisci nuovo codice:", user.TaxCode);
        user.Email = AnsiConsole.Ask<string>($"Email attuale: {user.Email} - Inserisci nuova email:", user.Email);
        user.Phone = AnsiConsole.Ask<string>($"Telefono attuale: {user.Phone} - Inserisci nuovo telefono:", user.Phone);
        user.Address = AnsiConsole.Ask<string>($"Indirizzo attuale: {user.Address} - Inserisci nuovo indirizzo:", user.Address);
        user.City = AnsiConsole.Ask<string>($"Città attuale: {user.City} - Inserisci nuova città:", user.City);
        user.Province = AnsiConsole.Ask<string>($"Provincia attuale: {user.Province} - Inserisci nuova provincia:", user.Province);

        _context.SaveChanges();

        AnsiConsole.MarkupLine("[green]✏️ Utente modificato con successo![/]");
        Console.ReadKey();
    }

    public void DeleteUser()
    {
        var userId = AnsiConsole.Ask<int>("Inserisci l'ID dell'utente da eliminare:");
        var user = _context.Users.Find(userId);

        if (user == null)
        {
            AnsiConsole.MarkupLine("[red]Utente non trovato![/]");
            Console.ReadKey();
            return;
        }

        AnsiConsole.MarkupLine($"[yellow]Sei sicuro di voler eliminare l'utente '{user.FirstName} {user.LastName}'? (S/N)[/]");
        var confirmation = Console.ReadLine()?.Trim().ToLower();

        if (confirmation != "s")
        {
            AnsiConsole.MarkupLine("[yellow]Operazione annullata.[/]");
            return;
        }

        _context.Users.Remove(user);
        _context.SaveChanges();

        AnsiConsole.MarkupLine("[green]🗑️ Utente eliminato con successo![/]");
        Console.ReadKey();
    }
}
