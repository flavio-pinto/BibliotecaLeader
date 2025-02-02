namespace BibliotecaLeader.Models;

internal class Loan
{
    private static int _nextId = 1;
    private string _loanId;
    private string _bookId;
    private string _userId;
    private DateTime _startDate;
    private DateTime _endDate;
    private DateTime? _returnDate = null;
    private decimal _penalty;

    public string LoanId { get => _loanId; }
    public string BookId { get => _bookId; set => _bookId = value; }
    public string UserId { get => _userId; set => _userId = value; }
    public DateTime StartDate { get => _startDate; set => _startDate = value; }
    public DateTime EndDate { get => _endDate; set => _endDate = value; }
    public DateTime? ReturnDate { get => _returnDate; set => _returnDate = value; }
    public decimal Penalty { get => _penalty; set => _penalty = value; }

    public Loan(string bookId, string userId, DateTime startDate, DateTime endDate, decimal penalty)
    {
        _loanId = _nextId.ToString();
        _nextId++;
        _bookId = bookId;
        _userId = userId;
        _startDate = startDate;
        _endDate = endDate;
        _returnDate = DateTime.MinValue;
        _penalty = penalty;
    }
}
