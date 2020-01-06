using System.Windows.Controls;

namespace JsonFlier.Command
{
    public class Command_ScrollDown : ICommand
    {
        public Command_ScrollDown(ListView listView)
        {
            ListView = listView;
        }

        public ListView ListView { get; set; }

        public bool IsEnabled => true;

        public void Execute()
        {
            if (ListView.Items.Count > 0)
            {
                ListView.ScrollIntoView(ListView.Items[ListView.Items.Count - 1]);
            }
        }
    }
}
