using System.Linq;
using Avalonia.Controls;
using Avalonia.Interactivity;
using AVGameRPG;            // GameSession
using AVGameRPG.Models;    // Item, ItemCategory

namespace AVGameRPG.Views
{
    public partial class InventoryView : UserControl
    {
        public InventoryView()
        {
            InitializeComponent();
            RefreshAll();
        }

        private void RefreshAll()
        {
            BindCategory(ItemCategory.Head, HeadItems, HeadEmpty, HeadCurrentText);
            BindCategory(ItemCategory.Armor, ArmorItems, ArmorEmpty, ArmorCurrentText);
            BindCategory(ItemCategory.Ring, RingItems, RingEmpty, RingCurrentText);
            BindCategory(ItemCategory.Necklace, NecklaceItems, NecklaceEmpty, NecklaceCurrentText);
            BindCategory(ItemCategory.Gloves, GlovesItems, GlovesEmpty, GlovesCurrentText);
            BindCategory(ItemCategory.Weapon, WeaponItems, WeaponEmpty, WeaponCurrentText);
            BindCategory(ItemCategory.Shield, ShieldItems, ShieldEmpty, ShieldCurrentText);
            BindCategory(ItemCategory.Boots, BootsItems, BootsEmpty, BootsCurrentText);
            BindCategory(ItemCategory.Misc, MiscItems, MiscEmpty, MiscCurrentText);
        }

        private void BindCategory(ItemCategory cat, ItemsControl list, TextBlock emptyLabel, TextBlock currentLabel)
        {
            var items = GameSession.Inventory.Where(i => i.Category == cat)
                                             .OrderBy(i => i.RequiredLevel)
                                             .ThenBy(i => i.Name)
                                             .ToList();

            list.ItemsSource = items;                 // wa¿ne: ItemsSource, nie Items
            emptyLabel.IsVisible = items.Count == 0;

            var equipped = GameSession.Equipment.GetSlot(cat);
            currentLabel.Text = equipped?.Name ?? "";
        }

        private void OnEquipClick(object? sender, RoutedEventArgs e)
        {
            if (sender is not Button btn || btn.DataContext is not Item item) return;

            var player = GameSession.Player;
            GameSession.Equipment.Equip(player, item);  // zdejmie poprzedni i na³o¿y nowy

            RefreshAll();                               // odœwie¿ nag³ówki/listy
        }

        private void OnExitClick(object? sender, RoutedEventArgs e)
        {
            (this.VisualRoot as MainWindow)?.SetContent(new MainGameMenuView());
        }
    }
}


