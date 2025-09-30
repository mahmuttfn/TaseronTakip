using System.ComponentModel.DataAnnotations;

namespace TaseronTakip.Models;

public class Firma
{
    public int Id { get; set; }

    [Required, MaxLength(200)]
    public string Ad { get; set; } = "";

    [MaxLength(20)]
    public string? VergiNo { get; set; }

    [MaxLength(150)]
    public string? Yetkili { get; set; }

    [MaxLength(50)]
    public string? Telefon { get; set; }

    [EmailAddress, MaxLength(200)]
    public string? Eposta { get; set; }

    [MaxLength(400)]
    public string? Adres { get; set; }

    public bool Aktif { get; set; } = true;
}
