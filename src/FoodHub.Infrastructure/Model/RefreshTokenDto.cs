
#nullable disable
namespace FoodHub.Infrastructure.Model;

public class RefreshTokenDto
{
    public string RefreshToken { get; init; } = string.Empty;

    public TokenStorageMode TokenStorageMode { get; init; }

}
