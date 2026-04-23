using System;
using System.Collections.Generic;
using System.Linq;

namespace FacultyLife.Models
{
    public class FacultyStatistics
    {
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int TotalEvents { get; set; }

        private readonly List<FacultyEventArgs> _processed = new();

        public void Record(FacultyEventArgs e) => _processed.Add(e);
        public void Reset() => _processed.Clear();

        public int ProcessedCount => _processed.Count;
        public TimeSpan Duration => EndTime - StartTime;
        public int CountByPriority(EventPriority p) => _processed.Count(e => e.Priority == p);
    }
}
