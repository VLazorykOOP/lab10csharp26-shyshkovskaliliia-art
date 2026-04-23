using System;
using System.Collections.Generic;

namespace FacultyLife.Models
{
    public delegate void FacultyEventHandler(object sender, FacultyEventArgs e);

    public enum EventPriority
    {
        High = 3,
        Medium = 2,
        Low = 1
    }

    public class FacultyEventArgs : EventArgs
    {
        public string ActivityName { get; }
        public EventPriority Priority { get; }
        public DateTime Timestamp { get; }

        public string DeanResult { get; set; } = string.Empty;

        public List<string> StudentResults { get; } = new List<string>();

        public FacultyEventArgs(string name, EventPriority priority)
        {
            ActivityName = name;
            Priority = priority;
            Timestamp = DateTime.Now;
        }
    }
}
