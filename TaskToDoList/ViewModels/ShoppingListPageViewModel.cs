using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskToDoList.Models;
using TaskToDoList.Services;

namespace TaskToDoList.ViewModels
{
    class ShoppingListPageViewModel : BindableBase
    {
        #region Commands

        public DelegateCommand AddGroceryCommand { get; set; }
        public DelegateCommand DeleteGroceryCommand { get; set; }
        public DelegateCommand AddQuantityCommand { get; set; }
        public DelegateCommand SetPurchasedCommand { get; set; }
        public RelayCommand<GroceryListItemViewModel> SelectedGroceryCommand { get; set; }

        #endregion

        #region Properties

        private const string shoppingListPath = "ShoppingList.xml";

        public Date Date { get; set; }

        private string itemName;
        public string ItemName
        {
            get { return itemName; }
            set
            {
                itemName = value;
                this.RaisePropertyChanged("ItemName");
            }
        }

        private string quantity;
        public string Quantity
        {
            get { return quantity; }
            set
            {
                quantity = value;
                this.RaisePropertyChanged("Quantity");
            }
        }
        
        private ObservableCollection<GroceryListItemViewModel> groceryItems;
        public ObservableCollection<GroceryListItemViewModel> GroceryItems
        {
            get { return groceryItems; }
            set
            {
                groceryItems = value;
                this.RaisePropertyChanged("GroceryItems");
            }
        }

        private GroceryListItemViewModel groceryItem;
        public GroceryListItemViewModel GroceryItem
        {
            get { return groceryItem; }
            set
            {
                groceryItem = value;
                this.RaisePropertyChanged("GroceryItem");
            }
        }

        #endregion

        #region Constructor

        //Setup date to display, initialize important task list and commands
        public ShoppingListPageViewModel()
        {
            this.Date = new Date();
            this.GroceryItems = XmlDataService.LoadData<GroceryListItemViewModel>(shoppingListPath);
            this.AddGroceryCommand = new DelegateCommand(new Action(AddGroceryCommandExecute));
            this.DeleteGroceryCommand = new DelegateCommand(new Action(DeleteGroceryCommandExecute));
            this.AddQuantityCommand = new DelegateCommand(new Action(AddQuantityCommandExecute));
            this.SetPurchasedCommand = new DelegateCommand(new Action(SetPurchasedCommandExecute));
            this.SelectedGroceryCommand = new RelayCommand<GroceryListItemViewModel>(g => SelectedGrocery(g));
        }

        #endregion

        #region Command Executors

        //Add grocery into xml file
        private void AddGroceryCommandExecute()
        {
            if (string.IsNullOrEmpty(this.ItemName))
                return;
            if(this.GroceryItems.Any(g => g.Grocery.ItemName == this.ItemName))
            {
                this.ItemName = "Item is already in the shopping list";
                Helper.Delay(2000);
                this.ItemName = "";
                return;
            }
            var grocery = new Grocery() { ItemName = this.ItemName };
            var groceryItem = new GroceryListItemViewModel() { Grocery = grocery };
            this.GroceryItems.Add(groceryItem);
            XmlDataService.SaveData(shoppingListPath, this.GroceryItems);
            this.ItemName = "";
        }
        
        //Delete grocery from xml file
        private void DeleteGroceryCommandExecute()
        {
            var selectedGrocery = this.GroceryItems.Where(g => g.IsSelected == true).First();
            XmlDataService.DeleteData(shoppingListPath, selectedGrocery, this.GroceryItems);
        }

        //Add quantity to grocery items
        private void AddQuantityCommandExecute()
        {
            if (string.IsNullOrEmpty(this.Quantity))
                return;
            var itemName = this.GroceryItem.Grocery.ItemName;
            var grocery = this.GroceryItems.Where(g => g.Grocery.ItemName == itemName).First();
            grocery.Quantity = this.Quantity;
            XmlDataService.SaveData(shoppingListPath, this.GroceryItems);
            this.Quantity = "";
        }

        //Record if grocery is purchsed
        private void SetPurchasedCommandExecute()
        {
            XmlDataService.SaveData(shoppingListPath, this.GroceryItems);
        }

        //Expand selected grocery item
        private void SelectedGrocery(GroceryListItemViewModel groceryListItem)
        {
            this.GroceryItem = groceryListItem;
            Messenger.Default.Send(groceryListItem, "Expand");
        }

        #endregion
    }
}
