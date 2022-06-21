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
    /// Логика взаимодействия для WorkDayPage.xaml
    /// </summary>
    public partial class WorkDayPage : Page
    {
        public WorkDayPage()
        {
            InitializeComponent();
            List<Employee> emps = new List<Employee>() { new Employee() };
            Employees.ItemsSource = emps;
        }
    }
}
