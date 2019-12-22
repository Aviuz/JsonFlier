using System.Windows.Controls;

namespace JsonFlier.Command
{
    public class Command_ScrollDown : ICommand
    {
        public ListView ListView { get; set; }

        public Command_ScrollDown(ListView listView)
        {
            ListView = listView;
        }

        public void Execute()
        {
            if (ListView.Items.Count > 0)
            {
                ListView.ScrollIntoView(ListView.Items[ListView.Items.Count - 1]);
            }
        }
    }
}
