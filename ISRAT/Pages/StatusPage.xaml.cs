using ISRAT.DataSet1TableAdapters;
using ISRAT.Windows;
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
    /// Логика взаимодействия для StatusPage.xaml
    /// </summary>
    public partial class StatusPage : Page
    {
        private StatusTableAdapter statusTableAdapter = new StatusTableAdapter();
        public StatusPage()
        {
            InitializeComponent();
            StatusDataGrid.ItemsSource = statusTableAdapter.GetData();
            ColumnNameBox.ItemsSource = StatusDataGrid.Columns;
            ColumnNameBox.DisplayMemberPath = "Header";
            ColumnNameBox.SelectedValuePath = "DisplayIndex";
        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            
        }
        private bool IsFieldsEmpty()
        {
            if (!string.IsNullOrEmpty(NameBox.Text))
            {
                return true;
            }
            else
            {
                MessageBox.Show("Заполните все поля!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }

        private void StatusDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataRowView resourceRowView = StatusDataGrid.SelectedItem as DataRowView;
            if (StatusDataGrid.SelectedItem != null)
            {
                NameBox.Text = resourceRowView.Row[1].ToString();
            }
        }

        private void AddStatusButton_Click(object sender, RoutedEventArgs e)
        {
            if (IsFieldsEmpty())
            {
                string sMessageBoxText = "Вы уверены, что хотите добавить запись?";
                string sCaption = "Добавление";

                MessageBoxButton btnMessageBox = MessageBoxButton.YesNo;
                MessageBoxImage icnMessageBox = MessageBoxImage.Warning;

                MessageBoxResult rsltMessageBox = MessageBox.Show(sMessageBoxText, sCaption, btnMessageBox, icnMessageBox);

                switch (rsltMessageBox)
                {
                    case MessageBoxResult.Yes:
                        statusTableAdapter.InsertQuery(NameBox.Text);
                        UpdateDataGrid();
                        break;
                }
            }
            else
            {
                MessageBox.Show("Заполните все поля!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ChangeStatusButton_Click(object sender, RoutedEventArgs e)
        {
            if (IsFieldsEmpty())
            {
                DataRowView resourceRowView = StatusDataGrid.SelectedItem as DataRowView;
                if (StatusDataGrid.SelectedItem != null)
                {
                    string sMessageBoxText = "Вы уверены, что хотите изменить запись?";
                    string sCaption = "Изменение";

                    MessageBoxButton btnMessageBox = MessageBoxButton.YesNo;
                    MessageBoxImage icnMessageBox = MessageBoxImage.Warning;

                    MessageBoxResult rsltMessageBox = MessageBox.Show(sMessageBoxText, sCaption, btnMessageBox, icnMessageBox);

                    switch (rsltMessageBox)
                    {
                        case MessageBoxResult.Yes:
                            statusTableAdapter.UpdateQuery(NameBox.Text, int.Parse(resourceRowView.Row[0].ToString()));
                            UpdateDataGrid();
                            break;
                    }

                }
                else
                {
                    MessageBox.Show("Выберите запись для изменения", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }

            }
        }

        private void DeleteStatusButton_Click(object sender, RoutedEventArgs e)
        {
            DataRowView resourceRowView = StatusDataGrid.SelectedItem as DataRowView;
            if (StatusDataGrid.SelectedItem != null)
            {
                string sMessageBoxText = "Вы уверены, что хотите удалить запись?";
                string sCaption = "Удаление";

                MessageBoxButton btnMessageBox = MessageBoxButton.YesNo;
                MessageBoxImage icnMessageBox = MessageBoxImage.Warning;

                MessageBoxResult rsltMessageBox = MessageBox.Show(sMessageBoxText, sCaption, btnMessageBox, icnMessageBox);

                switch (rsltMessageBox)
                {
                    case MessageBoxResult.Yes:
                        statusTableAdapter.DeleteQuery(int.Parse(resourceRowView.Row[0].ToString()));
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
                        StatusDataGrid.ItemsSource = statusTableAdapter.GetSortedTableByID(result);
                        break;
                    }
                case 1:
                    {
                        StatusDataGrid.ItemsSource = statusTableAdapter.GetSortedTableByName(FindBox.Text);
                        break;
                    }
            }

        }
        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            AdminStartPage.administrator.ChangeFrame(0);
        }

        private void UpdateDataGrid()
        {
            StatusDataGrid.ItemsSource = statusTableAdapter.GetData();
        }

        
    }
}
