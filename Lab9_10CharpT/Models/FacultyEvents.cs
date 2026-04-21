using System;

namespace FacultyLife.Models
{
    public delegate void FacultyEventHandler(object sender, FacultyEventArgs e);

    public class FacultyEventArgs : EventArgs
    {
        public string ActivityName { get; }
        public int Priority { get; }
        public string Result { get; set; }

        public FacultyEventArgs(string name, int priority)
        {
            ActivityName = name;
            this.Priority = priority;
        }
    }
}

