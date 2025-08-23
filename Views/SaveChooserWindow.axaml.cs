using System.Collections.Generic;
using System.IO;
using System.Linq;
using Avalonia.Controls;
using Avalonia.Interactivity;

namespace AVGameRPG.Views
{
    public partial class SaveChooserWindow : Window
    {
        // wymagany przez Avaloniê (designer/XAML)
        public SaveChooserWindow()
        {
            InitializeComponent();
        }

        // nasz wygodny konstruktor
        public SaveChooserWindow(IEnumerable<string> files) : this()
        {
            var rows = files.Select(p => new FileInfo(p))
                            .OrderByDescending(f => f.LastWriteTimeUtc)
                            .ToList();

            FilesList.ItemsSource = rows; // w XAML: DisplayMemberBinding="{Binding Name}"
        }

        private void OnSelectionChanged(object? s, SelectionChangedEventArgs e)
            => LoadBtn.IsEnabled = FilesList.SelectedItem != null;

        private void OnLoadClick(object? s, RoutedEventArgs e)
            => Close((FilesList.SelectedItem as FileInfo)?.FullName);

        private void OnCancelClick(object? s, RoutedEventArgs e)
            => Close(null);
    }
}


