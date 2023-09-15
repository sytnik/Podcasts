using System.Collections;
using System.Diagnostics;
using OfficeOpenXml;
using OfficeOpenXml.Drawing.Chart;

namespace ArraySortCore;

public partial class FormMain : Form
{
    public enum MyEnum
    {
        Bubble,
        Gnome,
        Insertion,
        Merge,
        Quick,
        Selection,
        Shell
    }

    public FormMain()
    {
        InitializeComponent();
        buttonSort.Click += async delegate { await OnSort(); };
        btnBatch.Click += async delegate { await OnBatchSort(); };
    }

    private async Task OnBatchSort()
    {
        var elemCounter = 5;
        var step = 10000;
        buttonSort.Enabled = btnBatch.Enabled = sizeOfArray.Enabled = sortingType.Enabled = false;
        var buttons = sortingType.Controls.OfType<RadioButton>().ToList();
        var buttonNames = buttons.Select(button => button.Name).ToList();
        var elemNumbers = Enumerable.Range(1, elemCounter)
            .Select(i => i * step).ToList();
        var resultsTable = new long[elemCounter, buttonNames.Count];
        for (var i = 0; i < buttons.Count; i++)
        {
            buttons[i].Checked = true;
            for (var y = 1; y <= elemCounter; y++)
            {
                sizeOfArray.Value = elemNumbers[y - 1];
                resultsTable[y - 1, i] = await OnSort();
            }
        }

        await ExcelExport(buttonNames, elemCounter, elemNumbers, resultsTable, buttons);
        buttonSort.Enabled = btnBatch.Enabled = sizeOfArray.Enabled = sortingType.Enabled = true;
    }

    private static async Task ExcelExport(IReadOnlyList<string> buttonNames, int elemCounter,
        IReadOnlyList<int> elemNumbers, long[,] resultsTable, ICollection buttons)
    {
        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
        using var package = new ExcelPackage();
        var sheet = package.Workbook.Worksheets.Add("Results");
        for (var i = 0; i < buttonNames.Count; i++)
            sheet.Cells[1, i + 2].Value = buttonNames[i];
        for (var row = 0; row < elemCounter; row++)
        {
            sheet.Cells[row + 2, 1].Value = elemNumbers[row];
            for (var col = 0; col < buttonNames.Count; col++)
                sheet.Cells[row + 2, col + 2].Value = resultsTable[row, col];
        }

        var chart = sheet.Drawings.AddChart("TestingResultsChart", eChartType.Line);
        for (var i = 0; i < buttons.Count; i++)
        {
            var series = chart.Series
                .Add(sheet.Cells[2, i + 2, elemCounter + 1, i + 2],
                    sheet.Cells[1, i + 2]);
            series.Header = buttonNames[i];
        }

        chart.Title.Text = "Sorting algorithms testing results";
        chart.SetPosition(0, 0,
            buttonNames.Count + 1, 0);
        chart.SetSize(800, 600);
        await package.SaveAsAsync(new FileInfo("testResults.xlsx"));
    }

    private async Task<long> OnSort()
    {
        var array = Enumerable.Range(0, (int)sizeOfArray.Value)
            .Select(_ => new Random().Next()).ToArray();
        pBar.Maximum = array.Length;
        pBar.Value = 0;
        var stopwatch = new Stopwatch();
        var radioButton = sortingType.Controls.OfType<RadioButton>()
            .FirstOrDefault(button => button.Checked);
        var method = radioButton?.Name switch
        {
            "Bubble" => MyEnum.Bubble,
            "Gnome" => MyEnum.Gnome,
            "Insertion" => MyEnum.Insertion,
            "Merge" => MyEnum.Merge,
            "Quick" => MyEnum.Quick,
            "Selection" => MyEnum.Selection,
            "Shell" => MyEnum.Shell
        };
        Action<int[]> action = method switch
        {
            MyEnum.Bubble => BubbleSort,
            MyEnum.Gnome => GnomeSort,
            MyEnum.Insertion => InsertionSort,
            MyEnum.Merge => MergeSort,
            MyEnum.Quick => Array.Sort,
            MyEnum.Selection => SelectionSort,
            MyEnum.Shell => ShellSort,
        };
        stopwatch.Start();
        await Task.Run(() => action(array));
        stopwatch.Stop();
        var textBoxResult = this.textBoxResult;
        textBoxResult.Text = textBoxResult.Text + "\r\n" + method + "  \t" +
                             sizeOfArray.Value + "\tSort time = " +
                             stopwatch.ElapsedMilliseconds + " ms";
        return stopwatch.ElapsedMilliseconds;
    }

