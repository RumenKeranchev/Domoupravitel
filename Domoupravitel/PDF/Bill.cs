namespace Domoupravitel.PDF
{
    using Domoupravitel.Shared;
    using QuestPDF.Fluent;
    using QuestPDF.Helpers;
    using QuestPDF.Infrastructure;

    public class Bill(List<UserReport> reports, int month, int year) : IDocument
    {
        public void Compose(IDocumentContainer container) => container.Page(page =>
        {
            page.Size(PageSizes.A4);
            page.Margin(0.1f, Unit.Centimetre);

            page.Content().Element(cont
                => cont.Column(col =>
                {
                    for (int i = 0; i < reports.Count - 1; i += 2)
                    {
                        col.Item().PaddingBottom(0.3f, Unit.Centimetre).Row(r =>
                        {
                            r.RelativeItem().PaddingRight(0.3f, Unit.Centimetre).Element(cont => Elem(cont, reports[i]));
                            r.RelativeItem().Element(cont => Elem(cont, reports[i + 1]));
                        });
                    }
                }));
        });

        private void Elem(IContainer container, UserReport elem)
            => container
            .Border(1)
            .Column(col =>
            {
                col
                .Item()
                .Border(1)
                .Padding(2)
                .Row(row =>
                {
                    row.RelativeItem().Text($"За месец: {month:d2}/{year}").AlignCenter().Bold();
                    row.RelativeItem().Text($"Апартамент №: {elem.ApartmentNo}").AlignCenter().Bold();
                });

                col
                .Item()
                .Row(row =>
                {
                    row.RelativeItem().Text($"Каса:").FontSize(10).AlignCenter();
                    row.RelativeItem().Text($"Месечна:").FontSize(10).AlignCenter();
                    row.RelativeItem().Text($"Асансьор:").FontSize(10).AlignCenter();
                    row.RelativeItem().Text($"Ток:").FontSize(10).AlignCenter();
                    row.RelativeItem(2).Text($"Дом. любимци:").FontSize(10).AlignCenter();
                });

                col
                .Item()
                .Row(row =>
                {
                    row.RelativeItem().Text($"{elem.Vault}").FontSize(10).AlignCenter();
                    row.RelativeItem().Text($"{elem.MonthlyFee}").FontSize(10).AlignCenter();
                    row.RelativeItem().Text($"{elem.Elevator}").FontSize(10).AlignCenter();
                    row.RelativeItem().Text($"{elem.Electricity}").FontSize(10).AlignCenter();
                    row.RelativeItem(2).Text($"{elem.Pets}").FontSize(10).AlignCenter();
                });

                col
                .Item()
                .Border(1)
                .Padding(2)
                .Row(row =>
                {
                    row.RelativeItem().Text($"Общо: {elem.Total}").AlignCenter().Bold();
                    row.RelativeItem().Text($"Дължимо: {elem.AmountDue}").AlignCenter().Bold();
                });
            });
    }
}
