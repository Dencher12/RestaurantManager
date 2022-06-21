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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Restaurant.Windows.MainWindowPages.EditWindows
{
    /// <summary>
    /// Логика взаимодействия для EditEmploeeWorkDayWindow.xaml
    /// </summary>
    public partial class EditEmploeeWorkDayWindow : Window
    {
        public EditEmploeeWorkDayWindow(EmployeeWorkDay sp)
        {
            InitializeComponent();
            DataContext = sp.Clone();
            FetchData();
        }

        private void FetchData()
        {
            using (RestaurantManagerContext context = new RestaurantManagerContext())
            {
                WorkDayBox.ItemsSource = context.WorkDays.ToList();
                EmployeeBox.ItemsSource = context.Employees.ToList();

                EmployeeWorkDay sp = (EmployeeWorkDay) DataContext;
                WorkDayBox.SelectedItem = context.WorkDays.Find(sp.WorkDayId);
                EmployeeBox.SelectedItem = context.Employees.Find(sp.EmployeeId);
            }
        }

        private void OnSave(object sender, RoutedEventArgs e)
        {
            EmployeeWorkDay sp = (EmployeeWorkDay)DataContext;
            sp.WorkDay = (WorkDay)WorkDayBox.SelectedItem;
            sp.Employee = (Employee)EmployeeBox.SelectedItem;

            this.DialogResult = true;
            this.Close();
        }

        private void OnCancel(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
