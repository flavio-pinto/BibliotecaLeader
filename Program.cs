using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using BibliotecaLeader;
using BibliotecaLeader.Data;
using BibliotecaLeader.Controllers;

var serviceProvider = new ServiceCollection()
    .AddDbContext<BibliotecaContext>(options =>
        options.UseSqlServer("Server=localhost;Database=biblioteca_leader;Trusted_Connection=True;TrustServerCertificate=True;"))
    .BuildServiceProvider();

using var scope = serviceProvider.CreateScope();
var dbContext = scope.ServiceProvider.GetRequiredService<BibliotecaContext>();

// Applica eventuali migrazioni pendenti
dbContext.Database.Migrate();

// Crea i controller
var booksController = new BooksController(dbContext);
var usersController = new UsersController(dbContext);
var loansController = new LoansController(dbContext);

// Avvia l'interfaccia utente
var userInterface = new UserInterface(dbContext);
userInterface.MainMenu();
