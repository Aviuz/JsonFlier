using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace JsonFlier.UserControls.Toolbar.ActionFactories
{
    public interface IActionFactory
    {
        Control[] CreateActions();
    }
}
