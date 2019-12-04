using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskToDoList.Models;

namespace TaskToDoList.ViewModels
{
    public class GroceryListItemViewModel : BindableBase
    {
        public Grocery Grocery { get; set; }

        private bool isSelected;
        public bool IsSelected
        {
            get { return isSelected; }
            set
            {
                isSelected = value;
                this.RaisePropertyChanged("IsSelected");
            }
        }

        private bool isPurchased;
        public bool IsPurchased
        {
            get { return isPurchased; }
            set
            {
                isPurchased = value;
                this.RaisePropertyChanged("IsPurchased");
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
    }
}
