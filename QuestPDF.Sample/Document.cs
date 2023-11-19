using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;

namespace QuestPDF.Sample;

public class Document : IDocument
{
    private readonly DocumentModel _model;

    public Document(DocumentModel model)
    {
        _model = model;
    }

    public DocumentMetadata GetMetadata() => DocumentMetadata.Default;

    public void Compose(IDocumentContainer container)
    {
        container
            .Page(page =>
            {
                page.Margin(50);

                page.Header().Element(ComposeHeader);
                page.Content().Element(ComposeContent);

                page.Footer().AlignCenter().Text(text =>
                {
                    text.CurrentPageNumber();
                    text.Span(" / ");
                    text.TotalPages();
                });
            });
    }

    private void ComposeHeader(IContainer container)
    {
        container.PaddingBottom(24).Row(row =>
        {
            row.RelativeItem().Column(column =>
            {
                column
                    .Item().Text($"Data Protection")
                    .FontSize(20).SemiBold().FontColor(Colors.Blue.Medium);
            });
        });
    }

    private void ComposeContent(IContainer container)
    {
        container.Column(column =>
        {
            column.Spacing(48);
            column.Item().Element(ComposeDescription);
            column.Item().Element(ComposeInputs);
            column.Item().Text(Placeholders.Paragraphs());

            column.Item().Element(ComposeSignature);
        });
    }

    private void ComposeDescription(IContainer container)
    {
        container.Column(column =>
        {
            column.Item().Text(text =>
            {
                text.Span("Issue date: ").SemiBold();
                text.Span($"{DateTime.Now:d}");
            });
                    
            column.Item()
                .Component(new AddressComponent("Data Protection Officer", _model.Address));
            
            column.Item().PaddingTop(24).Text(Placeholders.Paragraph());
        });
    }
    
    private void ComposeInputs(IContainer container)
    {
        container.Column(column =>
        {
            column.Spacing(12);
            
            column.Item().Component(new InputComponent("Fullname"));
            column.Item().Component(new InputComponent("Birthdate"));
            column.Item().Component(new InputComponent("Street"));
            column.Item().Component(new InputComponent("City"));
            column.Item().Component(new InputComponent("State"));
            column.Item().Component(new InputComponent("Email"));
            column.Item().Component(new InputComponent("Phone"));
        });
    }

    private void ComposeSignature(IContainer container)
    {
        container.Column(column =>
        {
            column.Spacing(24);
            column.Item().Row(row =>
            {
                row.ConstantItem(100).Text("Date:").SemiBold();
                row.RelativeItem().ExtendHorizontal().BorderBottom(1).Padding(5);
            });
            column.Item().Row(row =>
            {
                row.ConstantItem(100).Text("Signature:").SemiBold();
                row.RelativeItem().ExtendHorizontal().BorderBottom(1).Padding(5);
            });
        });
    }

    public class InputComponent : IComponent
    {
        private readonly string _label;

        public InputComponent(string label)
        {
            _label = label;
        }
        
        public void Compose(IContainer container)
        {
            container.Row(row =>
            {
                row.ConstantItem(100).AlignLeft()
                    .Text($"{_label}:")
                    .SemiBold();
                
                row.RelativeItem(10)
                    .ExtendHorizontal()
                    .BorderBottom(1).Padding(5)
                    .Background(Colors.Grey.Lighten1)
                    .AlignCenter();
            });
        }
    }
    
    public class AddressComponent : IComponent
    {
        private string Title { get; }
        private AddressModel Address { get; }

        public AddressComponent(string title, AddressModel address)
        {
            Title = title;
            Address = address;
        }
        
        public void Compose(IContainer container)
        {
            container.ShowEntire().Column(column =>
            {
                column.Spacing(2);

                column.Item().Text(Title).SemiBold();
                column.Item().PaddingBottom(5).LineHorizontal(1); 
                
                column.Item().Text(Address.CompanyName);
                column.Item().Text(Address.Street);
                column.Item().Text($"{Address.City}, {Address.State}");
                column.Item().Text(Address.Email);
                column.Item().Text(Address.Phone);
            });
        }
    }
}