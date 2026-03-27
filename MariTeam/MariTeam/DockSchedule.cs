using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MariTeam
{
    internal class DockSchedule
    {
        // definisi instance variable
        private string _scheduleID;
        private DateTime _arrivalTime;
        private DateTime _departureTime;

        // definisi property
        public string ScheduleID
        {
            get { return _scheduleID; }
        }
        public DateTime ArrivalTime
        {
            get { return _arrivalTime; }
            set { _arrivalTime = value; }
        }
        public DateTime DepartureTime
        {
            get { return _departureTime; }
            set { _departureTime = value; }
        }

        // definisi method
        public void CalculateBerthTime()
        {
            TimeSpan berthTime = _departureTime - _arrivalTime;
            Console.WriteLine($"Berth time is {berthTime.TotalHours} hours.");
        }
    }
}
