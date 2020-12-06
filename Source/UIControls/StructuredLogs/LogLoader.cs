using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace JsonFlier.UIControls.LogTabs.JsonLog
{
    public class LogLoader
    {
        private const int BufferSize = 512;

        private readonly string filePath;
        private readonly Dispatcher dispatcher;
        private readonly StructuredLogViewModel logViewer;

        private Queue<LogEntryViewModel> logsQueue;
        private bool isCompleted;
        private bool interrupt = false;

        private Task refreshingTask;
        private Task parsingTask;

        public LogLoader(string filePath, Dispatcher dispatcher, StructuredLogViewModel logViewer)
        {
            this.filePath = filePath;
            this.dispatcher = dispatcher;
            this.logViewer = logViewer;
        }

        public Task Load()
        {
            logsQueue = new Queue<LogEntryViewModel>(BufferSize);
            isCompleted = false;

            refreshingTask = Task.Run(RefreshViewModel);
            parsingTask = Task.Run(LoadLogsFromFile);

            return parsingTask;
        }

        public void Interrupt()
        {
            if (refreshingTask != null || parsingTask != null || logsQueue != null)
            {
                interrupt = true;
                refreshingTask?.Wait();
                parsingTask?.Wait();
            }
        }

        private void RefreshViewModel()
        {
            while (!isCompleted || logsQueue.Count > 0)
            {
                if (dispatcher.HasShutdownStarted || interrupt)
                    return;

                try
                {
                    dispatcher.Invoke(() =>
                    {
                        while (logsQueue.Count > 0 && !interrupt)
                        {
                            var entry = logsQueue.Dequeue();
                            if (entry != null)
                                logViewer.AddEntry(entry);
                        }

                        logViewer.MessageTray = $"Loaded {logViewer.VisibleEntries.Count} entries";
                    });
                }
                catch (TaskCanceledException) { break; }
                Task.Delay(200);
            }
        }

        private void LoadLogsFromFile()
        {
            using (var fileStream = File.Open(filePath, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            using (var reader = new StreamReader(fileStream))
            {
                while (!reader.EndOfStream && !interrupt)
                {
                    LogEntryViewModel entry = LogEntryViewModel.FromString(reader.ReadLine());
                    if (logViewer.DateFilter.Filter(entry.DateTime) && logViewer.LevelFilter.Filter(entry.Level))
                    {
                        while (logsQueue.Count >= BufferSize)
                            Task.Delay(100);
                        logsQueue.Enqueue(entry);
                    }
                }
            }

            isCompleted = true;
        }
    }
}
