using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace BroadwayEnglishSchoolTimetable.Models
{
    // В профиль пользователя можно добавить дополнительные данные, если указать больше свойств для класса ApplicationUser. Подробности см. на странице https://go.microsoft.com/fwlink/?LinkID=317594.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Обратите внимание, что authenticationType должен совпадать с типом, определенным в CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Здесь добавьте утверждения пользователя
            return userIdentity;
        }
    }
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("BESTDBEntity", throwIfV1Schema: false)
        {
        }
        static ApplicationDbContext()
        {
           Database.SetInitializer<ApplicationDbContext>(new AppDbInitializer());
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }


    }

    public class AppDbInitializer : CreateDatabaseIfNotExists<ApplicationDbContext>
    {
        //DropCreateDatabaseAlways
        //CreateDatabaseIfNotExists
        //DropCreateDatabaseIfModelChanges
        protected override void Seed(ApplicationDbContext context)
        {
            var userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(context));
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            // создаем две роли
            var role1 = new IdentityRole { Name = "supAdmin" };
            var role2 = new IdentityRole { Name = "admin" };
            var role3 = new IdentityRole { Name = "teacher" };
            var role4 = new IdentityRole { Name = "learner" };

            // добавляем роли в бд
            roleManager.Create(role1);
            roleManager.Create(role2);
            roleManager.Create(role3);
            roleManager.Create(role4);

            // создаем пользователей
            var admin = new ApplicationUser { Email = "ijw53107@awsoo.com", UserName = "whitewAw", EmailConfirmed = true };
            string password = "7D@@vEs6ot";
            var result = userManager.Create(admin, password);

            // если создание пользователя прошло успешно
            if (result.Succeeded)
            {
                // добавляем для пользователя роль
                userManager.AddToRole(admin.Id, role1.Name);
                userManager.AddToRole(admin.Id, role2.Name);
                userManager.AddToRole(admin.Id, role3.Name);
                userManager.AddToRole(admin.Id, role4.Name);
            }
            base.Seed(context);
        }
    }
}