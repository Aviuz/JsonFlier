﻿using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace JsonFlier.UserControls.Logs
{
    public class LogEntryModel
    {
        public string Title { get; set; }

        public string Date { get; set; }

        public string Time { get; set; }

        public string Category { get; set; }

        public string DataType { get; set; }

        public FontWeight FontWeight { get; set; }

        public FontStyle FontStyle { get; set; }

        public JObject Json { get; set; }

        public JToken Data => Json != null ? Json["data"] : null;

        public Control DetailView { get; set; }

        public static LogEntryModel FromJObject(JObject log)
        {
            return new LogEntryModel()
            {
                Title = log["title"]?.ToString(),
                Category = log["category"]?.ToString(),
                DataType = log["data"] != null ? log["dataType"]?.ToString() : null,
                FontWeight = log["data"] != null && log["dataType"] != null ? FontWeights.Bold : FontWeights.Normal,
                FontStyle = log["category"]?.ToString() == "Trace" ? FontStyles.Italic : FontStyles.Normal,
                Date = log["date"]?.ToString(),
                Time = log["time"]?.ToString(),
                Json = log
            };
        }
    }
}
