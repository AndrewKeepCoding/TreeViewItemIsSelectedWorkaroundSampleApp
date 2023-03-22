using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;

namespace TreeViewItemIsSelectedWorkaroundSampleApp;

public partial class ViewModel : ObservableObject
{
    [ObservableProperty]
    private ObservableCollection<Item> items = new();

    public ViewModel()
    {
        Item root = new(null) { Text = "Root" };
        Items.Add(root);

        int childCount = 3;
        int grandChildCount = 3;

        foreach (string item in new[] { "A", "B", "C" })
        {
            Item childItem = new(root) { Text = item };
            root.ChildItems.Add(childItem);

            for (int childIndex = 0; childIndex < childCount; childIndex++)
            {
                string text = $"{item}-{childIndex + 1}";
                Item grandChildItem = new(childItem) { Text = $"{text}" };
                childItem.ChildItems.Add(grandChildItem);

                for (int grandChildIndex = 0; grandChildIndex < grandChildCount; grandChildIndex++)
                {
                    grandChildItem.ChildItems.Add(new(grandChildItem) { Text = $"{item}-{childIndex + 1}-{grandChildIndex + 1}" });
                }
            }
        }
    }
}