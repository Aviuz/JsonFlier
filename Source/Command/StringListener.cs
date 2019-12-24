using System;

namespace JsonFlier.Command
{
    public class StringListener
    {
        private string data;

        public StringListener(string value)
        {
            this.data = value;
        }

        public delegate void ValueChangedEvent(string oldValue, string newValue);

        public event ValueChangedEvent OnValueChanged;

        public String Value
        {
            get => data;
            set
            {
                var oldValue = value;
                data = value;
                OnValueChanged?.Invoke(oldValue, value);
            }
        }
    }
}
