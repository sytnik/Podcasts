using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Microsoft.Office.Interop.Excel;
using ExcelApp = Microsoft.Office.Interop.Excel.Application;

namespace ExcelTransfer;

public partial class Form1 : Form
{
    public Form1() => InitializeComponent();

    private void button1_Click(object sender, EventArgs e)
    {
        using var dialog = new FolderBrowserDialog();
        var result = dialog.ShowDialog();
        if (result is not DialogResult.OK || string.IsNullOrWhiteSpace(dialog.SelectedPath)) return;
        // Get Excel application
        var excelApplication = new ExcelApp();
        // Get all .xls files in the selected directory and subdirectories
        var allFiles = Directory.GetFiles(dialog.SelectedPath, "*.xls", SearchOption.AllDirectories);
        var xlsFiles = allFiles.Where(file => !file.EndsWith(".xlsx")).ToList();
        xlsFiles.ForEach(file =>
        {
            // Open .xls file
            var workbook = excelApplication.Workbooks.Open(file);
            // Save as .xlsx
            var newFileName = Path.ChangeExtension(file, ".xlsx");
            workbook.SaveAs(Filename: newFileName, FileFormat: XlFileFormat.xlOpenXMLWorkbook);
            workbook.Close();
            // Delete the source file
            File.Delete(file);
        });
        // Quit Excel application
        excelApplication.Quit();
        MessageBox.Show($@"All {xlsFiles.Count()} files converted");
    }
}