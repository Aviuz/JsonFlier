using System;
using System.ComponentModel;

namespace JsonFlier.Context.Tabs
{
    public class TabManager
    {
        public delegate void TabChangedEventHandler(TabManager sender, TabContext oldTab, TabContext newTab);
        public delegate void BaseTabManagerEventHandler(TabManager sender, TabContext tab);

        public event TabChangedEventHandler OnTabChanged;
        public event BaseTabManagerEventHandler OnAddedTab;
        public event BaseTabManagerEventHandler OnRemovedTab;

        public ApplicationContext Context { get; }

        private TabContext currentTab;
        public TabContext CurrentTab { get => currentTab; set => ChangeTab(value); }

        public BindingList<TabContext> Tabs { get; private set; } = new BindingList<TabContext>();

        public TabManager(ApplicationContext context)
        {
            Context = context;
        }

        public void Add(TabContext tabContext)
        {
            if (Tabs.Contains(tabContext))
                throw new Exception("Tab is already in Tabs collection");

            Tabs.Add(tabContext);

            OnAddedTab?.Invoke(this, tabContext);
        }

        public void Remove(TabContext tabContext)
        {
            Tabs.Remove(tabContext);
            if (CurrentTab == tabContext)
                CurrentTab = null;

            OnRemovedTab?.Invoke(this, tabContext);
        }

        private void ChangeTab(TabContext newTab)
        {
            if (newTab == currentTab)
                return;

            if (newTab != null && !Tabs.Contains(newTab))
                throw new Exception($"Tab {newTab} is from outside of tabs collection. You need to select tab from Tabs collection");

            var oldValue = currentTab;
            currentTab = newTab;
            OnTabChanged.Invoke(this, oldValue, newTab);
        }
    }
}
