using GalaSoft.MvvmLight;
using JsonFlier.Enums;
using JsonFlier.Utilities;
using System;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace JsonFlier.UIControls.LogTabs.JsonLog
{
    public class StructuredLogViewModel : ViewModelBase, IRefreshable
    {
        private LogLoader logLoader;

        public StructuredLogViewModel()
        {
            DateFilter = new DateFilter();
            DateFilter.OnChanged += DateFilter_OnChanged;
            LevelFilter = new LevelFilter();
            LevelFilter.OnChanged += LevelFilter_OnChanged;
        }

        private bool _AutoRefresh;
        public bool AutoRefresh { get => _AutoRefresh; set { _AutoRefresh = value; RaisePropertyChanged(nameof(AutoRefresh)); } }


        private string _FilePath;
        public string FilePath { get => _FilePath; set { _FilePath = value; RaisePropertyChanged(nameof(FilePath)); } }


        private DateTime _FilterStart;
        public DateTime FilterStart { get => _FilterStart; set { _FilterStart = value; RaisePropertyChanged(nameof(FilterStart)); } }


        private DateTime _FilterEnd;
        public DateTime FilterEnd { get => _FilterEnd; set { _FilterEnd = value; RaisePropertyChanged(nameof(FilterEnd)); } }


        private LogLevel _MinimumLevel;
        public LogLevel MinimumLevel { get => _MinimumLevel; set { _MinimumLevel = value; RaisePropertyChanged(nameof(MinimumLevel)); } }


        private string _MessageTray;
        public string MessageTray { get => _MessageTray; set { _MessageTray = value; RaisePropertyChanged(nameof(MessageTray)); } }

        private LogEntryViewModel _SelectedEntry;
        public LogEntryViewModel SelectedEntry { get => _SelectedEntry; set { _SelectedEntry = value; RaisePropertyChanged(nameof(SelectedEntry)); } }

        public BindingList<LogEntryViewModel> VisibleEntries { get; } = new BindingList<LogEntryViewModel>();

        public DateFilter DateFilter { get; set; }
        public LevelFilter LevelFilter { get; set; }

        public Task LoadAsync(Dispatcher dispatcher)
        {
            logLoader = new LogLoader(FilePath, dispatcher, this);
            return logLoader.Load();
        }

        public void AddEntry(LogEntryViewModel entry)
        {
            VisibleEntries.Add(entry);
            entry.OnSelected += Entry_OnSelected;
        }

        public void RemoveEntryAt(int index)
        {
            if (SelectedEntry == VisibleEntries[index])
                SelectedEntry = null;
            VisibleEntries.RemoveAt(index);
        }

        private void Entry_OnSelected(object sender, EventArgs e)
        {
            LogEntryViewModel selectedEntry = (LogEntryViewModel)sender;

            foreach (var entry in VisibleEntries)
            {
                if (entry != selectedEntry)
                    entry.IsSelected = false;
            }

            SelectedEntry = selectedEntry;
        }

        public bool CanRefresh()
        {
            return true;
        }

        public Task Refresh()
        {
            VisibleEntries.Clear();
            SelectedEntry = null;
            logLoader?.Interrupt();
            logLoader = new LogLoader(FilePath, Dispatcher.CurrentDispatcher, this);
            return Task.Run(logLoader.Load);
        }

        private void DateFilter_OnChanged(bool narrowingDown)
        {
            if (narrowingDown)
            {
                for (int i = 0; i < VisibleEntries.Count; i++)
                {
                    if (!DateFilter.Filter(VisibleEntries[i].DateTime))
                    {
                        RemoveEntryAt(i--);
                    }
                }
            }
            else
            {
                Refresh();
            }
        }

        private void LevelFilter_OnChanged(bool narrowingDown)
        {
            if (narrowingDown)
            {
                for (int i = 0; i < VisibleEntries.Count; i++)
                {
                    if (!LevelFilter.Filter(VisibleEntries[i].Level))
                    {
                        RemoveEntryAt(i--);
                    }
                }
            }
            else
            {
                Refresh();
            }
        }
    }
}
