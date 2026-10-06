using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodHub.BlazorShared.Dto
{
    public class TokenResultDto
    {
        #nullable disable

        
        public string AccessToken { get; init; } = string.Empty;

        public string RefreshToken { get; init; } = string.Empty;

        public DateTime AccessTokenExpire { get; init; }

        public DateTime RefreshTokenExpire { get; init; }

    }
}
