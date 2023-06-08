using Microsoft.WindowsAPICodePack.Dialogs;
using PdfSharp.Pdf;
using PdfSharp.Pdf.IO;
using System;
using System.IO;
using System.Windows;

namespace WpfApp1
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Tab1_TextBox1_PreviewDragOver(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effects = DragDropEffects.Copy;
                //解决办法添加
                e.Handled = true;
            }
            else
            {
                e.Effects |= DragDropEffects.None;
            }
        }

        private void Tab1_TextBox1_PreviewDrop(object sender, DragEventArgs e)
        {
            try
            {
                string filePath = ((System.Array)e.Data.GetData(DataFormats.FileDrop)).GetValue(0).ToString();
                Tab1_TextBox1.Text = filePath;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void Tab1_TextBox2_PreviewDragOver(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effects = DragDropEffects.Copy;
                //解决办法添加
                e.Handled = true;
            }
            else
            {
                e.Effects |= DragDropEffects.None;
            }
        }

        private void Tab1_TextBox2_PreviewDrop(object sender, DragEventArgs e)
        {
            try
            {
                string filePath = ((System.Array)e.Data.GetData(DataFormats.FileDrop)).GetValue(0).ToString();
                Tab1_TextBox2.Text = filePath;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Tab1_Btn1_Click(object sender, RoutedEventArgs e)
        {
            // 在VS里打开Package Manager Console后输入Install-Package WindowsAPICodePack - Shell
            // https://www.nuget.org/packages/WindowsAPICodePack-Shell/
            CommonOpenFileDialog dialog = new CommonOpenFileDialog
            {
                IsFolderPicker = true,//设置为选择文件夹
                //Multiselect = true //文件夹多选
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory),
            };
            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                Tab1_TextBox1.Text = dialog.FileName;
            }
        }

        private void Tab1_Btn2_Click(object sender, RoutedEventArgs e)
        {
            // 在VS里打开Package Manager Console后输入Install-Package WindowsAPICodePack - Shell
            // https://www.nuget.org/packages/WindowsAPICodePack-Shell/
            CommonOpenFileDialog dialog = new CommonOpenFileDialog
            {
                IsFolderPicker = true,//设置为选择文件夹
                                      //Multiselect = true //文件夹多选
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory),
            };
            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                Tab1_TextBox2.Text = dialog.FileName;
            }
        }

        private void Tab1_Btn3_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(Tab1_TextBox1.Text))
            {
                MessageBox.Show("请选择数据文件夹");
                return;
            }
            // 修复Tab1_TextBox2.Text不为空时不会合并的BUG
            string saveFilePath;
            if (string.IsNullOrEmpty(Tab1_TextBox2.Text))
            {
                saveFilePath = Tab1_TextBox1.Text + "\\0000合并.pdf";
            }
            else
            {
                saveFilePath = Tab1_TextBox2.Text + "\\0000合并.pdf";
            }
            if (File.Exists(saveFilePath))
            {
                File.Delete(saveFilePath);
            }

            string[] pdfFiles = Directory.GetFiles(Tab1_TextBox1.Text.ToString(), "*.pdf");
            MergeMultiplePDFIntoSinglePDF(saveFilePath, pdfFiles);
            MessageBox.Show("完成合并!");
            Tab1_TextBox1.Text = "";
            Tab1_TextBox2.Text = "";
            // 结束
        }
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
    }
}
