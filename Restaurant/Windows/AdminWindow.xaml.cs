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
using System.Windows.Shapes;

namespace Restaurant.Windows
{
    /// <summary>
    /// Логика взаимодействия для AdminWindow.xaml
    /// </summary>
    public partial class AdminWindow : Window
    {
        public AdminWindow(string email, string role)
        {
            InitializeComponent();
            this.Title = $"{email} ({role})";
            FetchData();
        }

        private void FetchData()
        {
            using (RestaurantManagerContext context = new RestaurantManagerContext())
            {
                var users = context.Users.ToList();
                UsersList.ItemsSource = users;
            }
        }

        private void OnEdit(object sender, RoutedEventArgs e)
        {
            var dataContext = ((Button)sender).DataContext;
            User clone = (User)((User)dataContext).Clone();
            EditUserWindow editUserWindow = new EditUserWindow() { DataContext = clone };
            editUserWindow.PasswordField.Password = clone.Password;
         
            editUserWindow.ShowDialog();
            if(editUserWindow.DialogResult == true)
            {
                User user = (User) editUserWindow.DataContext;
                using (RestaurantManagerContext context = new RestaurantManagerContext())
                {
                    context.Users.Update(user);
                    context.SaveChanges();
                    FetchData();
                }
            }
        }

        private void OnAdd(object sender, RoutedEventArgs e)
        {
            EditUserWindow editUserWindow = new EditUserWindow() { DataContext = new User() };
            editUserWindow.Title = "Добавление нового пользователя";
            editUserWindow.ShowDialog();

            if (editUserWindow.DialogResult == true)
            {
                User user = (User) editUserWindow.DataContext;
                using (RestaurantManagerContext context = new RestaurantManagerContext())
                {
                    context.Users.Add(user);
                    context.SaveChanges();
                    FetchData();
                }
            }
        }

        private void OnDelete(object sender, RoutedEventArgs e)
        {
            User user = ((User)((Button)sender).DataContext);
            MessageBoxResult result = MessageBox.Show(
                "Вы действительно хотите удалить пользователя \"" + user.FullName + " \" ?", 
                "Удаление пользователя", 
                MessageBoxButton.YesNo, 
                MessageBoxImage.Warning);

            if(result == MessageBoxResult.Yes)
                using (RestaurantManagerContext context = new RestaurantManagerContext())
                {
                    context.Users.Remove(user);
                    context.SaveChanges();
                    FetchData();
                }
        }



        private void OnSearch(object sender, RoutedEventArgs e)
        {
            String searchString = SearchField.Text;
            using (RestaurantManagerContext context = new RestaurantManagerContext())
            {
                var users = context.Users.Where(u => u.FullName.Contains(searchString) ||
                                                     u.Email.Contains(searchString) ||
                                                     u.Phone.Contains(searchString)).ToList();


                UsersList.ItemsSource = users;
            }
        }
    }
}
