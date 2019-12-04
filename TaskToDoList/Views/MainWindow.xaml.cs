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
using TaskToDoList.ViewModels;

namespace TaskToDoList
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            //Enable mouse drag move
            this.MouseDown += (sender, e) =>
            {
                if (e.LeftButton == MouseButtonState.Pressed)
                    this.DragMove();
            };
        }

        //Maximize window
        private void btnMax_Click(object sender, RoutedEventArgs e)
        {
            if (this.WindowState == WindowState.Normal)
                this.WindowState = WindowState.Maximized;
            else
                this.WindowState = WindowState.Normal;
        }

        //Minimize window
        private void btnMin_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        //Close window
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Environment.Exit(Environment.ExitCode);
        }

        //Navigate to Agenda page
        private void btnAgenda_Click(object sender, RoutedEventArgs e)
        {
            this.btnAgenda.Foreground = new SolidColorBrush(Colors.Green);
            this.pages.Source = new Uri("AgendaPage.xaml", UriKind.RelativeOrAbsolute);
        }

        //Navigate to ImportantTasks page
        private void btnImportantTask_Click(object sender, RoutedEventArgs e)
        {
            this.btnAgenda.Foreground = new SolidColorBrush(Colors.Green);
            this.pages.Source = new Uri("ImportantTasksPage.xaml", UriKind.RelativeOrAbsolute);
        }

        //Navigate to ShoppingList page
        private void btnShoppingList_Click(object sender, RoutedEventArgs e)
        {
            this.btnAgenda.Foreground = new SolidColorBrush(Colors.Purple);
            this.pages.Source = new Uri("ShoppingListPage.xaml", UriKind.RelativeOrAbsolute);
        }
    }
}