    private void BubbleSort(int[] array)
    {
        for (var i = 0; i < array.Length; ++i)
        {
            for (var index2 = 0; index2 < array.Length - i - 1; ++index2)
                if (array[index2] < array[index2 + 1])
                    (array[index2], array[index2 + 1]) = (array[index2 + 1], array[index2]);
            ProgressStep();
        }
    }

    private void ProgressStep()
    {
        if (pBar.Value < pBar.Maximum) Invoke(pBar.PerformStep);
    }

    public void InsertionSort(int[] array)
    {
        for (var i = 1; i < array.Length; ++i)
        {
            var num = array[i];
            int index2;
            for (index2 = i; index2 > 0 && num < array[index2 - 1]; --index2)
                array[index2] = array[index2 - 1];
            array[index2] = num;
            ProgressStep();
        }
    }

    public void SelectionSort(int[] array)
    {
        for (var i = 0; i < array.Length - 1; ++i)
        {
            var index2 = i;
            for (var index3 = i + 1; index3 < array.Length; ++index3)
                if (array[index3] < array[index2])
                    index2 = index3;
            (array[i], array[index2]) = (array[index2], array[i]);
            ProgressStep();
        }
    }

    public void GnomeSort(int[] array)
    {
        var index = 1;
        var num = 2;
        while (index < array.Length)
        {
            if (array[index - 1] < array[index])
            {
                index = num;
                ++num;
            }
            else
            {
                Swap(ref array[index - 1], ref array[index]);
                --index;
                if (index != 0) continue;
                index = num;
                ++num;
            }

            ProgressStep();
        }
    }

    private static void Swap<T>(ref T lhs, ref T rhs) => (lhs, rhs) = (rhs, lhs);

    private void ShellSort(int[] array)
    {
        var num1 = array.Length / 2;
        var num2 = 0;
        while (num1 > 0)
        {
            num1 /= 2;
            ++num2;
        }

        Invoke(delegate { pBar.Maximum = num2;});
        var num3 = array.Length / 2;
        while (num3 > 0)
        {
            for (var i = num3; i < array.Length; ++i)
            {
                var num4 = array[i];
                int index2;
                for (index2 = i - num3; index2 >= 0 && array[index2] > num4; index2 -= num3)
                    array[index2 + num3] = array[index2];
                array[index2 + num3] = num4;
            }

            num3 /= 2;
            ProgressStep();
        }
    }

    private void MergeSort(int[] arr, int left, int right)
    {
        if (left >= right) return;
        var middle = (left + right) / 2;
        MergeSort(arr, left, middle);
        MergeSort(arr, middle + 1, right);
        MergeSort(arr, left, middle, right);
    }

    private void MergeSort(int[] arr, int left, int middle, int right)
    {
        var n1 = middle - left + 1;
        var n2 = right - middle;
        var L = new int[n1];
        var R = new int[n2];
        Array.Copy(arr, left, L, 0, n1);
        Array.Copy(arr, middle + 1, R, 0, n2);
        int i = 0, j = 0;
        for (var k = left; k <= right; k++)
        {
            if (i < n1 && (j >= n2 || L[i] <= R[j]))
            {
                arr[k] = L[i];
                i++;
            }
            else
            {
                arr[k] = R[j];
                j++;
            }
        }

        ProgressStep();
    }

    private void MergeSort(int[] array) => MergeSort(array, 0, array.Length - 1);
}