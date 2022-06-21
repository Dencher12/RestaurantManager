using Microsoft.EntityFrameworkCore;
using Restaurant.Models;
using Restaurant.Windows.MainWindowPages.EditWindows;
using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Restaurant.Windows.MainWindowPages
{
    /// <summary>
    /// Логика взаимодействия для SupplyProductsPage.xaml
    /// </summary>
    public partial class SupplyProductsPage : Page
    {
        public SupplyProductsPage()
        {
            InitializeComponent();
            FetchData();
        }

        private void FetchData()
        {
            using (RestaurantManagerContext context = new RestaurantManagerContext())
            {
                var sup = context.SupplyProducts
                    .Include(sp => sp.Product)
                    .Include(sp => sp.Supply)
                    .Include(sp => sp.SupplyNavigation).ToList();
                SupplyProductsList.ItemsSource = sup;
            }
        }

        private void OnEdit(object sender, RoutedEventArgs e)
        {
            var dataContext = ((Button)sender).DataContext;
            SupplyProduct clone = (SupplyProduct)((SupplyProduct)dataContext).Clone();
            EditSupplyProductWindow editSupplyProductWindow = new EditSupplyProductWindow(clone);
            editSupplyProductWindow.ShowDialog();

            if (editSupplyProductWindow.DialogResult == true)
            {
                SupplyProduct sup = (SupplyProduct) editSupplyProductWindow.DataContext;
                using (RestaurantManagerContext context = new RestaurantManagerContext())
                {
                    context.SupplyProducts.Update(sup);
                    context.SaveChanges();
                    FetchData();
                }
            }
        }

        private void OnAdd(object sender, RoutedEventArgs e)
        {
            EditSupplyProductWindow editSupplyProductWindow = new EditSupplyProductWindow(new SupplyProduct());
            editSupplyProductWindow.Title = "Добавление нового продукта в поставке";
            editSupplyProductWindow.ShowDialog();

            if (editSupplyProductWindow.DialogResult == true)
            {
                SupplyProduct sup = (SupplyProduct) editSupplyProductWindow.DataContext;
                using (RestaurantManagerContext context = new RestaurantManagerContext())
                {
                    context.SupplyProducts.Attach(sup);
                    context.SaveChanges();
                    FetchData();
                }
            }
        }

        private void OnDelete(object sender, RoutedEventArgs e)
        {
            SupplyProduct supply = ((SupplyProduct)((Button)sender).DataContext);
            MessageBoxResult result = MessageBox.Show(
                "Вы действительно хотите удалить? \"" + supply.Product.Title + " \" из поставки?",
                "Удаление продукта в поставке",
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes)
                using (RestaurantManagerContext context = new RestaurantManagerContext())
                {
                    context.SupplyProducts.Remove(supply);
                    context.SaveChanges();
                    FetchData();
                }
        }

        private void OnSearch(object sender, RoutedEventArgs e)
        {
            String searchString = SearchField.Text;
            using (RestaurantManagerContext context = new RestaurantManagerContext())
            {
                var sup = context.SupplyProducts
                    .Include(sp => sp.Product)
                    .Include(sp => sp.Supply)
                    .Include(sp => sp.SupplyNavigation)
                    .Where(sp => sp.Product.Title.Contains(searchString) ||
                    sp.Product.Price.ToString().Contains(searchString) ||
                    sp.Supply.Date.ToString().Contains(searchString) ||
                    sp.SupplyNavigation.Title.Contains(searchString)).ToList();

                SupplyProductsList.ItemsSource = sup;
            }
        }
    }
}
