using GalaSoft.MvvmLight;
using JsonFlier.Context;
using JsonFlier.Context.Tabs;
using JsonFlier.UIControls.CustomTabControl;
using JsonFlier.UIControls.Toolbars;
using JsonFlier.UIControls.Toolbars.PredefinedToolbars;
using System.Linq;

namespace JsonFlier.UIControls.Main
{
    public class MainControlViewModel : ViewModelBase
    {
        private ApplicationContext context;
        private MainControlContextMediator mediator;

        public MainControlViewModel()
        {
            context = new ApplicationContext();
            mediator = new MainControlContextMediator(context, this);

            ToolbarTray = new ToolbarTrayContainerViewModel();
            TabManager = new TabControlViewModel();
            MainMenuViewModel = new MainMenuViewModel(context);

            ToolbarTray.Toolbars.Add(FileToolbar.Create(context));

            Initialize();
        }

        public ToolbarTrayContainerViewModel ToolbarTray { get; }
        public TabControlViewModel TabManager { get; }
        public MainMenuViewModel MainMenuViewModel { get; }

        private void Initialize()
        {
            if (context.BookmarkManager.Bookmarks.Any(b => b.OpenOnStartup))
                context.BookmarkManager.OpenStartupBookmarks();
            else
                ShowStartPage();
        }

        private void ShowStartPage()
        {
            var viewModel = new StartPageViewModel(context);
            var startTab = new TabContext()
            {
                Name = "Start page",
                TabInternalControl = new StartPage() { DataContext =  viewModel},
                TabControlType = Enums.TabControlType.Other,
            };
            context.TabManager.Add(startTab);
            viewModel.SetDeletionOnNewTab(context.TabManager, startTab);
        }
    }
}
