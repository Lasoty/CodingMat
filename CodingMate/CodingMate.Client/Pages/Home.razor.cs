using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace CodingMate.Client.Pages;

public partial class Home
{
    [Inject] private IJSRuntime JS { get; set; }


    public int Rows { get; set; } = 10;
    public int Columns { get; set; } = 10;
    public string SelectedColor { get; set; } = "#FFFFFF";
    public string[,] Matrix { get; set; }

    protected override void OnInitialized()
    {
        InitMatrix();
    }

    void InitMatrix()
    {
        Matrix = new string[Rows, Columns];
    }

    void SetCellColor(int row, int col)
    {
        Matrix[row, col] = Matrix[row, col] == SelectedColor ? string.Empty : SelectedColor;
        StateHasChanged();
    }

    void SetGridSize((int, int) values)
    {
        Rows = values.Item1;
        Columns = values.Item2;
        InitMatrix();
        StateHasChanged();
    }

    void SelectColor(string color)
    {
        SelectedColor = color;
        StateHasChanged();
    }

    private void ResetMat()
    {
        for (int i = 0; i < Rows; i++)
        {
            for (int j = 0; j < Columns; j++)
            {
                Matrix[i, j] = string.Empty;
            }
        }
        StateHasChanged();
    }

    private async Task SaveMat()
    {
        // Konwertuj string[,] Matrix do tablicy tablic JS (row-major)
        var matrixList = new List<List<string>>();
        for (int r = 0; r < Rows; r++)
        {
            var row = new List<string>();
            for (int c = 0; c < Columns; c++)
                row.Add(string.IsNullOrEmpty(Matrix[r, c]) ? "#fff" : Matrix[r, c]);
            matrixList.Add(row);
        }

        // Ustal rozmiar kratki w px: możesz bazować na CellSizeEm z CodingMat lub wpisać na sztywno
        int cellSizePx = (int)(4 * 16); // np. 4em * 16px = 64px

        var matData = new
        {
            rows = Rows,
            columns = Columns,
            matrix = matrixList,
            cellSizePx = cellSizePx
        };

        await JS.InvokeVoidAsync("exportCodingMatToJpg", matData);
    }
}
