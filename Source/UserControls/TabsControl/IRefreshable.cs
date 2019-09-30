using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsonFlier.UserControls.TabsControl
{
    public interface IRefreshable
    {
        bool CanRefresh { get; }

        void Refresh();
    }
}
