using System.Linq;
using System.Windows.Controls;

namespace JsonFlier.Command
{
    public class Command_ScrollUp : ICommand
    {
        public ListView ListView { get; set; }

        public Command_ScrollUp(ListView listView)
        {
            ListView = listView;
        }

        public void Execute()
        {
            if (ListView.Items.Count > 0)
            {
                ListView.ScrollIntoView(ListView.Items[0]);
            }
        }
    }
}
