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
using System.Xml.Linq;
using TaskToDoList.Services;
using System.Threading;
using GalaSoft.MvvmLight;

namespace TaskToDoList.ViewModels
{
    class AgendaPageViewModel : ViewModelBase
    {
        #region Commands

        public DelegateCommand AddTaskCommand { get; set; }
        public DelegateCommand DeleteTaskCommand { get; set; }
        public DelegateCommand SetImportantTaskCommand { get; set; }
        public DelegateCommand AddTaskDetailCommand { get; set; }
        public RelayCommand<TaskListItemViewModel> SelectedTaskCommand { get; set; }

        #endregion

        #region Properties

        private const string taskPath = "TaskInfo.xml";
        private const string importantTaskPath = "ImportantTaskInfo.xml";

        public Date Date { get; set; }

        private ObservableCollection<TaskListItemViewModel> taskItems;
        public ObservableCollection<TaskListItemViewModel> TaskListItems
        {
            get { return taskItems; }
            set
            {
                taskItems = value;
                this.RaisePropertyChanged("TaskListItems");
            }
        }

        private string taskContent;
        public string TaskContent
        {
            get { return taskContent; }
            set
            {
                taskContent = value;
                this.RaisePropertyChanged("TaskContent");
            }
        }

        private string taskDetail;
        public string TaskDetail
        {
            get { return taskDetail; }
            set
            {
                taskDetail = value;
                this.RaisePropertyChanged("TaskDetail");
            }
        }
        
        private TaskListItemViewModel taskListItem;
        public TaskListItemViewModel TaskListItem
        {
            get { return taskListItem; }
            set
            {
                taskListItem = value;
                this.RaisePropertyChanged("TaskListItem");
            }
        }

        #endregion

        #region Constructor

        //Setup date to display, initialize task list and commands
        public AgendaPageViewModel()
        {
            this.Date = new Date();
            this.TaskListItems = XmlDataService.LoadData<TaskListItemViewModel>(taskPath);
            this.AddTaskCommand = new DelegateCommand(new Action(AddTaskCommandExecute));
            this.AddTaskDetailCommand = new DelegateCommand(new Action(AddTaskDetailCommandExecute));
            this.DeleteTaskCommand = new DelegateCommand(new Action(DeleteTaskCommandExecute));
            this.SetImportantTaskCommand = new DelegateCommand(new Action(SetImportantTaskCommandExecute));
            this.SelectedTaskCommand = new RelayCommand<TaskListItemViewModel>(t => SelectedTask(t));
        }

        #endregion

        #region Command Executors 

        //Add task into xml file
        private void AddTaskCommandExecute()
        {
            if (string.IsNullOrEmpty(this.TaskContent))
                return;
            if(this.TaskListItems.Any(t => t.TaskInfo.Content == this.TaskContent))
            {
                this.TaskContent = "Task is already in the list";
                Helper.Delay(2000);
                this.TaskContent = "";
                return;
            }
            var taskInfo = new TaskInfo() { Content = this.TaskContent };
            var taskItem = new TaskListItemViewModel() { TaskInfo = taskInfo };
            this.TaskListItems.Add(taskItem);
            XmlDataService.SaveData(taskPath, this.TaskListItems);
            this.TaskContent = "";
        }

        //Add task detail in xml file
        private void AddTaskDetailCommandExecute()
        {
            if (string.IsNullOrEmpty(this.TaskDetail))
                return;
            var taskContent = this.TaskListItem.TaskInfo.Content;
            var task = this.TaskListItems.Where(t => t.TaskInfo.Content == taskContent).First();
            task.TaskInfo.Details = this.TaskDetail;
            XmlDataService.SaveData(taskPath, this.TaskListItems);
            ObservableCollection<TaskListItemViewModel> importantTasks = XmlDataService.LoadData<TaskListItemViewModel>(importantTaskPath);
            foreach (var t in importantTasks)
            {
                if (t.TaskInfo.Content == taskContent)
                {
                    t.TaskInfo.Details = this.TaskDetail;
                    break;
                }
            }
            XmlDataService.SaveData(importantTaskPath, importantTasks);
            this.TaskDetail = "";
        }
        
        //Record important task into xml file
        private void SetImportantTaskCommandExecute()
        {
            var temTasks = this.TaskListItems.Where(t => t.IsImportant == true).ToList();
            var importantTasks = new ObservableCollection<TaskListItemViewModel>();
            foreach (var t in temTasks)
                importantTasks.Add(t);
            XmlDataService.SaveData(importantTaskPath, importantTasks);
            XmlDataService.SaveData(taskPath, this.TaskListItems);
        }

        //Delete task from xml file
        private void DeleteTaskCommandExecute()
        {
            var selectedTask = this.TaskListItems.Where(t => t.IsSelected == true).First();
            XmlDataService.DeleteData(taskPath, selectedTask, this.TaskListItems);
            var temTasks = this.TaskListItems.Where(t => t.IsImportant == true).ToList();
            var importantTasks = new ObservableCollection<TaskListItemViewModel>();
            foreach (var t in temTasks)
                importantTasks.Add(t);
            XmlDataService.SaveData(importantTaskPath, importantTasks);
        }

        //Expand selected task
        private void SelectedTask(TaskListItemViewModel taskListItem)
        {
            this.TaskListItem = taskListItem;
            Messenger.Default.Send(taskListItem, "Expand");
        }

        #endregion
    }
}
