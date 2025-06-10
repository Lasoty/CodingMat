using Microsoft.AspNetCore.Components;

namespace CodingMate.Client.Controls;

public partial class CodingMat
{
    [Parameter] public int Rows { get; set; }
    [Parameter] public int Columns { get; set; }
    [Parameter] public string? CurrentColor { get; set; }
    [Parameter] public string[,] Matrix { get; set; }
    [Parameter] public EventCallback<(int, int)> OnCellClick { get; set; }
    [Parameter] public double CellSizeEm { get; set; } = 4;

    private string GetCellStyle(int row, int col)
        => $"width:{CellSizeEm}em;height:{CellSizeEm}em;" +
           $"background:{(row >= 0 && col >= 0 && row < Rows && col < Columns ? Matrix[row, col] : "#fff")};" +
           "border-radius:0px;border:1px solid #bbb;cursor:pointer;box-sizing:border-box;";

    private string GetHeaderCellStyle(bool isCol)
        => isCol
            ? $"width:{CellSizeEm}em;height:{Math.Round(CellSizeEm * 1.1, 2)}em;display:flex;align-items:center;justify-content:center;font-weight:bold;"
            : $"width:{Math.Round(CellSizeEm * 1.15 * 8, 0)}px;height:{CellSizeEm}em;display:flex;align-items:center;justify-content:center;font-weight:bold;";


    private string GetColumnLetter(int index)
    {
        // Pozwala na 702 kolumny: A-Z, AA-AZ, BA-BZ, ..., ZZ
        string result = "";
        int n = index;
        do
        {
            result = (char)('A' + (n % 26)) + result;
            n = n / 26 - 1;
        } while (n >= 0);
        return result;
    }

}
