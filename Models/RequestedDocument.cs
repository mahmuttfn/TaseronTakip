namespace TaseronTakip.Models;

public class RequestedDocument
{
    public int Id { get; set; }
    public int AppointmentRequestId { get; set; }
    public string? Tip { get; set; }
    public string? Not { get; set; }
}
