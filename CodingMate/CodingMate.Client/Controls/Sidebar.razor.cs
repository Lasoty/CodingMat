using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using MudBlazor.Utilities;

namespace CodingMate.Client.Controls;

public partial class Sidebar
{
    private int localRows;
    private int localColumns;

    [Parameter] public string? SelectedColor { get; set; }
    [Parameter] public EventCallback<string> OnColorSelected { get; set; }
    [Parameter] public int Rows { get; set; }
    [Parameter] public int Columns { get; set; }
    [Parameter] public EventCallback<(int, int)> OnGridSizeChanged { get; set; }
    [Parameter] public EventCallback OnResetMat { get; set; }
    [Parameter] public EventCallback OnSaveMat { get; set; }

    private MudColor? _pickedColor = MudColor.Parse("#FFFFFF");
    public MudColor? PickedColor
    {
        get => _pickedColor;
        set
        {
            _pickedColor = value;
            if (_pickedColor != null)
            {
                UpdateSelectedColor(_pickedColor).ConfigureAwait(false);
            }
        }
    }


    protected override void OnParametersSet()
    {
        localRows = Rows;
        localColumns = Columns;
    }

    private async Task RowsChanged(int value)
    {
        localRows = Math.Clamp(value, 1, 30);
        await OnGridSizeChanged.InvokeAsync((localRows, localColumns));
    }
    private async Task ColumnsChanged(int value)
    {
        localColumns = Math.Clamp(value, 1, 30);
        await OnGridSizeChanged.InvokeAsync((localRows, localColumns));
    }

    private Task UpdateSelectedColor(MudColor color)
    {
        SelectedColor = color.Value;
        return OnColorSelected.InvokeAsync(color.Value);
    }

    private Task ResetMat() => OnResetMat.InvokeAsync();

    private Task ExportMatToJpg()
    {
        return OnSaveMat.InvokeAsync();
    }
}
