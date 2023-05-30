# WpfApp1

## �ϲ�PDF

ʹ��PDFsharp���ϲ�PDF

```csharp
private static void MergeMultiplePDFIntoSinglePDF(string outputFilePath, string[] pdfFiles)
        {
            PdfDocument outputPDFDocument = new PdfDocument();
            foreach (string pdfFile in pdfFiles)
            {
                PdfDocument inputPDFDocument = PdfReader.Open(pdfFile, PdfDocumentOpenMode.Import);
                outputPDFDocument.Version = inputPDFDocument.Version;
                foreach (PdfPage page in inputPDFDocument.Pages)
                {
                    outputPDFDocument.AddPage(page);
                }
            }
            outputPDFDocument.Save(outputFilePath);
        }
```

## ��ק�ļ���

TextBoxʹ��PreviewDrop��PreviewDragOverʵ����ק

```csharp
private void TextBox_PreviewDrop(object sender, DragEventArgs e)
        {
            try
            {
                string filePath = ((System.Array)e.Data.GetData(DataFormats.FileDrop)).GetValue(0).ToString();
                TextBox.Text = filePath;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

private void TextBox_PreviewDragOver(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effects = DragDropEffects.Copy;
                //ע�����
                e.Handled = true;
            }
            else
            {
                e.Effects |= DragDropEffects.None;
            }
        }
```

## ѡ���ļ���

`ʹ��WindowsAPICodePack����FolderBrowserDialog����ѡ���ļ���`
��VS���Package Manager Console������Install-Package WindowsAPICodePack - Shell
���߷���[��ַ](https://www.nuget.org/packages/WindowsAPICodePack-Shell/)