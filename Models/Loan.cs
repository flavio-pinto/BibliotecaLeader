namespace BibliotecaLeader.Models;

internal class Loan
{
    private static int _nextId = 1;

    static Loan()
    {
        if (MockDatabase.Loans != null && MockDatabase.Loans.Any())
        {
            _nextId = MockDatabase.Loans.Max(l => int.Parse(l.LoanId)) + 1;
        }
    }

    public string LoanId { get; set; }
    public string BookId { get; set; }
    public string UserId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public DateTime? ReturnDate { get; set; }
    public decimal Penalty { get; set; } = 0;

    public Loan(string bookId, string userId, DateTime startDate, DateTime endDate)
    {
        LoanId = _nextId.ToString();
        _nextId++;
        BookId = bookId;
        UserId = userId;
        StartDate = startDate;
        EndDate = endDate;
        ReturnDate = null;
    }
}