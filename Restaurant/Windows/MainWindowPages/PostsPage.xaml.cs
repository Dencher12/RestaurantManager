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
    /// Логика взаимодействия для PostsPage.xaml
    /// </summary>
    public partial class PostsPage : Page
    {
        public PostsPage()
        {
            InitializeComponent();
            FetchData();
        }

        private void FetchData()
        {
            using (RestaurantManagerContext context = new RestaurantManagerContext())
            {
                var posts = context.Posts.ToList();
                PostsList.ItemsSource = posts;
            }
        }

        private void OnEdit(object sender, RoutedEventArgs e)
        {
            var dataContext = ((Button)sender).DataContext;
            Post clone = (Post)((Post)dataContext).Clone();
            EditPostWindow postsWindow = new EditPostWindow() { DataContext = clone };
            postsWindow.ShowDialog();

            if (postsWindow.DialogResult == true)
            {
                Post post = (Post) postsWindow.DataContext;
                using (RestaurantManagerContext context = new RestaurantManagerContext())
                {
                    context.Posts.Update(post);
                    context.SaveChanges();
                    FetchData();
                }
            }
        }

        private void OnAdd(object sender, RoutedEventArgs e)
        {
            EditPostWindow postsWindow = new EditPostWindow() { DataContext = new Post() };
            postsWindow.Title = "Добавление новой должности";
            postsWindow.ShowDialog();

            if (postsWindow.DialogResult == true)
            {
                Post post = (Post) postsWindow.DataContext;
                using (RestaurantManagerContext context = new RestaurantManagerContext())
                {
                    context.Posts.Add(post);
                    context.SaveChanges();
                    FetchData();
                }
            }
        }

        private void OnDelete(object sender, RoutedEventArgs e)
        {
            Post post = ((Post)((Button) sender).DataContext);
            MessageBoxResult result = MessageBox.Show(
                "Вы действительно хотите должность \"" + post.Title + " \" ?",
                "Удаление должности",
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes)
                using (RestaurantManagerContext context = new RestaurantManagerContext())
                {
                    context.Posts.Remove(post);
                    context.SaveChanges();
                    FetchData();
                }
        }

        private void OnSearch(object sender, RoutedEventArgs e)
        {
            String searchString = SearchField.Text;
            using (RestaurantManagerContext context = new RestaurantManagerContext())
            {
                var products = context.Posts.Where(u => u.Title.Contains(searchString)).ToList();
                PostsList.ItemsSource = products;
            }
        }
    }
}
