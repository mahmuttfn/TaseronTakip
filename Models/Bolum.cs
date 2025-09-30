using System.ComponentModel.DataAnnotations;

namespace TaseronTakip.Models;

public class Bolum
{
    public int Id { get; set; }

    [Required, MaxLength(150)]
    public string Ad { get; set; } = "";

    [MaxLength(30)]
    public string? KisaKod { get; set; }

    [MaxLength(150)]
    public string? Sorumlu { get; set; }

    [MaxLength(300)]
    public string? Aciklama { get; set; }

    public bool Aktif { get; set; } = true;
}
