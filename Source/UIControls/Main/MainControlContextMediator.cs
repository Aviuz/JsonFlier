using JsonFlier.Context.Tabs;
using JsonFlier.UIControls.CustomTabControl;
using JsonFlier.UIControls.Main;
using System.Collections.Generic;

namespace JsonFlier.Context
{
    public class MainControlContextMediator
    {
        private Dictionary<TabContext, TabCardViewModel> TabBindings = new Dictionary<TabContext, TabCardViewModel>();

        public ApplicationContext ApplicationContext { get; }
        public MainControlViewModel ViewModel { get; }

        public MainControlContextMediator(ApplicationContext applicationContext, MainControlViewModel viewModel)
        {
            ApplicationContext = applicationContext;
            ViewModel = viewModel;
            WireUp();
        }

        protected void WireUp()
        {
            ApplicationContext.TabManager.OnAddedTab += TabManager_OnAddedTab;
            ApplicationContext.TabManager.OnTabChanged += TabManager_OnTabChanged;
            ApplicationContext.TabManager.OnRemovedTab += TabManager_OnRemovedTab;
        }

        private void TabManager_OnAddedTab(TabManager sender, TabContext tabContext)
        {
            var tabViewModel = new TabCardViewModel() { Name = tabContext.Name, TabContent = tabContext.TabInternalControl };
            TabBindings[tabContext] = tabViewModel;

            tabViewModel.OnSelected += new TabCardViewModel.TabEventHandler(_ =>
            {
                ApplicationContext.TabManager.CurrentTab = tabContext;
            });
            tabViewModel.OnClose += new TabCardViewModel.TabEventHandler(_ =>
            {
                ApplicationContext.TabManager.Remove(tabContext);
            });

            ViewModel.TabManager.Tabs.Add(tabViewModel);
        }

        private void TabManager_OnTabChanged(TabManager sender, TabContext oldTab, TabContext newTab)
        {
            if (oldTab != null)
            {
                foreach (var toolbar in oldTab.Toolbars)
                    ViewModel.ToolbarTray.Toolbars.Remove(toolbar);
            }

            if (newTab != null)
            {
                foreach (var toolbar in newTab.Toolbars)
                    ViewModel.ToolbarTray.Toolbars.Add(toolbar);

                TabBindings[newTab].Select();
            }
        }

        private void TabManager_OnRemovedTab(TabManager sender, TabContext tab)
        {
            ViewModel.TabManager.Tabs.Remove(TabBindings[tab]);
            TabBindings.Remove(tab);
        }
    }
}
