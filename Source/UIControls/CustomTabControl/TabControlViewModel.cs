using GalaSoft.MvvmLight;
using System.ComponentModel;
using System.Linq;

namespace JsonFlier.UIControls.CustomTabControl
{
    public class TabControlViewModel : ViewModelBase
    {
        public TabControlViewModel()
        {
            Tabs.ListChanged += Tabs_ListChanged;
        }

        private TabCardViewModel _SelectedTab;
        public TabCardViewModel SelectedTab { get => _SelectedTab; set { _SelectedTab = value; RaisePropertyChanged(nameof(SelectedTab)); } }

        private bool _IsEmpty = true;
        public bool IsEmpty { get => _IsEmpty; set { _IsEmpty = value; RaisePropertyChanged(nameof(IsEmpty)); } }

        public BindingList<TabCardViewModel> Tabs { get; } = new BindingList<TabCardViewModel>();

        public void SelectTab(TabCardViewModel selectedTab)
        {
            SelectedTab = selectedTab;
            foreach (var tab in Tabs.Where(t => t.IsSelected))
                tab.IsSelected = false;
            SelectedTab.IsSelected = true;
        }

        public void RemoveTab(TabCardViewModel tab)
        {
            Tabs.Remove(tab);
        }

        private void Tabs_ListChanged(object sender, ListChangedEventArgs e)
        {
            switch (e.ListChangedType)
            {
                case ListChangedType.ItemAdded:
                    TabCardViewModel tab = Tabs[e.NewIndex];
                    tab.OnSelected += SelectTab;
                    tab.OnClose += RemoveTab;
                    goto case ListChangedType.Reset;

                case ListChangedType.ItemDeleted:
                case ListChangedType.Reset:
                    if (!Tabs.Contains(SelectedTab))
                        SelectedTab = null;

                    if (Tabs.Count == 0)
                    {
                        IsEmpty = true;
                    }
                    else
                    {
                        if (SelectedTab == null)
                            Tabs[0].Select();
                        IsEmpty = false;
                    }
                    break;
            }
        }
    }
}