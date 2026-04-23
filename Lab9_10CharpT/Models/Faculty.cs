using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FacultyLife.Models
{
    public class Faculty
    {
        public event FacultyEventHandler DayOfFaculty;

        private readonly List<FacultyEventArgs> _eventQueue = new();

        public FacultyStatistics Statistics { get; } = new();

        protected virtual void OnDayOfFaculty(FacultyEventArgs e)
        {
            DayOfFaculty?.Invoke(this, e);
        }

        public void EnqueueEvents()
        {
            _eventQueue.Clear();
            _eventQueue.Add(new FacultyEventArgs("Урочиста частина", EventPriority.High));
            _eventQueue.Add(new FacultyEventArgs("Спортивні змагання", EventPriority.Medium));
            _eventQueue.Add(new FacultyEventArgs("Концерт", EventPriority.Medium));
            _eventQueue.Add(new FacultyEventArgs("Виставка студентських робіт", EventPriority.Low));
            _eventQueue.Add(new FacultyEventArgs("Зустріч із випускниками", EventPriority.High));
            _eventQueue.Add(new FacultyEventArgs("Майстер-клас", EventPriority.Low));
        }

        public async Task SimulateLife(IProgress<FacultyEventArgs> progress,
                                       IProgress<string> statusProgress)
        {
            var ordered = _eventQueue.OrderByDescending(e => (int)e.Priority).ToList();

            Statistics.StartTime = DateTime.Now;
            Statistics.TotalEvents = ordered.Count;

            foreach (var e in ordered)
            {
                statusProgress.Report($"Виконується: {e.ActivityName}...");

                int delay = e.Priority switch
                {
                    EventPriority.High => 700,
                    EventPriority.Medium => 1000,
                    EventPriority.Low => 1400,
                    _ => 1000
                };

                await Task.Delay(delay);
                OnDayOfFaculty(e);
                Statistics.Record(e);

                progress.Report(e);
            }

            Statistics.EndTime = DateTime.Now;
            statusProgress.Report("Готово");
        }
    }
}
