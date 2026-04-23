using System;

namespace FacultyLife.Models
{
    // Завдання 1: підписник — Деканат
    public class Deanery
    {
        public void HandleEvent(object sender, FacultyEventArgs e)
        {
            e.DeanResult = e.Priority switch
            {
                EventPriority.High => "Організовано Деканатом (високий пріоритет)",
                EventPriority.Medium => "Взято до уваги Деканатом",
                EventPriority.Low => "Деканат прийняв до відома",
                _ => "Зафіксовано"
            };
        }
    }

    // Завдання 1: підписник — Студентська рада
    public class StudentCouncil
    {
        public void HandleEvent(object sender, FacultyEventArgs e)
        {
            if (e.ActivityName.Contains("Спортивні"))
                e.DeanResult += " | Студрада закупила воду.";
            else if (e.ActivityName.Contains("Концерт"))
                e.DeanResult += " | Студрада організувала квитки.";
            else if (e.ActivityName.Contains("Виставка"))
                e.DeanResult += " | Студрада оформила залу.";
        }
    }

    // Завдання 1: підписник — Студент
    public class Student
    {
        public string Name { get; }
        public string Group { get; }

        public Student(string name, string group)
        {
            Name = name;
            Group = group;
        }

        public void HandleEvent(object sender, FacultyEventArgs e)
        {
            string reaction = e.ActivityName switch
            {
                var a when a.Contains("Концерт") => "виступив(-ла) на сцені",
                var a when a.Contains("Спортивні") => "вболівав(-ла) за команду",
                var a when a.Contains("Урочиста") => "отримав(-ла) грамоту",
                var a when a.Contains("Виставка") => "представив(-ла) свій проект",
                var a when a.Contains("Майстер") => "взяв(-ла) участь у майстер-класі",
                var a when a.Contains("Зустріч") => "поспілкувався(-лась) із випускниками",
                _ => "відвідав(-ла) захід"
            };

            e.StudentResults.Add($"{Name} ({Group}): {reaction}");
        }
    }
}
