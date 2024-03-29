﻿using ISRAT.DataSet1TableAdapters;
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
    /// Логика взаимодействия для ProjectsPage.xaml
    /// </summary>
    public partial class ProjectsPage : Page
    {
        ProjectsTableAdapter projectsTableAdapter =new ProjectsTableAdapter();
        public ProjectsPage()
        {
            InitializeComponent();
            
        }

        private bool FieldsCheck()
        {
            if (!string.IsNullOrEmpty(NameBox.Text) && !string.IsNullOrEmpty(DescriptionBox.Text))
            {
                return true;
            }
            else
            {
                MessageBox.Show("Заполните все поля!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            UpdateDataGrid();
            ColumnNameBox.ItemsSource = ProjectsDataGrid.Columns;
            ColumnNameBox.DisplayMemberPath = "Header";
            ColumnNameBox.SelectedValuePath = "DisplayIndex";
        }

        private void StatusDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataRowView projectRowView = ProjectsDataGrid.SelectedItem as DataRowView;
            if (ProjectsDataGrid.SelectedItem != null)
            {
                NameBox.Text = projectRowView.Row[1].ToString();
                DescriptionBox.Text = projectRowView.Row[2].ToString();
            }
        }

        private void AddProjectButton_Click(object sender, RoutedEventArgs e)
        {
            if (FieldsCheck())
            {

                switch (DialogWindow.InsertDialog())
                {
                    case MessageBoxResult.Yes:
                        projectsTableAdapter.InsertQuery(NameBox.Text, DescriptionBox.Text);
                        UpdateDataGrid();
                        break;
                }
            }
        }

        private void ChangeProjectButton_Click(object sender, RoutedEventArgs e)
        {
            if (FieldsCheck())
            {
                DataRowView projectRowView = ProjectsDataGrid.SelectedItem as DataRowView;
                if (ProjectsDataGrid.SelectedItem != null)
                {

                    switch (DialogWindow.UpdateDialog())
                    {
                        case MessageBoxResult.Yes:
                            projectsTableAdapter.UpdateQuery(NameBox.Text, DescriptionBox.Text, int.Parse(projectRowView.Row[0].ToString()));
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

        private void DeleteProjectButton_Click(object sender, RoutedEventArgs e)
        {
            DataRowView projectRowView = ProjectsDataGrid.SelectedItem as DataRowView;
            if (ProjectsDataGrid.SelectedItem != null)
            {

                switch (DialogWindow.DeleteDialog())
                {
                    case MessageBoxResult.Yes:
                        projectsTableAdapter.DeleteQuery(int.Parse(projectRowView.Row[0].ToString()));
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
                        ProjectsDataGrid.ItemsSource = projectsTableAdapter.GetSortedTableByID(result);
                        break;
                    }
                case 1:
                    {
                        ProjectsDataGrid.ItemsSource = projectsTableAdapter.GetSortedTableByName(FindBox.Text);
                        break;
                    }
                case 2:
                    {
                        ProjectsDataGrid.ItemsSource = projectsTableAdapter.GetSortedTableByDesc(FindBox.Text);
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
            ProjectsDataGrid.ItemsSource = projectsTableAdapter.GetData();
        }

        private void ClearFields()
        {
            NameBox.Text = string.Empty;
            DescriptionBox.Text = string.Empty;
        }
    }
}
