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
    class ImportantTasksPageViewModel : BindableBase
    {
        #region Properties

        private const string importantTaskPath = "ImportantTaskInfo.xml";

        public Date Date { get; set; }
        private ObservableCollection<TaskListItemViewModel> importantTask;
        public ObservableCollection<TaskListItemViewModel> ImportantTasks
        {
            get { return importantTask; }
            set
            {
                importantTask = value;
                this.RaisePropertyChanged("ImportantTasks");
            }
        }

        #endregion

        #region Constructor

        //Setup date to display, initialize important task list and commands
        public ImportantTasksPageViewModel()
        {
            this.Date = new Date();
            this.ImportantTasks = XmlDataService.LoadData<TaskListItemViewModel>(importantTaskPath);
        }

        #endregion
    }
}
