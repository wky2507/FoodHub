using System.Security.Claims;
using System.Text.Json;
using System.IdentityModel.Tokens.Jwt;
namespace FoodHub.Blazor.Client.Security;
public class JwtParser
{
    
    public static IEnumerable<Claim> ParseClaimsFromJwt(string token)
    {
        var claims = new List<Claim>();

        if (string.IsNullOrEmpty(token)) return claims;

        try
        {
            var parts = token.Split('.');
            if (parts.Length < 2) return claims;

            var payload = parts[1];
            var jsonBytes = ParseBase64WithoutPadding(payload);
            var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);

            if (keyValuePairs != null)
            {
                foreach (var kvp in keyValuePairs)
                {

                    var claimType = Map(kvp.Key);
                    // 特殊处理：如果角色（role）是数组形式（多个角色）
                    if (kvp.Value is JsonElement element && element.ValueKind == JsonValueKind.Array)
                    {
                        foreach (var item in element.EnumerateArray())
                        {
                            claims.Add(new Claim(claimType, item.ToString() ?? ""));
                        }
                    }
                    else
                    {
                        // 普通键值对直接转 Claim
                        claims.Add(new Claim(claimType, kvp.Value.ToString() ?? string.Empty));
                    }
                }
            }
        }
        catch
        {
            // 解析异常返回空列表
        }

        return claims;
    }

    /// <summary>
    /// 3. 【核心补齐方法】免除第三方库，手动补齐 Base64 的 '=' 号
    /// </summary>
    private static byte[] ParseBase64WithoutPadding(string base64)
    {
        // Base64 字符串的长度必须是 4 的倍数，缺几位就补几个 '='
        switch (base64.Length % 4)
        {
            case 2: base64 += "=="; break;
            case 3: base64 += "="; break;
        }
        return Convert.FromBase64String(base64);
    }


    private static string Map(string claimType) {

        return claimType switch
        {


            JwtRegisteredClaimNames.Sub
            => ClaimTypes.NameIdentifier,

            JwtRegisteredClaimNames.UniqueName
            => ClaimTypes.Name,

            JwtRegisteredClaimNames.Email
            => ClaimTypes.Email,

            "role"
            => ClaimTypes.Role,

            _ => claimType

        };


    
    
    
    }


}