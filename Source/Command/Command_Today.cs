using JsonFlier.UserControls.Logs;

namespace JsonFlier.Command
{
    public class Command_Today : ICommand
    {
        public Command_Today(JsonArray jsonArrayControl)
        {
            JsonArrayControl = jsonArrayControl;
        }

        public JsonArray JsonArrayControl { get; }

        public void Execute()
        {
            JsonArrayControl.PanToToday();
        }
    }
}
