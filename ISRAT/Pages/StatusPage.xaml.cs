﻿using ISRAT.DataSet1TableAdapters;
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
            UpdateDataGrid();
        }
        private bool FieldsCheck()
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
            if (FieldsCheck())
            {

                switch (DialogWindow.InsertDialog())
                {
                    case MessageBoxResult.Yes:
                        statusTableAdapter.InsertQuery(NameBox.Text);
                        UpdateDataGrid();
                        break;
                }
            }
        }

        private void ChangeStatusButton_Click(object sender, RoutedEventArgs e)
        {
            if (FieldsCheck())
            {
                DataRowView resourceRowView = StatusDataGrid.SelectedItem as DataRowView;
                if (StatusDataGrid.SelectedItem != null)
                {

                    switch (DialogWindow.UpdateDialog())
                    {
                        case MessageBoxResult.Yes:
                            statusTableAdapter.UpdateQuery(NameBox.Text, int.Parse(resourceRowView.Row[0].ToString()));
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

        private void DeleteStatusButton_Click(object sender, RoutedEventArgs e)
        {
            DataRowView resourceRowView = StatusDataGrid.SelectedItem as DataRowView;
            if (StatusDataGrid.SelectedItem != null)
            {
                switch (DialogWindow.DeleteDialog())
                {
                    case MessageBoxResult.Yes:
                        try
                        {
                            statusTableAdapter.DeleteQuery(int.Parse(resourceRowView.Row[0].ToString()));
                            UpdateDataGrid();
                        }
                        catch
                        {
                            MessageBox.Show("Ошибка удаления!\nИмеются связанные таблицы!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
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

        private void ClearFields()
        {
            NameBox.Text = string.Empty;
        }


    }
}
