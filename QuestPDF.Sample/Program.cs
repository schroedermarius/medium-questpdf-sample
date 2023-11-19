using QuestPDF;
using QuestPDF.Infrastructure;
using QuestPDF.Previewer;
using QuestPDF.Sample;

Settings.License = LicenseType.Community;

DocumentModel model = DocumentDataSource.GetDetails();
IDocument document = new Document(model);
        
// document.GeneratePdfAndShow();
await document.ShowInPreviewerAsync();