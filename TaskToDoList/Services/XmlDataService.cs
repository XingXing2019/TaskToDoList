using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using TaskToDoList.Models;
using TaskToDoList.ViewModels;

namespace TaskToDoList.Services
{
    class XmlDataService
    {
        private static XmlWriter writer;
        private static XmlReader reader;
        private static XmlWriterSettings settings = new XmlWriterSettings() { Indent = true };

        #region Generic Methods

        //Save data in xml file
        public static void SaveData<T>(string path, ObservableCollection<T> taskListItems)
        {
            var serializer = new XmlSerializer(typeof(ObservableCollection<T>));
            using (writer = XmlWriter.Create(path, settings))
                serializer.Serialize(writer, taskListItems);
        }

        //Read data from xml file
        public static ObservableCollection<T> LoadData<T>(string path)
        {
            var serializer = new XmlSerializer(typeof(ObservableCollection<T>));
            var taskListItems = new ObservableCollection<T>();
            if (File.Exists(path))
                using (reader = XmlReader.Create(path))
                    taskListItems = (ObservableCollection<T>)serializer.Deserialize(reader);
            return taskListItems;
        }       

        //Delete data from xml file
        public static void DeleteData<T>(string path, T taskListItem, ObservableCollection<T> taskListItems)
        {
            taskListItems.Remove(taskListItem);
            var serializer = new XmlSerializer(typeof(ObservableCollection<T>));
            using (writer = XmlWriter.Create(path, settings))
                serializer.Serialize(writer, taskListItems);
        }

        #endregion
    }
}
