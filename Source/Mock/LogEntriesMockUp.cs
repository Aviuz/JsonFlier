using JsonFlier.UserControls.Logs;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsonFlier.Mock
{
    public class LogEntriesMockUp : List<LogEntryModel>
    {
        public static JObject[] SampleData = new JObject[]
        {
            JObject.Parse(@"{""title"": ""Message"", ""category"": ""Info"", ""dataType"": ""Text"", ""date"": ""11.09.2019"", ""time"": ""17:01:19""}"),
            JObject.Parse(@"{""title"": ""Object log"", ""category"": ""Trace"", ""dataType"": ""Object"", ""date"": ""11.09.2019"", ""time"": ""17:01:19"", ""data"": ""very long text""}"),
            JObject.Parse(@"{""title"": ""Test warning"", ""category"": ""Warning"", ""dataType"": ""Text"", ""date"": ""11.09.2019"", ""time"": ""17:01:19"", ""data"": ""very long text""}"),
            JObject.Parse(@"{""title"": ""Test exception"", ""category"": ""Critical"", ""dataType"": ""Exception"", ""date"": ""11.09.2019"", ""time"": ""17:01:19"", ""data"": ""very long text""}")
        };

        public LogEntriesMockUp()
        {
            Entries = new List<LogEntryModel>(SampleData.Select(d => LogEntryModel.FromJObject(d)).ToArray());
        }

        public List<LogEntryModel> Entries { get; set; }
    }
}
