using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FoodHub.Domain;
using FoodHub.Domain.Entity.Identity;
namespace FoodHub.Infrastructure.Data
{
    public class UserNotFoundException :Exception
    {

        public UserNotFoundException(ApplicationUser user)
            : base($"No user found with username:{user.UserName}")
        { }

    }
}
