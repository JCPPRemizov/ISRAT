using ISRAT.DataSet1TableAdapters;
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

namespace ISRAT.Pages
{
    /// <summary>
    /// Логика взаимодействия для TasksPage.xaml
    /// </summary>
    public partial class TasksPage : Page
    {
        TasksTableAdapter tasksTableAdapter = new TasksTableAdapter();
        ProjectsTableAdapter projectsTableAdapter = new ProjectsTableAdapter();
        UsersTableAdapter usersTableAdapter = new UsersTableAdapter();
        StatusTableAdapter statusTableAdapter = new StatusTableAdapter();
        public TasksPage()
        {
            InitializeComponent();       
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            ResourcesDataGrid.ItemsSource = tasksTableAdapter.GetData();
            ProjectIDBox.ItemsSource = projectsTableAdapter.GetData();
            ProjectIDBox.SelectedValuePath = "ID";
            ProjectIDBox.DisplayMemberPath = "ID";

            StatusIDBox.ItemsSource = statusTableAdapter.GetData();
            StatusIDBox.SelectedValuePath = "ID";
            StatusIDBox.DisplayMemberPath = "ID";

            ResponsibleUserIDBox.ItemsSource = usersTableAdapter.GetData();
            ResponsibleUserIDBox.SelectedValuePath = "ID";
            ResponsibleUserIDBox.DisplayMemberPath = "ID";
        }

        private void ResourcesDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void AddResourceButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ChangeResourceButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DeleteResourceButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void FindButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            AdminStartPage.administrator.ChangeFrame(0);
        }
    }
}
