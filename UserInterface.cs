using Spectre.Console;

namespace BibliotecaLeader;

internal class UserInterface
{
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

            if (actionChoice == MainMenuOptions.Exit)
            {
                AnsiConsole.MarkupLine("[red]Uscita dal programma...[/]");
                break;
            }

            AnsiConsole.MarkupLine("[gray]Premi un tasto per continuare...[/]");
            Console.ReadKey(true);
        }
    }
}