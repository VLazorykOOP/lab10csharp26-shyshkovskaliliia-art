using System;
using System.Collections.Generic;
using System.Text;

namespace FacultyLife.Models
{
    public class Deanery
    {
        public void HandleEvent(object sender, FacultyEventArgs e)
        {
            if (e.Priority == 1)
                e.Result = "Деканат підготував нагороди.";
            else
                e.Result = "Деканат веде облік відвідуваності.";
        }
    }

    public class StudentCouncil
    {
        public void HandleEvent(object sender, FacultyEventArgs e)
        {
            if (e.ActivityName.Contains("Спортивні"))
                e.Result += " Студрада закупила воду для учасників.";
        }
    }
}
