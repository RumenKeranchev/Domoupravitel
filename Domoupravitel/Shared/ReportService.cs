namespace Domoupravitel.Shared
{
    using Domoupravitel.Data;
    using Microsoft.EntityFrameworkCore;

    public class ReportService
    {
        private readonly ApplicationDbContext db;

        public ReportService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public async Task<(Expense? Expense, List<UserReport> Reports)> GetUserReportsAsync(int month, int year)
        {
            var expense = await db.Expenses
            .AsNoTracking()
            .FirstOrDefaultAsync(e => e.Date.Month == month && e.Date.Year == year);

            if (expense is null)
            {
                return (null, []);
            }

            var users = await db.Users
                .Select(u => new
                {
                    u.ApartmentNo,
                    u.NoOfPeopleLiving,
                    u.NoOfPets,
                    u.AmountDue
                })
                .Distinct()
                .ToListAsync();

            var userReports = users
                .Select(u => new UserReport(
                    u.ApartmentNo,
                    (int)Math.Ceiling(u.ApartmentNo / 3.0),
                    u.NoOfPeopleLiving,
                    expense.Vault * u.NoOfPeopleLiving,
                    expense.MonthlyFee * u.NoOfPeopleLiving,
                    expense.Elevator * u.NoOfPeopleLiving,
                    expense.Electricity * u.NoOfPeopleLiving,
                    expense.Pets * u.NoOfPets,
                    ((expense.Total - expense.Pets) * u.NoOfPeopleLiving) + (expense.Pets * u.NoOfPets),
                    u.AmountDue
                ))
                .ToList();

            return (expense, userReports);
        }
    }
}
