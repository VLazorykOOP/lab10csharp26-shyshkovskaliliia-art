using FacultyLife.Models;
using System;
using System.Windows.Forms;

namespace FacultyLife.Tasks
{
    public class Lab10T2
    {
        private readonly ListBox _list;
        private readonly Label _stats;
        private readonly ProgressBar _progress;
        private readonly Button _btnStart;

        public Lab10T2(ListBox list, Label stats, ProgressBar progress, Button btnStart)
        {
            _list = list;
            _stats = stats;
            _progress = progress;
            _btnStart = btnStart;
        }

        public async void Run()
        {
            _list.Items.Clear();
            _btnStart.Enabled = false;
            _progress.Style = ProgressBarStyle.Marquee;

            // --- Завдання 1: об'єкт факультету та підписники ---
            Faculty faculty = new Faculty();

            Deanery dean = new Deanery();
            StudentCouncil council = new StudentCouncil();
            Student s1 = new Student("Олена Ковальчук", "КН-21");
            Student s2 = new Student("Максим Бондар", "ІПЗ-22");
            Student s3 = new Student("Аліна Мороз", "КН-21");

            // Підписка на подію
            faculty.DayOfFaculty += dean.HandleEvent;
            faculty.DayOfFaculty += council.HandleEvent;
            faculty.DayOfFaculty += s1.HandleEvent;
            faculty.DayOfFaculty += s2.HandleEvent;
            faculty.DayOfFaculty += s3.HandleEvent;

            // --- Завдання 2: черга з пріоритетами ---
            faculty.EnqueueEvents();

            int count = 0;

            // --- Завдання 2: Progress<T> для асинхронного оновлення UI ---
            var eventProgress = new Progress<FacultyEventArgs>(e =>
            {
                count++;

                _list.Items.Add($"Подія: {e.ActivityName}. Пріоритет: {e.Priority}");

                if (!string.IsNullOrWhiteSpace(e.DeanResult))
                    _list.Items.Add($"  Результат: {e.DeanResult}");

                foreach (var sr in e.StudentResults)
                    _list.Items.Add($"  {sr}");

                _stats.Text = $"Заходів проведено: {count}";
                _list.TopIndex = _list.Items.Count - 1;
            });

            var statusProgress = new Progress<string>(_ => { });

            _list.Items.Add("--- Старт моделювання ---");

            // --- Завдання 2: асинхронне моделювання ---
            await faculty.SimulateLife(eventProgress, statusProgress);

            _list.Items.Add("--- Статистика ---");
            _list.Items.Add($"  Тривалість: {faculty.Statistics.Duration.TotalSeconds:F1} с");
            _list.Items.Add($"  Всього заходів: {faculty.Statistics.TotalEvents}");
            _list.Items.Add($"  High-пріоритетних: {faculty.Statistics.CountByPriority(EventPriority.High)}");
            _list.Items.Add($"  Medium-пріоритетних: {faculty.Statistics.CountByPriority(EventPriority.Medium)}");
            _list.Items.Add($"  Low-пріоритетних: {faculty.Statistics.CountByPriority(EventPriority.Low)}");

            _stats.Text = $"Заходів проведено: {count}  |  " +
                          $"Тривалість: {faculty.Statistics.Duration.TotalSeconds:F1} с";

            _progress.Style = ProgressBarStyle.Blocks;
            _progress.Value = 100;
            _btnStart.Enabled = true;
        }
    }
}
