using JsonFlier.UserControls.Logs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsonFlier.UserControls.Toolbar.Actions
{
    public class SelectEndDateAction : SelectDateAction
    {
        public SelectEndDateAction(JsonArray jsonArrayControl) : base("End", jsonArrayControl.DateTimeEndListener)
        {
            Enabled = jsonArrayControl.CanRefresh;
        }
    }
}
