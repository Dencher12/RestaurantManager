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
    /// Логика взаимодействия для TableReservesPage.xaml
    /// </summary>
    public partial class TableReservesPage : Page
    {
        public TableReservesPage()
        {
            InitializeComponent();

            TableReserve tr = new TableReserve();
            tr.TableNumber = 1;
            tr.DateTime = DateTime.Now;
            tr.CustomerPhone = "+7 (234) 232-23-46";
            tr.CustomersCount = 3;
            tr.CustomerName = "Иванов Иван";

            List<TableReserve> list = new List<TableReserve>() { tr };

            TableReservesList.ItemsSource = list;
        }
    }
}
