namespace Domoupravitel.Data
{
    public class Budget
    {
        public Guid Id { get; set; }

        public DateOnly Date { get; set; }

        public decimal Amount { get; set; }

        public ICollection<Transaction> Transactions { get; set; } = [];
    }
}
