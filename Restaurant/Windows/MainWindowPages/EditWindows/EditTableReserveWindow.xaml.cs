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
    /// Логика взаимодействия для EditTableReserveWindow.xaml
    /// </summary>
    public partial class EditTableReserveWindow : Window
    {
        public EditTableReserveWindow()
        {
            InitializeComponent();
        }

        private void OnSave(object sender, RoutedEventArgs e)
        {
            TableReserve tableReserve = (TableReserve) DataContext;
            tableReserve.DateTime = DateTime.Parse(datePick.Text);

            this.DialogResult = true;
            this.Close();
        }

        private void OnCancel(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
