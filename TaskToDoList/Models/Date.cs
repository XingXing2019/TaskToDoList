using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskToDoList.Models
{
    public class Date
    {
        public string DateAndDay { get; set; }
        public Date()
        {
            this.DateAndDay = GetDate();
        }

        //Get current date and day
        private static string GetDate()
        {
            string[] days = { "Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday" };
            string day = days[(int)DateTime.Now.DayOfWeek];
            string date = string.Format("{0:M}", DateTime.Now);
            return $"{date}, {day}";
        }
    }
}
