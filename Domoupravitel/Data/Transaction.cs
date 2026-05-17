namespace Domoupravitel.Data
{
    public class Transaction
    {
        public Guid Id { get; set; }

        public DateOnly Date { get; set; } = DateOnly.FromDateTime(DateTime.UtcNow);

        public TransactionType Type { get; set; }

        public int NoOfPeopleLiving { get; set; }

        public decimal Amount { get; set; }

        public string? Description { get; set; }

        public Guid ExpenseId { get; set; }
        public Expense? Expense { get; set; }

        public Guid? BudgetId { get; set; }
        public Budget? Budget { get; set; }

        public Guid? UserId { get; set; }
        public ApplicationUser? User { get; set; }

        public ICollection<Document> Documents { get; set; } = [];
    }
}
