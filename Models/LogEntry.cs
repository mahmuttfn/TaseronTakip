using System.ComponentModel.DataAnnotations;

namespace TaseronTakip.Models;

public class LogEntry
{
    public int Id { get; set; }
    [MaxLength(100)]
    public string? Kullanici { get; set; }
    [MaxLength(60)]
    public string? Eylem { get; set; }     // Örn: Create/Update/Delete/Login/Approve
    [MaxLength(100)]
    public string? Nesne { get; set; }     // Örn: Firma#12, Bolum#5
    public DateTime Zaman { get; set; } = DateTime.UtcNow;
    [MaxLength(1000)]
    public string? Detay { get; set; }
}
