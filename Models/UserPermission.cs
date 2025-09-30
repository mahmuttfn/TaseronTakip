using System.ComponentModel.DataAnnotations;

public class UserPermission
{
    public int Id { get; set; }
    public int UserId { get; set; }

    [Required, MaxLength(40)]
    public string MenuKey { get; set; } = "";

    public bool CanView { get; set; }
    public bool CanAct { get; set; }
}
