using JsonFlier.Command;
using JsonFlier.UserControls.Logs;

namespace JsonFlier.UserControls.Toolbar.Actions
{
    public class RefreshAction : SimpleAction
    {
        public RefreshAction(JsonArray jsonArray) : base("reload.png", new Command_Refresh(jsonArray)) { }
    }
}
