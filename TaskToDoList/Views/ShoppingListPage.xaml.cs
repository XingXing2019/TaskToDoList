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
using TaskToDoList.ViewModels;

namespace TaskToDoList.Views
{
    /// <summary>
    /// Interaction logic for ShoppingListPage.xaml
    /// </summary>
    public partial class ShoppingListPage : Page
    {
        public ShoppingListPage()
        {
            InitializeComponent();
            this.DataContext = new ShoppingListPageViewModel();
            Messenger.Default.Register<GroceryListItemViewModel>(this, "Expand", ExpandColumn);
        }

        //Method to Expand the Column
        private void ExpandColumn(GroceryListItemViewModel groceryListItem)
        {
            var cdf = grc.ColumnDefinitions;
            if (cdf[1].Width == new GridLength(0))
                cdf[1].Width = new GridLength(250);
            else
                cdf[1].Width = new GridLength(0);
        }
    }
}
