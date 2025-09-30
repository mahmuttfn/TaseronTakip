using System.ComponentModel.DataAnnotations;

public class AppUser
{
    public int Id { get; set; }

    [Required, MaxLength(150)]
    public string FullName { get; set; } = "";

    [Required, EmailAddress, MaxLength(200)]
    public string Email { get; set; } = "";

    [Required, MaxLength(50)]
    public string UserName { get; set; } = "";

    // Demo için plain tutuluyor; gerçek projede hash kullan
    [Required, MaxLength(100)]
    public string TempPassword { get; set; } = "";

    [Required, MaxLength(30)]
    public string Role { get; set; } = "Kullanıcı";

    public bool IsActive { get; set; } = true;
}
