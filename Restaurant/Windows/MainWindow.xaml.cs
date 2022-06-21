using Restaurant.Windows;
using Restaurant.Windows.MainWindowPages;
using Restaurant.Windows.MainWindowPages.EditWindows;
using System.Windows;
namespace Restaurant
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow(string email, string role)
        {
            InitializeComponent();
            this.Title = $"{email} ({role})";
            WorkDaysPage p = new WorkDaysPage();
            this.Content.NavigationService.Navigate(p);
        }

        private void OpenReservePage(object sender, RoutedEventArgs e)
        {
            TableReservesPage p = new TableReservesPage();
            this.Content.NavigationService.Navigate(p);
        }

        private void OpenSuppliesPage(object sender, RoutedEventArgs e)
        {
            SuppliesPage p = new SuppliesPage();
            this.Content.NavigationService.Navigate(p);
        }

        private void OpenWorkDays(object sender, RoutedEventArgs e)
        {
            WorkDaysPage p = new WorkDaysPage();
            this.Content.NavigationService.Navigate(p);
        }

        private void OpenProductsPage(object sender, RoutedEventArgs e)
        {
            ProductsPage p = new ProductsPage();
            this.Content.NavigationService.Navigate(p);
        }

        private void OpenStatusesPage(object sender, RoutedEventArgs e)
        {
            StatusesPage p = new StatusesPage();
            this.Content.NavigationService.Navigate(p);
        }

        private void OpenSupplyProductsPage(object sender, RoutedEventArgs e)
        {
            SupplyProductsPage p = new SupplyProductsPage();
            this.Content.NavigationService.Navigate(p);
        }

        private void OpenPostsPage(object sender, RoutedEventArgs e)
        {
            PostsPage p = new PostsPage();
            this.Content.NavigationService.Navigate(p);
        }

        private void OpenEmployeesPage(object sender, RoutedEventArgs e)
        {
            EmployeesPage p = new EmployeesPage();
            this.Content.NavigationService.Navigate(p);
        }

        private void OpenEmpWorkDays(object sender, RoutedEventArgs e)
        {
            EmployeeWorkDaysPage p = new EmployeeWorkDaysPage();
            this.Content.NavigationService.Navigate(p);
        }

        private void OpenStatisticPage(object sender, RoutedEventArgs e)
        {
            StatisticPage p = new StatisticPage();
            this.Content.NavigationService.Navigate(p);
        }
    }
}
