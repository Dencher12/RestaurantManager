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

namespace Restaurant.Windows
{
    /// <summary>
    /// Логика взаимодействия для LoginWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
        }

        private void OnLogin(object sender, RoutedEventArgs e)
        {
            using (RestaurantManagerContext context = new RestaurantManagerContext())
            {
                try
                {
                    User user = context.Users.Where(u => u.Email == EmailBox.Text && u.Password == PasswordBox.Password).Single();
                    Window window;
                    if (user.IsAdmin == "1")
                        window = new AdminWindow(user.Email, "Администратор");
                    else
                        window = new MainWindow(user.Email, "Менеджер");
                        
                    this.Hide();
                    window.Show();
                } catch (InvalidOperationException ex)
                {
                    MessageBox.Show("Email или пароль неверны!", "Ошибка входа", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}
