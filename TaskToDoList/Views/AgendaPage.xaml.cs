using GalaSoft.MvvmLight.Messaging;
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
using TaskToDoList.Models;
using TaskToDoList.ViewModels;

namespace TaskToDoList.Views
{
    /// <summary>
    /// Interaction logic for AgendaPage.xaml
    /// </summary>
    public partial class AgendaPage : Page
    {
        public AgendaPage()
        {
            InitializeComponent();
            this.DataContext = new AgendaPageViewModel();
            Messenger.Default.Register<TaskListItemViewModel>(this, "Expand", ExpandColumn);
        }

        //Method to Expand the Column
        private void ExpandColumn(TaskListItemViewModel taskItem)
        {
            var cdf = grc.ColumnDefinitions;
            if (cdf[1].Width == new GridLength(0))
                cdf[1].Width = new GridLength(250);
            else
                cdf[1].Width = new GridLength(0);
        }
    }
}
