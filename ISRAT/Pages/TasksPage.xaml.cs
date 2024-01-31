using ISRAT.DataSet1TableAdapters;
using ISRAT.Model;
using System;
using System.Collections.Generic;
using System.Data;
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
            ColumnNameBox.ItemsSource =TasksDataGrid.Columns;
            ColumnNameBox.DisplayMemberPath = "Header";
            ColumnNameBox.SelectedValuePath = "DisplayIndex";

            TasksDataGrid.ItemsSource = tasksTableAdapter.GetData();
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

        private bool FieldsCheck()
        {
            if (!string.IsNullOrEmpty(NameBox.Text) && !string.IsNullOrEmpty(DescriptionBox.Text) && !string.IsNullOrEmpty(PriorityBox.Text) 
                && ProjectIDBox.SelectedValue != null && StatusIDBox.SelectedValue != null && ResponsibleUserIDBox.SelectedValue != null)
            {
                return true;
            }
            else
            {
                MessageBox.Show("Заполните все поля!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }

        private void UpdateDataGrid()
        {
            TasksDataGrid.ItemsSource = tasksTableAdapter.GetData();
        }

        private void ClearFields()
        {
            NameBox.Text = string.Empty;
            DescriptionBox.Text = string.Empty;
            PriorityBox.Text = string.Empty;
            StatusIDBox.SelectedValue = null;
            ResponsibleUserIDBox.SelectedValue = null;
            ProjectIDBox.SelectedValue = null;

            StartDatePicker.SelectedDate = null;
            EndDatePicker.SelectedDate = null;
        }

        private void ResourcesDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataRowView taskRowView = TasksDataGrid.SelectedItem as DataRowView;
            if (TasksDataGrid.SelectedItem != null)
            {
                NameBox.Text = taskRowView.Row[1].ToString();
                DescriptionBox.Text = taskRowView.Row[2].ToString();
                PriorityBox.Text = taskRowView.Row[3].ToString();
                StatusIDBox.SelectedValue = taskRowView.Row[4].ToString();
                ResponsibleUserIDBox.SelectedValue = taskRowView.Row[5].ToString();
                ProjectIDBox.SelectedValue = taskRowView.Row[8].ToString();

                StartDatePicker.SelectedDate = DateTime.Parse(taskRowView.Row[6].ToString());
                EndDatePicker.SelectedDate = DateTime.Parse(taskRowView.Row[7].ToString());

            }
        }

        private void AddTaskButton_Click(object sender, RoutedEventArgs e)
        {
            if (FieldsCheck())
            {

                switch (DialogWindow.InsertDialog())
                {
                    case MessageBoxResult.Yes:
                        tasksTableAdapter.InsertQuery(NameBox.Text, DescriptionBox.Text, PriorityBox.Text, (int)StatusIDBox.SelectedValue,
                            (int)ResponsibleUserIDBox.SelectedValue, StartDatePicker.SelectedDate.ToString(), EndDatePicker.SelectedDate.ToString(),
                            (int)ProjectIDBox.SelectedValue);
                        UpdateDataGrid();
                        break;
                }
            }
        }

        private void ChangeResourceButton_Click(object sender, RoutedEventArgs e)
        {
            if (FieldsCheck())
            {
                DataRowView taskRowView = TasksDataGrid.SelectedItem as DataRowView;
                if (TasksDataGrid.SelectedItem != null)
                {

                    switch (DialogWindow.UpdateDialog())
                    {
                        case MessageBoxResult.Yes:
                            tasksTableAdapter.UpdateQuery(NameBox.Text, DescriptionBox.Text, PriorityBox.Text, int.Parse(StatusIDBox.SelectedValue.ToString()),
                            int.Parse(ResponsibleUserIDBox.SelectedValue.ToString()), StartDatePicker.SelectedDate.ToString(), EndDatePicker.SelectedDate.ToString(),
                            int.Parse(ProjectIDBox.SelectedValue.ToString()), int.Parse(taskRowView.Row[0].ToString()));
                            UpdateDataGrid();
                            ClearFields();
                            break;
                    }

                }
                else
                {
                    MessageBox.Show("Выберите запись для изменения", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }

            }
        }

        private void DeleteResourceButton_Click(object sender, RoutedEventArgs e)
        {
            DataRowView taskRowView = TasksDataGrid.SelectedItem as DataRowView;
            if (TasksDataGrid.SelectedItem != null)
            {

                switch (DialogWindow.DeleteDialog())
                {
                    case MessageBoxResult.Yes:
                        tasksTableAdapter.DeleteQuery(int.Parse(taskRowView.Row[0].ToString()));
                        UpdateDataGrid();
                        break;
                }

            }
            else
            {
                MessageBox.Show("Выберите запись для удаления", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void FindButton_Click(object sender, RoutedEventArgs e)
        {
            int result = 0;
            switch (ColumnNameBox.SelectedValue)
            {
                case 0:
                    {
                        int.TryParse(FindBox.Text, out result);
                        TasksDataGrid.ItemsSource = tasksTableAdapter.GetSortedTableByID(result);
                        break;
                    }
                case 1:
                    {
                        TasksDataGrid.ItemsSource = tasksTableAdapter.GetSortedTableByName(FindBox.Text);
                        break;
                    }
                case 2:
                    {
                        TasksDataGrid.ItemsSource = tasksTableAdapter.GetSortedTableByDesc(FindBox.Text);
                        break;
                    }
                case 3:
                    {
                        TasksDataGrid.ItemsSource = tasksTableAdapter.GetSortedTableByPriority(FindBox.Text);
                        break;
                    }
                case 4:
                    {
                        int.TryParse(FindBox.Text, out result);
                        TasksDataGrid.ItemsSource = tasksTableAdapter.GetSortedTableByStatusID(result);
                        break;
                    }
                case 5:
                    {
                        int.TryParse(FindBox.Text, out result);
                        TasksDataGrid.ItemsSource = tasksTableAdapter.GetSortedTableByResponseUserID(result);
                        break;
                    }
                case 6:
                    {
                        TasksDataGrid.ItemsSource = tasksTableAdapter.GetSortedTableByStartDate(FindBox.Text);
                        break;
                    }
                case 7:
                    {
                        TasksDataGrid.ItemsSource = tasksTableAdapter.GetSortedTableByDueDate(FindBox.Text);
                        break;
                    }
                case 8:
                    {
                        int.TryParse(FindBox.Text, out result);
                        TasksDataGrid.ItemsSource = tasksTableAdapter.GetSortedTableByProjectID(result);
                        break;
                    }
            }
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            if (CurrentUser.RoleID == 3)
            {
                AdminStartPage.administrator.ChangeFrame(0);
            }
            else if (CurrentUser.RoleID == 1005)
            {
                SeniorManagerStartPage.seniorManager.ChangeFrame(0);
            }
        }

        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            UpdateDataGrid();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateDataGrid();
        }
    }
}
