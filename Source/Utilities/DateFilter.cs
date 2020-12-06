using System;

namespace JsonFlier.Utilities
{
    public class DateFilter
    {
        private DateTime? start, end;

        public delegate void DateFilterChangeEventHandler(bool narrowingDown);
        public event DateFilterChangeEventHandler OnChanged;

        public DateTime? Start => start;
        public DateTime? End => end;

        public void SetFilter(DateTime? filterStart, DateTime? filterEnd)
        {
            bool narrowingDown = true;
            if (start != null && (filterStart == null || start.Value > filterStart.Value))
                narrowingDown = false;
            if (end != null && (filterEnd == null || end.Value < filterEnd.Value))
                narrowingDown = false;

            start = filterStart;
            end = filterEnd;

            OnChanged?.Invoke(narrowingDown);
        }

        public bool Filter(DateTime dateTime)
        {
            if (start.HasValue && start > dateTime)
                return false;
            if (end.HasValue && end < dateTime)
                return false;
            return true;
        }
    }
}
