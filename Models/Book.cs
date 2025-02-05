using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BibliotecaLeader.Models;

public class Book
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }  // ID gestito automaticamente dal database

    [Required]
    public string Title { get; set; } = string.Empty;

    public int Year { get; set; }

    [Required]
    public string Authors { get; set; } = string.Empty;

    [Required]
    public string Publisher { get; set; } = string.Empty;

    [Required]
    public string Genre { get; set; } = string.Empty;

    [Required]
    public string Language { get; set; } = string.Empty;

    public int Quantity { get; set; }

    public int Availability { get; set; }  // Il controllo è demandato al database

    [Required]
    public string Edition { get; set; } = string.Empty;

    // Costruttore vuoto richiesto da Entity Framework
    public Book() { }
}
