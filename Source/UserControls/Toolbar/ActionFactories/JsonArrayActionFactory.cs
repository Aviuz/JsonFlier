using JsonFlier.UserControls.Logs;
using JsonFlier.UserControls.Toolbar.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace JsonFlier.UserControls.Toolbar.ActionFactories
{
    public class JsonArrayActionFactory : IActionFactory
    {
        public JsonArrayActionFactory(JsonArray jsonArrayControl)
        {
            JsonArrayControl = jsonArrayControl;
        }

        public JsonArray JsonArrayControl { get; }

        public Control[] CreateActions() => CreateActionsCore().ToArray();

        private IEnumerable<Control> CreateActionsCore()
        {
            yield return new ScrollUpAction(JsonArrayControl);
            yield return new ScrollDownAction(JsonArrayControl);

            yield return new Separator();

            yield return new RefreshAction(JsonArrayControl);
            yield return new TodayAction(JsonArrayControl);

            yield return new Separator();

            yield return new SelectStartDateAction(JsonArrayControl);
            yield return new Separator();
            yield return new SelectEndDateAction(JsonArrayControl);
        }
    }
}
