using Restaurant.Models;
using Restaurant.Windows.MainWindowPages.EditWindows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Restaurant.Windows.MainWindowPages
{
    /// <summary>
    /// Логика взаимодействия для StatusesPage.xaml
    /// </summary>
    public partial class StatusesPage : Page
    {
        public StatusesPage()
        {
            InitializeComponent();
            FetchData();
        }

        private void FetchData()
        {
            using (RestaurantManagerContext context = new RestaurantManagerContext())
            {
                var statuses = context.SupplyStatuses.ToList();
                StatusesList.ItemsSource = statuses;
            }
        }

        private void OnEdit(object sender, RoutedEventArgs e)
        {
            var dataContext = ((Button)sender).DataContext;
            SupplyStatus clone = (SupplyStatus)((SupplyStatus) dataContext).Clone();
            EditStatusWindow editStatusWindow = new EditStatusWindow() { DataContext = clone };
            editStatusWindow.ShowDialog();

            if (editStatusWindow.DialogResult == true)
            {
                SupplyStatus status = (SupplyStatus) editStatusWindow.DataContext;
                using (RestaurantManagerContext context = new RestaurantManagerContext())
                {
                    context.SupplyStatuses.Update(status);
                    context.SaveChanges();
                    FetchData();
                }
            }
        }

        private void OnAdd(object sender, RoutedEventArgs e)
        {
            EditStatusWindow editProductWindow = new EditStatusWindow() { DataContext = new SupplyStatus() };
            editProductWindow.Title = "Добавление нового статуса поставки";
            editProductWindow.ShowDialog();

            if (editProductWindow.DialogResult == true)
            {
                SupplyStatus status = (SupplyStatus) editProductWindow.DataContext;
                using (RestaurantManagerContext context = new RestaurantManagerContext())
                {
                    context.SupplyStatuses.Add(status);
                    context.SaveChanges();
                    FetchData();
                }
            }
        }

        private void OnDelete(object sender, RoutedEventArgs e)
        {
            SupplyStatus status = ((SupplyStatus)((Button)sender).DataContext);
            MessageBoxResult result = MessageBox.Show(
                "Вы действительно хотите удалить продукт \"" + status.Title + " \" ?",
                "Удаление пользователя",
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes)
                using (RestaurantManagerContext context = new RestaurantManagerContext())
                {
                    context.SupplyStatuses.Remove(status);
                    context.SaveChanges();
                    FetchData();
                }
        }



        private void OnSearch(object sender, RoutedEventArgs e)
        {
            String searchString = SearchField.Text;
            using (RestaurantManagerContext context = new RestaurantManagerContext())
            {
                var products = context.SupplyStatuses.Where(u => u.Title.Contains(searchString)).ToList();
                StatusesList.ItemsSource = products;
            }
        }
    }
}
