using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Windows.Input;

namespace JsonFlier.UIControls.LogTabs.JsonLog.JsonTreeView
{
    public class TreeElementViewModel : ViewModelBase
    {
        public TreeElementViewModel()
        {
            ToggleCollapseCommand = new RelayCommand(() => IsCollapsed = !IsCollapsed);
        }

        public string Name { get; set; }
        public TreeElementViewModel[] Elements { get; set; }

        public string Value { get; set; }
        public string MultiLineValue { get; set; }

        public bool CollapseDisabled { get; set; }

        public bool HeaderHidden { get; set; }

        private bool _IsCollapsed;
        public bool IsCollapsed { get => _IsCollapsed; set { _IsCollapsed = value; RaisePropertyChanged(nameof(IsCollapsed)); } }

        public ICommand ToggleCollapseCommand { get; }
    }
}
