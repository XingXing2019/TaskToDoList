using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskToDoList.Models;

namespace TaskToDoList.ViewModels
{
    public class TaskListItemViewModel : BindableBase
    {
        public TaskInfo TaskInfo { get; set; }

        private bool isImportant;
        public bool IsImportant
        {
            get { return isImportant; }
            set
            {
                isImportant = value;
                this.RaisePropertyChanged("IsImportant");
            }
        }

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
    }
}
