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
    /// Логика взаимодействия для EmployeesPage.xaml
    /// </summary>
    public partial class EmployeesPage : Page
    {
        public EmployeesPage()
        {
            InitializeComponent();
            FetchData();
        }

        private void FetchData()
        {
            using (RestaurantManagerContext context = new RestaurantManagerContext())
            {
                var employees = context.Employees.Include(e => e.Post).ToList();
                EmployeesList.ItemsSource = employees;
            }
        }

        private void OnEdit(object sender, RoutedEventArgs e)
        {
            var dataContext = ((Button)sender).DataContext;
            Employee clone = (Employee)((Employee)dataContext).Clone();
            EditEmployeeWindow editEmployeeWindow = new EditEmployeeWindow(clone);
            editEmployeeWindow.ShowDialog();

            if (editEmployeeWindow.DialogResult == true)
            {
                Employee employee = (Employee) editEmployeeWindow.DataContext;
                using (RestaurantManagerContext context = new RestaurantManagerContext())
                {
                    context.Employees.Update(employee);
                    context.SaveChanges();
                    FetchData();
                }
            }
        }

        private void OnAdd(object sender, RoutedEventArgs e)
        {
            EditEmployeeWindow editEmployeeWindow = new EditEmployeeWindow(new Employee());
            editEmployeeWindow.Title = "Добавление нового сотрудника";
            editEmployeeWindow.ShowDialog();

            if (editEmployeeWindow.DialogResult == true)
            {
                Employee employee = (Employee) editEmployeeWindow.DataContext;
                using (RestaurantManagerContext context = new RestaurantManagerContext())
                {
                    context.Employees.Attach(employee);
                    context.SaveChanges();
                    FetchData();
                }
            }
        }

        private void OnDelete(object sender, RoutedEventArgs e)
        {
            Employee employee = ((Employee)((Button)sender).DataContext);
            MessageBoxResult result = MessageBox.Show(
                "Вы действительно хотите удалить сотрудника \"" + employee.FullName + " \" ?",
                "Удаление сотрудника",
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes)
                using (RestaurantManagerContext context = new RestaurantManagerContext())
                {
                    context.Employees.Remove(employee);
                    context.SaveChanges();
                    FetchData();
                }
        }

        private void OnSearch(object sender, RoutedEventArgs e)
        {
            String searchString = SearchField.Text;
            using (RestaurantManagerContext context = new RestaurantManagerContext())
            {
                var employees = context.Employees.Where(u => 
                u.FullName.Contains(searchString) || 
                u.Salary.ToString().Contains(searchString) || 
                u.Post.Title.Contains(searchString)).ToList();

                EmployeesList.ItemsSource = employees;
            }
        }
    }
}
