using Microsoft.EntityFrameworkCore;
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

namespace Restaurant.Windows.MainWindowPages
{
    /// <summary>
    /// Логика взаимодействия для StatisticPage.xaml
    /// </summary>
    public partial class StatisticPage : Page
    {
        public StatisticPage()
        {
            InitializeComponent();

            using (RestaurantManagerContext context = new RestaurantManagerContext())
            {
                var employees = context.Employees.Include(e => e.EmployeeWorkDays).ToList();
                WorkHoursList.ItemsSource = employees;
            }

            IncomeStatisticList.ItemsSource = Stats.stats;
        }

        private void ComboBox_DropDownClosed(object sender, EventArgs e)
        {
            using (RestaurantManagerContext context = new RestaurantManagerContext())
            {
                var employees = context.Employees.Include(e => e.EmployeeWorkDays).ToList();
                WorkHoursList.ItemsSource = employees;
            }
        }
    }

}
