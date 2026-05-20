namespace Domoupravitel.Data
{
    using Microsoft.AspNetCore.Identity;

    public class ApplicationUser : IdentityUser<Guid>
    {
        public int ApartmentNo { get; set; }

        public int NoOfPeopleLiving { get; set; }

        public int NoOfPets { get; set; }

        public decimal AmountDue { get; set; }

        public ICollection<Transaction> Transactions { get; set; } = [];
    }
}