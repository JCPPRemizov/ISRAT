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
using ISRAT.Pages;

namespace ISRAT.Windows
{
    /// <summary>
    /// Логика взаимодействия для Administrator.xaml
    /// </summary>
    public partial class Administrator : Window
    {
        public static MainWindow mainWindow;

        private static readonly Page[] pages = new Page[]
        {
            new AdminStartPage(),
            new ResourcesPage(),
            new StatusPage(),
            new ProjectsPage(),
            new TasksPage()
        };
        public Administrator()
        {
            InitializeComponent();
        }

        public void ChangeFrame(int frameIndex)
        {
            AdminFrame.Content = pages[frameIndex];
        }


        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void HideAppButton_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        private void CloseAppButton_Click(object sender, RoutedEventArgs e)
        {
            Hide();
            mainWindow.Show();
        }


        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            AdminStartPage.administrator = this;
            AdminFrame.Content = pages[0];
        }
    }
}
