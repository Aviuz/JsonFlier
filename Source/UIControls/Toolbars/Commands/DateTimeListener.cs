using JsonFlier.Utilities;
using System;

namespace JsonFlier.UIControls.Toolbars.Commands
{
    public interface IDateTimeListener
    {
        public delegate void DateTimeChangedEvent(DateTime? oldValue, DateTime? newValue);
        public event DateTimeChangedEvent OnValueChanged;

        public DateTime? DateTime { get; set; }
    }

    public class StartDateFilterListener : IDateTimeListener
    {
        private readonly DateFilter dateFilter;
        private DateTime? oldValue;
        public event IDateTimeListener.DateTimeChangedEvent OnValueChanged;

        public StartDateFilterListener(DateFilter dateFilter)
        {
            this.dateFilter = dateFilter;
            oldValue = dateFilter.Start;
            dateFilter.OnChanged += DateFilter_OnChanged;
        }

        public DateTime? DateTime { get => dateFilter.Start; set => dateFilter.SetFilter(value, dateFilter.End); }

        private void DateFilter_OnChanged(bool narrowingDown)
        {
            if(oldValue != dateFilter.Start)
            {
                OnValueChanged?.Invoke(oldValue, dateFilter.Start);
                oldValue = dateFilter.Start;
            }
        }
    }

    public class EndDateFilterListener : IDateTimeListener
    {
        private readonly DateFilter dateFilter;
        private DateTime? oldValue;
        public event IDateTimeListener.DateTimeChangedEvent OnValueChanged;

        public EndDateFilterListener(DateFilter dateFilter)
        {
            this.dateFilter = dateFilter;
            oldValue = dateFilter.End;
            dateFilter.OnChanged += DateFilter_OnChanged;
        }

        public DateTime? DateTime { get => dateFilter.End; set => dateFilter.SetFilter(dateFilter.Start, value); }

        private void DateFilter_OnChanged(bool narrowingDown)
        {
            if (oldValue != dateFilter.End)
            {
                OnValueChanged?.Invoke(oldValue, dateFilter.End);
                oldValue = dateFilter.End;
            }
        }
    }
}
