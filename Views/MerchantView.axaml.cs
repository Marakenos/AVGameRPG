using System.Collections.Generic;
using System.Linq;
using Avalonia.Controls;
using Avalonia.Interactivity;
using AVGameRPG;             // GameSession
using AVGameRPG.Models;     // Item, Equipment, ShopData

namespace AVGameRPG.Views
{
    public partial class MerchantView : UserControl
    {
        private readonly List<Item> _catalog;

        public MerchantView()
        {
            InitializeComponent();

            // Złoto gracza
            PlayerGoldText.Text = GameSession.Player.Gold.ToString();

            // Załaduj ofertę sklepu
            _catalog = ShopData.BuildDefault();

            // Podpięcie źródeł pod każdą kategorię
            HeadList.ItemsSource = Filter(ItemCategory.Head);
            ArmorList.ItemsSource = Filter(ItemCategory.Armor);
            RingList.ItemsSource = Filter(ItemCategory.Ring);
            NecklaceList.ItemsSource = Filter(ItemCategory.Necklace);
            GlovesList.ItemsSource = Filter(ItemCategory.Gloves);
            WeaponList.ItemsSource = Filter(ItemCategory.Weapon);
            ShieldList.ItemsSource = Filter(ItemCategory.Shield);
            BootsList.ItemsSource = Filter(ItemCategory.Boots);
            MiscList.ItemsSource = Filter(ItemCategory.Misc);
        }

        private IEnumerable<Item> Filter(ItemCategory cat) =>
            _catalog.Where(i => i.Category == cat)
                    .OrderBy(i => i.RequiredLevel)
                    .ThenBy(i => i.Name);

        // Kliknięcie „Buy”
        private void OnBuyClick(object? sender, RoutedEventArgs e)
        {
            if (sender is not Button btn || btn.DataContext is not Item item)
                return;

            var player = GameSession.Player;

            // ⛔ BLOKADA: wymagany poziom
            if (player.Level < item.RequiredLevel)
            {
                ShowMessage($"Requires level {item.RequiredLevel} to buy this item.");
                return;
            }

            if (player.Gold < item.Price)
            {
                ShowMessage($"Not enough gold. Need {item.Price}.");
                return;
            }

            // Zapłać
            player.Gold -= item.Price;
            PlayerGoldText.Text = player.Gold.ToString();

            // Dodaj kopię do ekwipunku gracza
            GameSession.Inventory.Add(new Item
            {
                Name = item.Name,
                Category = item.Category,
                RequiredLevel = item.RequiredLevel,
                Price = item.Price,
                Attack = item.Attack,
                Defense = item.Defense,
                Agility = item.Agility,
                Intelligence = item.Intelligence,
                Hp = item.Hp,
                Description = item.Description
            });

            ShowMessage($"Purchased: {item.Name} (added to inventory)");
        }

        private async void ShowMessage(string msg)
        {
            var w = this.VisualRoot as Window;
            if (w is null) return;

            var dialog = new Window
            {
                Width = 360,
                Height = 140,
                CanResize = false,
                Title = "Shop",
                Content = new TextBlock
                {
                    Text = msg,
                    HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Center,
                    VerticalAlignment = Avalonia.Layout.VerticalAlignment.Center
                }
            };

            await dialog.ShowDialog(w);
        }

        private void OnExitClick(object? sender, RoutedEventArgs e)
        {
            (this.VisualRoot as MainWindow)?.SetContent(new MainGameMenuView());
        }
    }
}



