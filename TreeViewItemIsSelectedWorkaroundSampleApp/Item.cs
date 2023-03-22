using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using System.Linq;

namespace TreeViewItemIsSelectedWorkaroundSampleApp;

public partial class Item : ObservableObject
{
    private readonly Item? parentItem;

    [ObservableProperty]
    private string text = string.Empty;

    [ObservableProperty]
    private bool? isSelected = false;

    [ObservableProperty]
    private ObservableCollection<Item> childItems = new();

    public Item(Item? parentItem)
    {
        this.parentItem = parentItem;
    }

    private bool IgnoreIsSelectedChange { get; set; }

    public void UpdateIsSelected()
    {
        IgnoreIsSelectedChange = true;

        IsSelected = ChildItems
            .Where(x => x.IsSelected is true)
            .Count() == ChildItems.Count
                ? true
                : ChildItems
                    .Where(x => x.IsSelected is false)
                    .Count() == ChildItems.Count
                        ? false
                        : null;

        parentItem?.UpdateIsSelected();

        IgnoreIsSelectedChange = false;
    }

    partial void OnIsSelectedChanged(bool? value)
    {
        if (IgnoreIsSelectedChange is true)
        {
            return;
        }

        foreach (Item item in ChildItems)
        {
            item.IsSelected = value;
        }

        parentItem?.UpdateIsSelected();
    }
}