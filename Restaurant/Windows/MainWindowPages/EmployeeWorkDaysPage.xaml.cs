using Microsoft.EntityFrameworkCore;
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
    /// Логика взаимодействия для EmployeeWorkDaysPage.xaml
    /// </summary>
    public partial class EmployeeWorkDaysPage : Page
    {
        public EmployeeWorkDaysPage()
        {
            InitializeComponent();
        }

        private void FetchData()
        {
            using (RestaurantManagerContext context = new RestaurantManagerContext())
            {
                var sup = context.EmployeeWorkDays
                    .Include(sp => sp.WorkDay)
                    .Include(sp => sp.Employee).ToList();
                EmployeeWorkDaysList.ItemsSource = sup;
            }
        }

        private void OnEdit(object sender, RoutedEventArgs e)
        {
            var dataContext = ((Button)sender).DataContext;
            EmployeeWorkDay clone = (EmployeeWorkDay)((EmployeeWorkDay) dataContext).Clone();
            EditEmploeeWorkDayWindow editEmploeeWorkDayWindow = new EditEmploeeWorkDayWindow(clone);
            editEmploeeWorkDayWindow.ShowDialog();

            if (editEmploeeWorkDayWindow.DialogResult == true)
            {
                EmployeeWorkDay sup = (EmployeeWorkDay) editEmploeeWorkDayWindow.DataContext;
                using (RestaurantManagerContext context = new RestaurantManagerContext())
                {
                    context.EmployeeWorkDays.Update(sup);
                    context.SaveChanges();
                    FetchData();
                }
            }
        }

        private void OnAdd(object sender, RoutedEventArgs e)
        {
            EditEmploeeWorkDayWindow editSupplyProductWindow = new EditEmploeeWorkDayWindow(new EmployeeWorkDay());
            editSupplyProductWindow.Title = "Добавление сотрудника в рабочий день";
            editSupplyProductWindow.ShowDialog();

            if (editSupplyProductWindow.DialogResult == true)
            {
                EmployeeWorkDay sup = (EmployeeWorkDay) editSupplyProductWindow.DataContext;
                using (RestaurantManagerContext context = new RestaurantManagerContext())
                {
                    context.EmployeeWorkDays.Attach(sup);
                    context.SaveChanges();
                    FetchData();
                }
            }
        }

        private void OnDelete(object sender, RoutedEventArgs e)
        {
            EmployeeWorkDay sup = ((EmployeeWorkDay)((Button) sender).DataContext);
            MessageBoxResult result = MessageBox.Show(
                "Вы действительно хотите удалить сотрудника? \"" + sup.Employee.FullName + " \" из рабочего дня?",
                "Удаление сотрудника из рабочего дня",
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes)
                using (RestaurantManagerContext context = new RestaurantManagerContext())
                {
                    context.EmployeeWorkDays.Remove(sup);
                    context.SaveChanges();
                    FetchData();
                }
        }

        private void OnSearch(object sender, RoutedEventArgs e)
        {
            String searchString = SearchField.Text;
            using (RestaurantManagerContext context = new RestaurantManagerContext())
            {
                var sup = context.EmployeeWorkDays
                    .Include(sp => sp.WorkDay)
                    .Include(sp => sp.Employee)
                    .Where(sp => sp.Employee.FullName.Contains(searchString) ||
                           sp.WorkDay.Date.ToString().Contains(searchString) ||
                           sp.TargetWorkTime.ToString().Contains(searchString) ||
                           sp.RealWorkTime.ToString().Contains(searchString)).ToList();

                EmployeeWorkDaysList.ItemsSource = sup;
            }
        }
    }
}
