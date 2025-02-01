using BibliotecaLeader.Models;

namespace BibliotecaLeader;

internal static class MockDatabase
{
    public static List<Book> Books = new()
    {
        new Book("La Divina Commedia", 2000, "Dante Alighieri", "Mondadori Editore", "Thriller", "Italiano", 5, 4, "1^"),
        new Book("Le Ge Son Fè", 2001, "Fiorita Fiorano", "Scaramuzzi Editore", "Commedia", "Francese", 6, 5, "2^"),
        new Book("Flowers for Nina", 2002, "Gennaro Carozzo", "Beilibri Editore", "Fantascienza", "Inglese", 7, 6, "3^"),
        new Book("Paperino e Pippo", 2003, "Walt Disney", "Disney Libri", "Avventura", "Italiano", 8, 7, "2^"),
        new Book("Neve d'Estate", 2000, "Sasà Salvaggio", "Scaramuzzi Editore", "Thriller", "Italiano", 9, 8, "5^"),
        new Book("Cocco Bello", 2002, "Gennaro Carozzo", "Beilibri Editore", "Avventura", "Italiano", 10, 9, "1^"),
        new Book("Fantozzi", 2001, "Dante Alighieri", "Scaramuzzi Editore", "Fantascienza", "Inglese", 11, 10, "7^"),
        new Book("Bla Bla Bla", 2000, "Walt Disney", "Disney Libri", "Commedia", "Francese", 12, 11, "8^"),
        new Book("Chi Chi Chi Co Co Co", 2007, "Sasà Salvaggio", "Beilibri Editore", "Commedia", "Italiano", 13, 12, "2^"),
        new Book("Il Libro della Sardegna", 2009, "Walt Disney", "Mondadori Editore", "Thriller", "Italiano", 14, 0, "1^")
    };

    public static List<User> Users = new()
    {
        new User("Mario", "Rossi", new DateTime(1985, 5, 12), "MRORSS85M12A001Z", "mario.rossi@example.com", "1234567890", "Via Roma 1", "Milano", "MI"),
        new User("Luca", "Bianchi", new DateTime(1990, 8, 25), "LCABNC90M25A002Z", "luca.bianchi@example.com", "0987654321", "Via Verdi 2", "Roma", "RM"),
        new User("Anna", "Verdi", new DateTime(1995, 3, 30), "ANAVRD95M30A003Z", "anna.verdi@example.com", "1122334455", "Via Garibaldi 3", "Torino", "TO"),
        new User("Giovanni", "Neri", new DateTime(1980, 11, 18), "GVNNRI80M18A004Z", "giovanni.neri@example.com", "5566778899", "Via Dante 4", "Bari", "BA"),
        new User("Chiara", "Gialli", new DateTime(2000, 6, 5), "CHIGLL00M05A005Z", "chiara.gialli@example.com", "6677889900", "Via Mazzini 5", "Milano", "MI"),
        new User("Stefano", "Blu", new DateTime(1993, 9, 22), "STFBLU93M22A006Z", "stefano.blu@example.com", "7788990011", "Via Rossini 6", "Firenze", "FI"),
        new User("Elena", "Viola", new DateTime(1987, 7, 14), "ELNVIO87M14A007Z", "elena.viola@example.com", "8899001122", "Via Leopardi 7", "Bari", "BA"),
        new User("Paolo", "Marrone", new DateTime(1992, 12, 3), "PLMRRN92M03A008Z", "paolo.marrone@example.com", "9900112233", "Via Foscolo 8", "Roma", "RM"),
        new User("Sara", "Azzurri", new DateTime(1989, 4, 28), "SRAZZR89M28A009Z", "sara.azzurri@example.com", "1011121314", "Via Petrarca 9", "Bari", "BA"),
        new User("Riccardo", "Grigi", new DateTime(1997, 2, 17), "RCCGRG97M17A010Z", "riccardo.grigi@example.com", "1516171819", "Via Manzoni 10", "Roma", "RM")
    };

    public static List<Loan> Loans = new()
    {
        new Loan("1", "1", DateTime.Now.AddDays(-10), DateTime.Now.AddDays(20), 0),
        new Loan("2", "2", DateTime.Now.AddDays(-15), DateTime.Now.AddDays(10), 0),
        new Loan("3", "3", DateTime.Now.AddDays(-5), DateTime.Now.AddDays(25), 0),
        new Loan("4", "4", DateTime.Now.AddDays(-20), DateTime.Now.AddDays(5), 0),
        new Loan("5", "5", DateTime.Now.AddDays(-8), DateTime.Now.AddDays(15), 0),
        new Loan("6", "6", DateTime.Now.AddDays(-3), DateTime.Now.AddDays(30), 0),
        new Loan("7", "7", DateTime.Now.AddDays(-12), DateTime.Now.AddDays(18), 0),
        new Loan("8", "8", DateTime.Now.AddDays(-7), DateTime.Now.AddDays(22), 0),
        new Loan("9", "9", DateTime.Now.AddDays(-9), DateTime.Now.AddDays(12), 0),
        new Loan("10", "10", DateTime.Now.AddDays(-6), DateTime.Now.AddDays(28), 0)
    };
}
