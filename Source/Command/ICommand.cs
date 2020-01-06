using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsonFlier.Command
{
    public interface ICommand
    {
        bool IsEnabled { get; }

        void Execute();
    }
}
