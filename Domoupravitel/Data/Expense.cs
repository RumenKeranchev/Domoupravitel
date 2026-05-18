namespace Domoupravitel.Data
{
    public class Expense
    {
        public Guid Id { get; set; }

        public DateOnly Date { get; set; } = DateOnly.FromDateTime(DateTime.UtcNow);

        public decimal Electricity { get; set; }

        public decimal Elevator { get; set; }

        public decimal Pets { get; set; }

        public decimal Vault { get; set; }

        public decimal MonthlyFee { get; set; }

        public ICollection<Transaction> Transactions { get; set; } = [];

        public decimal Total => Electricity + Elevator + Pets + Vault + MonthlyFee;
    }
}
