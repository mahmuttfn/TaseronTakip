namespace TaseronTakip.Models;

public class AppointmentRequest
{
    public int Id { get; set; }
    public string? Firma { get; set; }
    public string? IlgiliKisi { get; set; }
    public string? Email { get; set; }
    public string? IletisimNo { get; set; }
    public DateTime? ZiyaretTarihi { get; set; }
    public TimeSpan? ZiyaretSaati { get; set; }
    public string? Aciklama { get; set; }
    public DateTime CreatedAt { get; set; }
}
