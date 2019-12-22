using System;

namespace JsonFlier.Command
{
    public class DateTimeListener
    {
        private DateTime? dateTime;

        public DateTimeListener(DateTime? value)
        {
            this.dateTime = value;
        }

        public delegate void DateTimeChangedEvent(DateTime? oldValue, DateTime? newValue);

        public event DateTimeChangedEvent OnValueChanged;

        public DateTime? DateTime
        {
            get => dateTime;
            set
            {
                var oldValue = dateTime;
                dateTime = value;
                OnValueChanged?.Invoke(oldValue, value);
            }
        }
    }
}
