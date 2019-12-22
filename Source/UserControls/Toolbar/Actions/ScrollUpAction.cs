using JsonFlier.Command;
using JsonFlier.UserControls.Logs;

namespace JsonFlier.UserControls.Toolbar.Actions
{
    public class ScrollUpAction : SimpleAction
    {
        public ScrollUpAction(JsonArray jsonArray) : base("up-long-arrow.png", new Command_ScrollUp(jsonArray.listView)) { }
    }
}
