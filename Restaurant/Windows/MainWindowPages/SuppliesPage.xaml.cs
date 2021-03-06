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
    public partial class SuppliesPage : Page
    {
        public SuppliesPage()
        {
            InitializeComponent();
            FetchData();
        }

        private void FetchData()
        {
            using (RestaurantManagerContext context = new RestaurantManagerContext())
            {
                var sup = context.Supplies.ToList();
                SuppliesList.ItemsSource = sup;
            }
        }

        private void OnEdit(object sender, RoutedEventArgs e)
        {
            var dataContext = ((Button)sender).DataContext;
            Supply clone = (Supply)((Supply)dataContext).Clone();
            EditSupplyWindow editSupplyWindow = new EditSupplyWindow() { DataContext = clone };
            editSupplyWindow.ShowDialog();

            if (editSupplyWindow.DialogResult == true)
            {
                Supply sup = (Supply) editSupplyWindow.DataContext;
                using (RestaurantManagerContext context = new RestaurantManagerContext())
                {
                    context.Supplies.Update(sup);
                    context.SaveChanges();
                    FetchData();
                }
            }
        }

        private void OnAdd(object sender, RoutedEventArgs e)
        {
            EditSupplyWindow editSupplyWindow = new EditSupplyWindow() { DataContext = new Supply() };
            editSupplyWindow.Title = "Добавление новой поставки";
            editSupplyWindow.ShowDialog();

            if (editSupplyWindow.DialogResult == true)
            {
                Supply sup = (Supply)editSupplyWindow.DataContext;
                using (RestaurantManagerContext context = new RestaurantManagerContext())
                {
                    context.Supplies.Add(sup);
                    context.SaveChanges();
                    FetchData();
                }
            }
        }

        private void OnDelete(object sender, RoutedEventArgs e)
        {
            Supply supply = ((Supply)((Button)sender).DataContext);
            MessageBoxResult result = MessageBox.Show(
                "Вы действительно хотите удалить поставку? \"" + supply.Date + " \" ?",
                "Удаление поставки",
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes)
                using (RestaurantManagerContext context = new RestaurantManagerContext())
                {
                    context.Supplies.Remove(supply);
                    context.SaveChanges();
                    FetchData();
                }
        }



        private void OnSearch(object sender, RoutedEventArgs e)
        {
            String searchString = SearchField.Text;
            using (RestaurantManagerContext context = new RestaurantManagerContext())
            {
                var sup = context.Supplies.Where(u => u.Date.ToString().Contains(searchString)).ToList();
                SuppliesList.ItemsSource = sup;
            }
        }
    }
}
