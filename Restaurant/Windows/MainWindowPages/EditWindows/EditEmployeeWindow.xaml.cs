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
    /// Логика взаимодействия для EditEmployeeWindow.xaml
    /// </summary>
    public partial class EditEmployeeWindow : Window
    {
        public EditEmployeeWindow(Employee e)
        {
            InitializeComponent();
            DataContext = e.Clone();
            FetchData();
        }

        private void FetchData()
        {
            using (RestaurantManagerContext context = new RestaurantManagerContext())
            {
                PostsBox.ItemsSource = context.Posts.ToList();
                Employee emp = (Employee) DataContext;
                PostsBox.SelectedItem = context.Posts.Find(emp.PostId);
            }
        }

        private void OnSave(object sender, RoutedEventArgs e)
        {
            Employee emp = (Employee) DataContext;
            emp.Post = (Post) PostsBox.SelectedItem;

            this.DialogResult = true;
            this.Close();
        }

        private void OnCancel(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
