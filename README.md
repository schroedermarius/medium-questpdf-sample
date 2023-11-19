# QuestPDF Sample App

This sample application demonstrates the usage of QuestPDF, a C# library for generating PDFs.

## Getting Started

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)

### Installation

1. Clone the repository:

   ```bash
   git clone https://github.com/schroedermarius/medium-questpdf-sample.git
   cd medium-questpdf-sample
   ```

2. Restore NuGet packages:
    ```
    dotnet restore
    ```

### Usage
1. Open the solution in your preferred IDE.

2. Navigate to the Program.cs file.

3. Modify the DocumentModel and AddressModel classes to suit your data structure.

4. Implement the MyDocument class by extending IDocument and define the PDF layout and content in the Compose method.

5. In the Main method of Program.cs, set the license type:

    ```csharp
    Settings.License = LicenseType.Community;
    ```

6. Generate the PDF:
    
    ```csharp
   var documentModel = new DocumentModel();
    var document = new MyDocument(documentModel);
    
    // Generate and show the PDF
    document.GeneratePdfAndShow();
    // Or to use the previewer
    // await document.ShowInPreviewerAsync();
    ```
7. Run the application to generate the PDF document.

## License
This project is licensed under the MIT License.

## Acknowledgments
Special thanks to QuestPDF for providing a powerful library for PDF generation.