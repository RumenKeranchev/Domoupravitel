namespace Domoupravitel.Shared
{
    using Bogus;
    using Domoupravitel.Data;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;

    public static class Seeder
    {
        private static List<ApplicationUser> Users = [];
        private const string AdminEmail = "test@test.com";

        public static async Task<bool> SeedUsersAsync(IServiceProvider services)
        {
            using var scope = services.CreateScope();

            var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole<Guid>>>();

            await db.Database.MigrateAsync();

            int usersCount = await userManager.Users.CountAsync();

            if (usersCount > 0)
            {
                return false;
            }

            string[] roles = [Roles.Admin, Roles.Domoupravitel, Roles.User];

            foreach (string? role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole<Guid>(role));
                }
            }

            var admin = new ApplicationUser { Email = AdminEmail, UserName = "admin", ApartmentNo = 12, NoOfPeopleLiving = 1 };
            var domoupravitel = new ApplicationUser { Email = "domoupravitel@whatever.com", UserName = "domoupravitel", ApartmentNo = 24, NoOfPeopleLiving = 1 };

            var result = await userManager.CreateAsync(admin, "Test123_");
            await userManager.AddToRoleAsync(admin, Roles.Admin);

            result = await userManager.CreateAsync(domoupravitel, "domoupravitel");
            await userManager.AddToRoleAsync(domoupravitel, Roles.Domoupravitel);

            var usersFaker = new Faker<ApplicationUser>()
                .RuleFor(u => u.UserName, (f, u) => f.Internet.UserName())
                .RuleFor(u => u.Email, (f, u) => f.Internet.Email(u.UserName))
                .RuleFor(u => u.EmailConfirmed, f => true)
                .RuleFor(u => u.NoOfPeopleLiving, f => f.Random.Int(1, 4));

            Users = usersFaker.Generate(22);

            foreach (var fake in Users)
            {
                fake.ApartmentNo = Users.IndexOf(fake) + 1 == 12 ? 23 : Users.IndexOf(fake) + 1;
                result = await userManager.CreateAsync(fake, "password");
                await userManager.AddToRoleAsync(fake, Roles.User);
            }

            Users.Add(admin);
            Users.Add(domoupravitel);

            return true;
        }

        public static async Task FakeData(IServiceProvider services)
        {
            using var scope = services.CreateScope();
            var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            var expenses = new List<Expense>{
                new()
                {
                    Date = DateOnly.FromDateTime(DateTime.Now),
                    Electricity = 1,
                    Elevator = 3,
                    Pets = 2.5m,
                    MonthlyFee = 3,
                    Vault = 2.5m,
                    Id = Guid.NewGuid(),
                    Transactions = []
                },
                new()
                {
                    Date = DateOnly.FromDateTime(DateTime.Now.AddMonths(-1)),
                    Electricity = 1.25m,
                    Elevator = 2.75m,
                    Pets = 2.75m,
                    MonthlyFee = 3.2m,
                    Vault = 3m,
                    Id = Guid.NewGuid(),
                    Transactions = []
                },
                new()
                {
                    Date = DateOnly.FromDateTime(DateTime.Now.AddMonths(-2)),
                    Electricity = 2m,
                    Elevator = 3.3m,
                    Pets = 2.5m,
                    MonthlyFee = 3.4m,
                    Vault = 2.5m,
                    Id = Guid.NewGuid(),
                    Transactions = []
                }
            };

            db.AddRange(expenses);

            foreach (var expense in expenses)
            {
                foreach (var user in Users)
                {
                    db.Transactions.Add(new Transaction
                    {
                        Id = Guid.NewGuid(),
                        Date = DateOnly.FromDateTime(DateTime.Now),
                        Type = TransactionType.Income,
                        NoOfPeopleLiving = user.NoOfPeopleLiving,
                        Amount = expense.Total * user.NoOfPeopleLiving,
                        Expense = expense
                    });
                }
            }

            await db.SaveChangesAsync();
        }
    }
}
