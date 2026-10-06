using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using FoodHub.Domain.Entity.Identity;
using FoodHub.Domain.Entity.MerchantApplicationAggregate;
using FoodHub.Domain.Interface;
using FoodHub.Domain.Entity.BuyerAggregate;
using FoodHub.Domain.Entity.OrderAggregate;
using FoodHub.Domain.Specifications;
namespace FoodHub.Infrastructure.Data
{
    public class AppIdentityDbContextSeed
    {
        public static async Task SeedAsync(AppIdentityDbContext identityDbContext,UserManager<ApplicationUser> userManager,RoleManager<IdentityRole> roleManager,IRepository<MerchantApplication> MerchantApplicationRepository,IRepository<Buyer> buyerManager,IRepository<Order> orderManager,IRepository<Product> productRepository)
        {
            //这里是清数据库，然后重建用
            //identityDbContext.UserRoles.RemoveRange(identityDbContext.UserRoles);

            //identityDbContext.UserClaims.RemoveRange(identityDbContext.UserClaims);

            //identityDbContext.UserLogins.RemoveRange(identityDbContext.UserLogins);

            //identityDbContext.Users.RemoveRange(identityDbContext.Users);

            //identityDbContext.Roles.RemoveRange(identityDbContext.Roles);

            //identityDbContext.RoleClaims.RemoveRange(identityDbContext.RoleClaims);

            //identityDbContext.UserTokens.RemoveRange(identityDbContext.UserTokens);

            //await identityDbContext.SaveChangesAsync();

            if (identityDbContext.Database.IsSqlServer()){

                identityDbContext.Database.Migrate();

            }
            if (!await roleManager.RoleExistsAsync(RoleConstants.Roles.ADMINISTRATORS))
            {
                var res = await roleManager.CreateAsync(new IdentityRole(RoleConstants.Roles.ADMINISTRATORS));
            }
            if (!await roleManager.RoleExistsAsync(RoleConstants.Roles.APPLICANT))
            {
                await roleManager.CreateAsync(new IdentityRole(RoleConstants.Roles.APPLICANT));
            }
            if (!await roleManager.RoleExistsAsync(RoleConstants.Roles.USER))
            {
                await roleManager.CreateAsync(new IdentityRole(RoleConstants.Roles.USER));
            }
            if (!await roleManager.RoleExistsAsync(RoleConstants.Roles.MERCHANT))
            {
                await roleManager.CreateAsync(new IdentityRole(RoleConstants.Roles.MERCHANT));
            }

            var existAdmin = await userManager.FindByNameAsync("adminName");

            if (existAdmin == null)
            {
                existAdmin = new ApplicationUser { UserName = "adminName", Email = "admin@outlook.com" };

                await userManager.CreateAsync(existAdmin, AuthorizationConstants.DEFAULT_PASSWORD);

            }
            if (!await userManager.IsInRoleAsync(existAdmin, RoleConstants.Roles.ADMINISTRATORS)) {

                await userManager.AddToRoleAsync(existAdmin, RoleConstants.Roles.ADMINISTRATORS);

            }

            var existUser = await userManager.FindByNameAsync("test123");

            if (existUser == null)
            {
                var testUser = new ApplicationUser { UserName = "test123", Email = "test@outlook.com" };

                const string Password = "@Wky20030105";

                await userManager.CreateAsync(testUser, Password);

               
            }

            existUser = await userManager.FindByNameAsync("test123");

            if (existUser == null) { return; }

            if (!await userManager.IsInRoleAsync(existUser, RoleConstants.Roles.USER)) {

                await userManager.AddToRoleAsync(existUser, RoleConstants.Roles.USER);

            }

            var testUser1 = await userManager.FindByNameAsync("testUser1");

            if (testUser1 == null) {

                 testUser1 = new ApplicationUser { UserName = "testUser1" };

                 const string Password = "@Wky20030105";

                await userManager.CreateAsync(testUser1, Password);


            }

            if (!await userManager.IsInRoleAsync(testUser1, RoleConstants.Roles.USER)) {

                await userManager.AddToRoleAsync(testUser1, RoleConstants.Roles.USER);

            }

            var testUser2 = await userManager.FindByNameAsync("testUser2");

            if (testUser2 == null)
            {

                testUser2 = new ApplicationUser { UserName = "testUser2" };

                const string Password = "@Wky20030105";

                await userManager.CreateAsync(testUser2, Password);


            }

            if (!await userManager.IsInRoleAsync(testUser2, RoleConstants.Roles.USER))
            {

                await userManager.AddToRoleAsync(testUser2, RoleConstants.Roles.USER);

            }

            //Console.WriteLine("注入申请测试数据,从这里开始");

            //var apply1 = new MerchantApplication("725eddbb-938b-41f7-b398-b113eea9a199", "麦香面馆", "上海市浦东新区世纪大道100号", "07724581", "/uploads/licenses/49e8ce0f-a40f-4cbb-adf7-65e5e3e7b772.png", ApplicationStatus.Pending, DateTime.UtcNow);

            //apply1.AddContact("张三", "13597204581", "210102199110180347", ContactPersonRoles.Operations);

            //var apply2 = new MerchantApplication("725eddbb-938b-41f7-b398-b113eea9a199", "东北大油边烧烤", "广州市天河区体育西路66号", "07724581", "/uploads/licenses/49e8ce0f-a40f-4cbb-adf7-65e5e3e7b772.png", ApplicationStatus.Pending, DateTime.UtcNow);

            //apply2.AddContact("何晨阳", "13176612672", "370102200206059214", ContactPersonRoles.LegalRepresentative);

            //var apply3 = new MerchantApplication("725eddbb-938b-41f7-b398-b113eea9a199", "兰州拉面", "山东省济南市历下区泉城路街道金汇商业中心401室(泉城路77号)", "07724581", "/uploads/licenses/49e8ce0f-a40f-4cbb-adf7-65e5e3e7b772.png", ApplicationStatus.Pending, DateTime.UtcNow);

            //apply3.AddContact("张旺", "17398419401", "420111200002148123", ContactPersonRoles.Finance);

            //var apply4 = new MerchantApplication("725eddbb-938b-41f7-b398-b113eea9a199", "袁记云饺", "福建省厦门市湖里区禾山街道亿华科创中心708(枋湖东路88号)", "07724581", "/uploads/licenses/49e8ce0f-a40f-4cbb-adf7-65e5e3e7b772.png", ApplicationStatus.Pending, DateTime.UtcNow);

            //apply4.AddContact("汪涵", "17255669945", "330108199701235716", ContactPersonRoles.LegalRepresentative);

            //var apply5 = new MerchantApplication("725eddbb-938b-41f7-b398-b113eea9a199", "蜜雪冰城", "河南省郑州市金水区花园路街道国贸新领地1106(花园路39号) ", "07724581", "/uploads/licenses/49e8ce0f-a40f-4cbb-adf7-65e5e3e7b772.png", ApplicationStatus.Pending, DateTime.UtcNow);

            //apply5.AddContact("何玉玲", "17589356112", "310101199508152461", ContactPersonRoles.LegalRepresentative);

            //var apply6 = new MerchantApplication("725eddbb-938b-41f7-b398-b113eea9a199", "巴蜀鸡公煲", "四川省成都市双流区华阳街道城南时代广场5楼(华府大道一段123号) ", "07724581", "/uploads/licenses/49e8ce0f-a40f-4cbb-adf7-65e5e3e7b772.png", ApplicationStatus.Pending, DateTime.UtcNow);

            //apply6.AddContact("衣驰", "19106652335", "440106198812203578", ContactPersonRoles.LegalRepresentative);

            //var apply7 = new MerchantApplication("725eddbb-938b-41f7-b398-b113eea9a199", "黄焖鸡", "广东省深圳市宝安区西乡街道汇智产业园B栋3层(固戍二路21号) ", "07724581", "/uploads/licenses/49e8ce0f-a40f-4cbb-adf7-65e5e3e7b772.png", ApplicationStatus.Pending, DateTime.UtcNow);

            //apply7.AddContact("董义军", "17598650099", "320583199205094627", ContactPersonRoles.LegalRepresentative);

            //var apply8 = new MerchantApplication("725eddbb-938b-41f7-b398-b113eea9a199", "遵义牛肉粉", "浙江省杭州市余杭区仓前街道海创园A座802(文一西路998号) ", "07724581", "/uploads/licenses/49e8ce0f-a40f-4cbb-adf7-65e5e3e7b772.png", ApplicationStatus.Pending, DateTime.UtcNow);

            //apply8.AddContact("陈昌毅", "18799065432", "440106198812203578", ContactPersonRoles.LegalRepresentative);

            //var apply9 = new MerchantApplication("725eddbb-938b-41f7-b398-b113eea9a199", "川渝小炒", "湖北省武汉市洪山区关山街道光谷软件园C6栋204(关山大道105号) ", "07724581", " / uploads/licenses/49e8ce0f-a40f-4cbb-adf7-65e5e3e7b772.png", ApplicationStatus.Pending, DateTime.UtcNow);

            //apply9.AddContact("莫不凡", "17699087654", "110101199003071234", ContactPersonRoles.LegalRepresentative);

            //var apply10 = new MerchantApplication("725eddbb-938b-41f7-b398-b113eea9a199", "香辣美蛙", "湖北省武汉市洪山区关山街道光谷软件园C6栋204(关山大道105号) ", "07724581", "/uploads/licenses/49e8ce0f-a40f-4cbb-adf7-65e5e3e7b772.png", ApplicationStatus.Pending, DateTime.UtcNow);

            //apply10.AddContact("陈其美", "19978654370", "610102198609307932", ContactPersonRoles.LegalRepresentative);

            //var apply11 = new MerchantApplication("725eddbb-938b-41f7-b398-b113eea9a199", "内蒙烤羊腿", "山东省济南市历下区泉城路街道金汇商业中心401室(泉城路77号)", "07724581", "/uploads/licenses/49e8ce0f-a40f-4cbb-adf7-65e5e3e7b772.png", ApplicationStatus.Pending, DateTime.UtcNow);

            //apply11.AddContact("蒋百里", "18366707564", "320583199205094627", ContactPersonRoles.LegalRepresentative);

            //var apply12 = new MerchantApplication("725eddbb-938b-41f7-b398-b113eea9a199", "渤海大闸蟹", "江苏省苏州市昆山市玉山镇前进中路恒泰商务大厦1205室(前进中路168号) ", "07724581", "/uploads/licenses/49e8ce0f-a40f-4cbb-adf7-65e5e3e7b772.png", ApplicationStatus.Pending, DateTime.UtcNow);

            //apply12.AddContact("何齐", "13409748961", "440106198812203578", ContactPersonRoles.LegalRepresentative);

            //var list = new List<MerchantApplication>();

            //list.AddRange(new[] { apply1, apply2, apply3, apply4, apply5, apply6, apply7, apply8, apply9, apply10, apply11, apply12 });

            //try
            //{

            //    await MerchantApplicationRepository.AddRangeAsync(list);

            //    Console.WriteLine("这里测试数据注入成功了!");
            //}
            //catch (Exception ex)
            //{

            //    Console.WriteLine(ex.Message);

            //}

            //Console.WriteLine("注入顾客");

            //var buyer1 = new Buyer( "李湘", "13178263562");

            //var buyer2 = new Buyer( "吴彦成", "13455632879");

            //var buyer3 = new Buyer( "何莉仁", "13588740975");

            //var buyerList = new List<Buyer>();

            //buyerList.AddRange(new[] { buyer1, buyer2, buyer3 });

            //await buyerManager.AddRangeAsync(buyerList);

            //Console.WriteLine("注入订单项和订单");

            //var order1 = new Order("FH202609160001",new DateTimeOffset(2026,9,29,11,31,0,TimeSpan.Zero), 4, 2, OrderStatus.Pending, "江苏省苏州市昆山市玉山镇前进中路恒泰商务大厦1205室(前进中路168号)", true, "菊花茶换可乐", 4.5m, 6);

            //var ordertItemsList = new List<OrderItem>();

            //var product1 = await productRepository.FirstOrDefaultAsync(new GetProductByNameSpec("小炒肉"));

            //if (product1 == null)
            //{

            //    return;

            //}

            //var orderItem1 = new OrderItem(order1.Id, product1.Id, 2, product1);

            //ordertItemsList.Add(orderItem1);

            //order1.AddOrderItems(ordertItemsList);

            //await orderManager.AddAsync(order1);

        }

    }
}
