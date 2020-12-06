using System;

namespace JsonFlier.UIControls.Toolbars.Commands
{
    public class StringListener
    {
        private string data;

        public StringListener(string value)
        {
            data = value;
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
