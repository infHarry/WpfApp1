# WpfApp1

## 合并PDF

使用PDFsharp包合并PDF

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

## 拖拽文件夹

TextBox使用PreviewDrop和PreviewDragOver实现拖拽

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
                //注意添加
                e.Handled = true;
            }
            else
            {
                e.Effects |= DragDropEffects.None;
            }
        }
```

## 选择文件夹

`使用WindowsAPICodePack代替FolderBrowserDialog对象选择文件夹`
在VS里打开Package Manager Console后输入Install-Package WindowsAPICodePack - Shell
或者访问[网址](https://www.nuget.org/packages/WindowsAPICodePack-Shell/)