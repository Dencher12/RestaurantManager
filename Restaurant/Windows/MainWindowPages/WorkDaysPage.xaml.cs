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
    /// Логика взаимодействия для WorkDaysPage.xaml
    /// </summary>
    public partial class WorkDaysPage : Page
    {
        public WorkDaysPage()
        {
            InitializeComponent();
            FetchData();
        }

        private void FetchData()
        {
            using (RestaurantManagerContext context = new RestaurantManagerContext())
            {
                var workDays = context.WorkDays.ToList();
                WorkDaysList.ItemsSource = workDays;
            }
        }

        private void OnEdit(object sender, RoutedEventArgs e)
        {
            var dataContext = ((Button) sender).DataContext;
            WorkDay clone = (WorkDay)((WorkDay) dataContext).Clone();
            EditWorkDay editWorkDay = new EditWorkDay() { DataContext = clone };
            editWorkDay.ShowDialog();

            if (editWorkDay.DialogResult == true)
            {
                WorkDay workDay = (WorkDay) editWorkDay.DataContext;
                using (RestaurantManagerContext context = new RestaurantManagerContext())
                {
                    context.WorkDays.Update(workDay);
                    context.SaveChanges();
                    FetchData();
                }
            }
        }

        private void OnAdd(object sender, RoutedEventArgs e)
        {
            EditWorkDay editWorkDay = new EditWorkDay() { DataContext = new WorkDay() };
            editWorkDay.Title = "Добавление рабочего дня";
            editWorkDay.ShowDialog();

            if (editWorkDay.DialogResult == true)
            {
                WorkDay workDay = (WorkDay) editWorkDay.DataContext;
                using (RestaurantManagerContext context = new RestaurantManagerContext())
                {
                    context.WorkDays.Add(workDay);
                    context.SaveChanges();
                    FetchData();
                }
            }
        }

        private void OnDelete(object sender, RoutedEventArgs e)
        {
            WorkDay workDay = ((WorkDay)((Button) sender).DataContext);
            MessageBoxResult result = MessageBox.Show(
                "Вы действительно хотите удалить рабочий день \"" + workDay.Date + " \" ?",
                "Удаление рабочего дня",
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes)
                using (RestaurantManagerContext context = new RestaurantManagerContext())
                {
                    context.WorkDays.Remove(workDay);
                    context.SaveChanges();
                    FetchData();
                }
        }



        private void OnSearch(object sender, RoutedEventArgs e)
        {
            String searchString = SearchField.Text;
            using (RestaurantManagerContext context = new RestaurantManagerContext())
            {
                var workDays = context.WorkDays.Where(u => u.Date.ToString().Contains(searchString) ||
                                                     u.MorningCash.ToString().Contains(searchString) ||
                                                     u.EveningCash.ToString().Contains(searchString)).ToList();


                WorkDaysList.ItemsSource = workDays;
            }
        }
    }
}
