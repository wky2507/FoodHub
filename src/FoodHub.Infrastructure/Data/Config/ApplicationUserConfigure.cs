using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Identity.Client;
using FoodHub.Domain.Entity.Identity;
namespace FoodHub.Infrastructure.Data.Config;
public class ApplicationUserConfigure :IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder) {
        //升级一下变唯一索引，但是先不执行数据库迁移，只在后端做手机号的存在检查
        builder.HasIndex(u => u.PhoneNumber).IsUnique();
                   
        
        //var nav1 = builder.Metadata.FindNavigation(nameof(ApplicationUser.MerchantApplications));

        //nav1?.SetPropertyAccessMode(PropertyAccessMode.Field);

        //var nav2 = builder.Metadata.FindNavigation(nameof(ApplicationUser.Store));

        //nav2?.SetPropertyAccessMode(PropertyAccessMode.Field);

        
    }
     
}
