using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BibliotecaLeader.Models;

public class Loan
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    public int BookId { get; set; }

    [Required]
    public int UserId { get; set; }

    [ForeignKey("BookId")]
    public Book? Book { get; set; }

    [ForeignKey("UserId")]
    public User? User { get; set; }

    [Required]
    public DateTime StartDate { get; set; }

    [Required]
    public DateTime EndDate { get; set; }

    public DateTime? ReturnDate { get; set; }

    public decimal Penalty { get; set; } = 0;

    // Costruttore vuoto richiesto da Entity Framework
    public Loan() { }
}
