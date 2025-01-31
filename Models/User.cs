namespace BibliotecaLeader.Models;

internal class User
{
    private static int _nextId = 1;
    private string _userId;
    private string _firstName;
    private string _lastName;
    private DateTime _birthDate;
    private string _taxCode;
    private string _email;
    private string _phone;
    private string _address;
    private string _city;
    private string _province;

    public string UserId { get => _userId; }
    public string FirstName { get => _firstName; set => _firstName = value; }
    public string LastName { get => _lastName; set => _lastName = value; }
    public DateTime BirthDate { get => _birthDate; set => _birthDate = value; }
    public string TaxCode { get => _taxCode; set => _taxCode = value; }
    public string Email { get => _email; set => _email = value; }
    public string Phone { get => _phone; set => _phone = value; }
    public string Address { get => _address; set => _address = value; }
    public string City { get => _city; set => _city = value; }
    public string Province { get => _province; set => _province = value; }

    public User(string firstName, string lastName, DateTime birthDate, string taxCode, string email, string phone, string address, string city, string province)
    {
        _userId = _nextId.ToString();
        _nextId++;
        _firstName = firstName;
        _lastName = lastName;
        _birthDate = birthDate;
        _taxCode = taxCode;
        _email = email;
        _phone = phone;
        _address = address;
        _city = city;
        _province = province;
    }
}