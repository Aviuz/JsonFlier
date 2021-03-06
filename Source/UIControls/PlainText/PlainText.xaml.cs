﻿using JsonFlier.Utilities;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace JsonFlier.UIControls.LogTabs.PlainText
{
    /// <summary>
    /// Interaction logic for PlainText.xaml
    /// </summary>
    public partial class PlainText : UserControl, IRefreshable
    {
        private string filePath;

        public PlainText(string text, string filePath = null)
        {
            InitializeComponent();

            this.filePath = filePath;

            textBlock.Text = text;

            int lineCount = text.Split('\n').Length;

            var sb = new StringBuilder();

            for (int i = 1; i <= lineCount; i++)
            {
                sb.AppendLine(i.ToString());
            }

            LnBlock.Text = sb.ToString();
        }

        public bool CanRefresh()
        {
            return !string.IsNullOrWhiteSpace(filePath);
        }

        private void ScrollViewer_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            LeftScroller.ScrollToVerticalOffset(RightScroller.VerticalOffset);
        }

        public Task Refresh()
        {
            if (!CanRefresh())
                throw new System.Exception("Can't refresh when there is no file origin");

            try
            {
                textBlock.Text = File.ReadAllText(filePath);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "Error");
            }

            return Task.CompletedTask;
        }
    }
}
