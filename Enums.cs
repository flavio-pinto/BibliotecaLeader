namespace BibliotecaLeader;

internal class Enums
{
    internal enum MainMenuOptions
    {
        Books = 1,
        Users,
        Loans,
        Exit
    }

    internal enum BookMenuOptions
    {
        ViewBooks,
        AddBook,
        EditBook,
        DeleteBook,
        Back
    }

    internal enum FilterBooksOptions
    {
        ShowAll,
        FilterByAuthor,
        FilterByTitle,
        FilterByYear,
        Back
    }
}