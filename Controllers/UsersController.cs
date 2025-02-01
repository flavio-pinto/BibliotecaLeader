using Spectre.Console;
using BibliotecaLeader.Models;

namespace BibliotecaLeader.Controllers;

internal class UsersController
{
    internal void ViewUsers()
    {
        Console.WriteLine("Visualizzazione utenti");
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