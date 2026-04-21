using FacultyLife.Models;
using System;
using System.Windows.Forms;

namespace FacultyLife.Tasks
{
    public class Lab10T2
    {
        private ListBox _list;
        private Label _stats;

        public Lab10T2(ListBox list, Label stats)
        {
            _list = list;
            _stats = stats;
        }

        public async void Run()
        {
            Faculty faculty = new Faculty();

            Deanery dean = new Deanery();

            faculty.DayOfFaculty += dean.HandleEvent;

            int count = 0;
            var progress = new Progress<FacultyEventArgs>(e => {
                count++;
                _list.Items.Add($"Подія: {e.ActivityName}. Пріоритет: {e.Priority}");
                _list.Items.Add($"  Результат: {e.Result}");
                _stats.Text = $"Заходів проведено: {count}";
            });

            _list.Items.Add("--- Старт моделювання ---");
            await faculty.SimulateLife(progress);
        }
    }

    public class Deanery
    {
        public void HandleEvent(object sender, FacultyEventArgs e)
        {
            e.Result = e.Priority == 1 ? "Організовано Деканатом" : "Взято до уваги";
        }
    }
}
