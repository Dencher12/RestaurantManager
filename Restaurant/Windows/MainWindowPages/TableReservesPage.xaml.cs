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
    /// Логика взаимодействия для TableReservesPage.xaml
    /// </summary>
    public partial class TableReservesPage : Page
    {
        public TableReservesPage()
        {
            InitializeComponent();
            FetchData();
        }

        private void FetchData()
        {
            using (RestaurantManagerContext context = new RestaurantManagerContext())
            {
                var sup = context.TableReserves.ToList();
                TableReservesList.ItemsSource = sup;
            }
        }

        private void OnEdit(object sender, RoutedEventArgs e)
        {
            var dataContext = ((Button)sender).DataContext;
            TableReserve clone = (TableReserve)((TableReserve)dataContext).Clone();
            EditTableReserveWindow editSupplyWindow = new EditTableReserveWindow() { DataContext = clone };
            editSupplyWindow.ShowDialog();

            if (editSupplyWindow.DialogResult == true)
            {
                TableReserve sup = (TableReserve) editSupplyWindow.DataContext;
                using (RestaurantManagerContext context = new RestaurantManagerContext())
                {
                    context.TableReserves.Update(sup);
                    context.SaveChanges();
                    FetchData();
                }
            }
        }

        private void OnAdd(object sender, RoutedEventArgs e)
        {
            EditTableReserveWindow editSupplyWindow = new EditTableReserveWindow() { DataContext = new TableReserve() };
            editSupplyWindow.Title = "Добавление брони столика";
            editSupplyWindow.ShowDialog();

            if (editSupplyWindow.DialogResult == true)
            {
                TableReserve sup = (TableReserve) editSupplyWindow.DataContext;
                using (RestaurantManagerContext context = new RestaurantManagerContext())
                {
                    context.TableReserves.Add(sup);
                    context.SaveChanges();
                    FetchData();
                }
            }
        }

        private void OnDelete(object sender, RoutedEventArgs e)
        {
            TableReserve supply = ((TableReserve)((Button) sender).DataContext);
            MessageBoxResult result = MessageBox.Show(
                "Вы действительно хотите удалить бронь?",
                "Удаление брони",
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes)
                using (RestaurantManagerContext context = new RestaurantManagerContext())
                {
                    context.TableReserves.Remove(supply);
                    context.SaveChanges();
                    FetchData();
                }
        }



        private void OnSearch(object sender, RoutedEventArgs e)
        {
            String searchString = SearchField.Text;
            using (RestaurantManagerContext context = new RestaurantManagerContext())
            {
                //var sup = context.TableReserves.Where(u => u.Date.ToString().Contains(searchString)).ToList();
                //TableReservesList.ItemsSource = sup;
            }
        }
    }
}
