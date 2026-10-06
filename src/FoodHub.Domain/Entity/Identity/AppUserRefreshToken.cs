using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FoodHub.Domain.Interface;
namespace FoodHub.Domain.Entity.Identity
{
    public class AppUserRefreshToken : BaseEntity , IAggregateRoot
    {

        private AppUserRefreshToken() { }

        public AppUserRefreshToken(string userId,string refreshToken,bool isRevoked, DateTime createAt, DateTime expiresAt){
            
            UserId = userId;

            RefreshToken = refreshToken;

            IsRevoked = isRevoked;

            CreatedAt = createAt;

            ExpiresAt = expiresAt;

        }

        public string UserId { get;init; } = default!;

        public string RefreshToken { get; private set; } = string.Empty;

        public DateTime ExpiresAt { get;private set; }

        public DateTime CreatedAt { get;private set; }

        public DateTime? RevokedAt { get;private set; }

        public bool IsRevoked { get;private set; }

        public string? CreatedByIp { get;private set; }

        public string? RevokedByIp { get;private set; }

        public string? DeviceName { get;private set; }

        public string? UserAgent { get;private set; }

        public ApplicationUser? ApplicationUser { get; private set; }

     
        public void Revoke(bool isRevoke,DateTime dateTime) {
            
            IsRevoked = isRevoke;

            RevokedAt = dateTime;
        }


       
        }

    }

