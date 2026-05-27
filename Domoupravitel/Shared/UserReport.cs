namespace Domoupravitel.Shared
{
    public record UserReport(int ApartmentNo, int Floor, int NoOfPeopleLiving, decimal Vault, decimal MonthlyFee, decimal Elevator, decimal Electricity, decimal Pets, decimal Total, decimal AmountDue);
}
