using TaxCRM.Entities.Common;

namespace TaxCRM.Entities.Users;

public class User : Entity
{
    public string? FirstName { get; set; }
    public string? LastName { get; set;}
    public string? Email { get; set; }
    public UserRole Role { get; init; }
}

public enum UserRole
{
    Editor,
    Entrepreneur,
    Admin
}
