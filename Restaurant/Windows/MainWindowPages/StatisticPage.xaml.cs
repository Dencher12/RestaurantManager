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
            IncomeStatisticList.ItemsSource = new List<Stat>() { new Stat("Январь", 303234), new Stat("Февраль", 454234), new Stat("Март", 504234) };
            WorkHoursStatisticList.ItemsSource = new List<Stat2>() { new Stat2("Нечаева Лидия Анатолиевна", "120/128"), new Stat2("Шкригунов Руслан Чеславович", "128/128") };
        }
    }

    class Stat
    {
        public String Month { get; set; }
        public double Sum { get; set; }

        public Stat(String month, double sum)
        {
            this.Month = month;
            this.Sum = sum;
        }
    }

    class Stat2
    {
        public String Name { get; set; }
        public String Hours { get; set; }

        public Stat2(String name, String hours)
        {
            this.Name = name;
            this.Hours = hours;
        }
    }
}
