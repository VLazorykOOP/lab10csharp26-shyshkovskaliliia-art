using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FacultyLife.Models
{
    public class Faculty
    {
        public event FacultyEventHandler DayOfFaculty;

        protected virtual void OnDayOfFaculty(FacultyEventArgs e)
        {
            if (DayOfFaculty != null)
            {
                DayOfFaculty(this, e);
            }
        }

        public async Task SimulateLife(IProgress<FacultyEventArgs> progress)
        {
            string[] activities = { "Урочиста частина", "Спортивні змагання", "Концерт" };
            Random rnd = new Random();

            foreach (var act in activities)
            {
                await Task.Delay(1000);
                var e = new FacultyEventArgs(act, rnd.Next(1, 3));

                OnDayOfFaculty(e);
                progress.Report(e);
            }
        }
    }
}

