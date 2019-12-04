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
        #region Commands

        public DelegateCommand LoadImportantTasksCommand { get; set; }

        #endregion

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
            this.ImportantTasks = new ObservableCollection<TaskListItemViewModel>();
            this.LoadImportantTasksCommand = new DelegateCommand(new Action(LoadImportantTasksCommandExecute));
        }

        #endregion

        #region Command Executors

        //Load important tasks from xml file 
        private void LoadImportantTasksCommandExecute()
        {
            this.ImportantTasks = XmlDataService.LoadData<TaskListItemViewModel>(importantTaskPath);
        }

        #endregion
    }
}
