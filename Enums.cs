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

    internal enum UserMenuOptions
    {
        ViewUsers,
        AddUser,
        EditUser,
        DeleteUser,
        Back
    }

    internal enum FilterUsersOptions
    {
        ShowAll,
        FilterByName,
        FilterByTaxCode,
        FilterByEmail,
        Back
    }
}