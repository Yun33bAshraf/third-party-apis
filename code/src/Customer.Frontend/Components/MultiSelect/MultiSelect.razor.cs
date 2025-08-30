using Microsoft.AspNetCore.Components;

namespace OpenProfileAPI.Frontend.Components.MultiSelect;

public partial class MultiSelect<TItem> : ComponentBase
{
    [Parameter]
    public List<TItem> SelectedOptions { get; set; } = new();

    [Parameter]
    public EventCallback<List<TItem>> SelectedOptionsChanged { get; set; }

    [Parameter]
    public List<TItem> Options { get; set; } = new();

    [Parameter]
    public string LabelProperty { get; set; } = "Name"; // Default label property name

    [Parameter]
    public string ValueProperty { get; set; } = "Id"; // Default value property name

    [Parameter]
    public string DefaultText { get; set; } = "Select Options";

    [Parameter]
    public RenderFragment<IEnumerable<TItem>>? SelectedOptionsRenderer { get; set; }

    [Parameter]
    public bool CanFilter { get; set; }

    [Parameter]
    public Func<TItem, string, bool>? FilterPredicate { get; set; }

    private bool DefaultFilterPredicate(TItem item, string filterString)
    {
        var label = typeof(TItem).GetProperty(LabelProperty)?.GetValue(item)?.ToString() ?? string.Empty;
        return label.Contains(filterString, StringComparison.OrdinalIgnoreCase);
    }

    [Parameter]
    public bool Virtualize { get; set; }

    [Parameter]
    public bool IsMultiSelect { get; set; } = true;

    [Parameter]
    public string? Class { get; set; }

    [Parameter]
    public string? Style { get; set; }

    [Parameter]
    public string? SearchPlaceHolder { get; set; } = "Search...";
}
