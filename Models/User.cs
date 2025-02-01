namespace BibliotecaLeader.Models;

internal class User
{
    private static int _nextId = 1;

    static User()
    {
        if (MockDatabase.Users != null && MockDatabase.Users.Any())
        {
            _nextId = MockDatabase.Users.Max(b => int.Parse(b.UserId)) + 1;
        }
    }

    public string UserId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime BirthDate { get; set; }
    public string TaxCode { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string Address { get; set; }
    public string City { get; set; }
    public string Province { get; set; }

    public User(string firstName, string lastName, DateTime birthDate, string taxCode, string email, string phone, string address, string city, string province)
    {
        UserId = _nextId.ToString();
        _nextId++;
        FirstName = firstName;
        LastName = lastName;
        BirthDate = birthDate;
        TaxCode = taxCode;
        Email = email;
        Phone = phone;
        Address = address;
        City = city;
        Province = province;
    }
}