namespace Domoupravitel.Data
{
    public class Document
    {
        public Guid Id { get; set; }

        public Guid TransactionId { get; set; }
        public Transaction? Transaction { get; set; }

        public required string FileName { get; set; }

        public byte[] File { get; set; } = [];
    }
}
