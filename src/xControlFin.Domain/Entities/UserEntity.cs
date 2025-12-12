using xControlFin.Domain.Entities.Base;

namespace xControlFin.Domain.Entities;

public class UserEntity : BaseEntity
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string? Image { get; set; }
    public bool Active { get; set; }

    public string Password { get; set; }
    public string? RefreshToken { get; set; }
    public DateTime? RefreshTokenExpiryTime { get; set; }
}