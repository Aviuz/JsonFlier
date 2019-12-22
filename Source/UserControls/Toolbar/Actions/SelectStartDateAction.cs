using JsonFlier.UserControls.Logs;

namespace JsonFlier.UserControls.Toolbar.Actions
{
    public class SelectStartDateAction : SelectDateAction
    {
        public SelectStartDateAction(JsonArray jsonArrayControl) : base("Start", jsonArrayControl.DateTimeStartListener)
        {
            Enabled = jsonArrayControl.CanRefresh;
        }
    }
}
