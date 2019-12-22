using JsonFlier.Command;
using JsonFlier.UserControls.Logs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace JsonFlier.UserControls.Toolbar.Actions
{
    public class ScrollDownAction : SimpleAction
    {
        public ScrollDownAction(JsonArray jsonArray) : base("down-long-arrow.png", new Command_ScrollDown(jsonArray.listView)) { }
    }
}
