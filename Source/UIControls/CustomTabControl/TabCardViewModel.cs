using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Windows;
using System.Windows.Input;

namespace JsonFlier.UIControls.CustomTabControl
{
    public class TabCardViewModel : ViewModelBase
    {
        public delegate void TabEventHandler(TabCardViewModel tab);
        public event TabEventHandler OnSelected;
        public event TabEventHandler OnClose;

        public TabCardViewModel()
        {
            SelectCommand = new RelayCommand(Select);
            CloseCommand = new RelayCommand(Close);
        }

        private string _Name;
        public string Name { get => _Name; set { _Name = value; RaisePropertyChanged(nameof(Name)); } }

        private bool _IsSelected;
        public bool IsSelected { get => _IsSelected; set { _IsSelected = value; RaisePropertyChanged(nameof(IsSelected)); } }

        private UIElement _TabContent;
        public UIElement TabContent { get => _TabContent; set { _TabContent = value; RaisePropertyChanged(nameof(TabContent)); } }

        public ICommand SelectCommand { get; }
        public ICommand CloseCommand { get; }

        public void Select()
        {
            OnSelected?.Invoke(this);
        }

        public void Close()
        {
            OnClose?.Invoke(this);
        }
    }
}
