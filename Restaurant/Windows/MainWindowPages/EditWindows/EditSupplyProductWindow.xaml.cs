using Restaurant.Models;
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

namespace Restaurant.Windows.MainWindowPages.EditWindows
{
    /// <summary>
    /// Логика взаимодействия для EditSupplyProductWindow.xaml
    /// </summary>
    public partial class EditSupplyProductWindow : Window
    {
        public EditSupplyProductWindow(SupplyProduct sp)
        {
            InitializeComponent();
            DataContext = sp.Clone();
            FetchData();
        }

        private void FetchData()
        {
            using (RestaurantManagerContext context = new RestaurantManagerContext())
            {
                ProductsBox.ItemsSource = context.Products.ToList();
                SuppliesBox.ItemsSource = context.Supplies.ToList();
                StatusesBox.ItemsSource = context.SupplyStatuses.ToList();

                SupplyProduct sp = (SupplyProduct) DataContext;
                ProductsBox.SelectedItem = context.Products.Find(sp.ProductId);
                SuppliesBox.SelectedItem = context.Supplies.Find(sp.SupplyId);
                StatusesBox.SelectedItem = context.SupplyStatuses.Find(sp.SupplyStatusId);
            }
        }

        private void OnSave(object sender, RoutedEventArgs e)
        {
            SupplyProduct sp = (SupplyProduct) DataContext;
            sp.Product = (Product) ProductsBox.SelectedItem;
            sp.Supply = (Supply) SuppliesBox.SelectedItem;
            sp.SupplyNavigation = (SupplyStatus) StatusesBox.SelectedItem;

            this.DialogResult = true;
            this.Close();
        }

        private void OnCancel(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
