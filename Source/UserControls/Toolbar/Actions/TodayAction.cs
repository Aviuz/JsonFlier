using JsonFlier.Command;
using JsonFlier.UserControls.Logs;

namespace JsonFlier.UserControls.Toolbar.Actions
{
    public class TodayAction : SimpleAction
    {
        public TodayAction(JsonArray jsonArray) : base("calendar.png", new Command_Today(jsonArray)) { }
    }
}
