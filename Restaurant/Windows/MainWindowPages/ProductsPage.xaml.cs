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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Restaurant.Windows.MainWindowPages
{
    /// <summary>
    /// Логика взаимодействия для ProductsPage.xaml
    /// </summary>
    public partial class ProductsPage : Page
    {
        public ProductsPage()
        {
            InitializeComponent();
            FetchData();
        }

        private void FetchData()
        {
            using (RestaurantManagerContext context = new RestaurantManagerContext())
            {
                var products = context.Products.ToList();
                ProductsList.ItemsSource = products;
            }
        }

        private void OnEdit(object sender, RoutedEventArgs e)
        {
            var dataContext = ((Button)sender).DataContext;
            Product clone = (Product)((Product)dataContext).Clone();
            EditProductWindow editProductWindow = new EditProductWindow() { DataContext = clone };
            editProductWindow.ShowDialog();

            if (editProductWindow.DialogResult == true)
            {
                Product product = (Product) editProductWindow.DataContext;
                using (RestaurantManagerContext context = new RestaurantManagerContext())
                {
                    context.Products.Update(product);
                    context.SaveChanges();
                    FetchData();
                }
            }
        }

        private void OnAdd(object sender, RoutedEventArgs e)
        {
            EditProductWindow editProductWindow = new EditProductWindow() { DataContext = new Product() };
            editProductWindow.Title = "Добавление нового продукта";
            editProductWindow.ShowDialog();

            if (editProductWindow.DialogResult == true)
            {
                Product product = (Product) editProductWindow.DataContext;
                using (RestaurantManagerContext context = new RestaurantManagerContext())
                {
                    context.Products.Add(product);
                    context.SaveChanges();
                    FetchData();
                }
            }
        }

        private void OnDelete(object sender, RoutedEventArgs e)
        {
            Product product = ((Product)((Button)sender).DataContext);
            MessageBoxResult result = MessageBox.Show(
                "Вы действительно хотите удалить продукт \"" + product.Title + " \" ?",
                "Удаление пользователя",
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes)
                using (RestaurantManagerContext context = new RestaurantManagerContext())
                {
                    context.Products.Remove(product);
                    context.SaveChanges();
                    FetchData();
                }
        }



        private void OnSearch(object sender, RoutedEventArgs e)
        {
            String searchString = SearchField.Text;
            using (RestaurantManagerContext context = new RestaurantManagerContext())
            {
                var products = context.Products.Where(u => u.Title.Contains(searchString) ||
                                                     u.Price.ToString().Contains(searchString) ||
                                                     u.Unit.Contains(searchString)).ToList();


                ProductsList.ItemsSource = products;
            }
        }
    }
}
