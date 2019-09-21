using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace JsonFlier.Bookmarks
{
    public class Bookmark
    {
        public bool OpenOnStartup { get; set; }

        public string Name { get; set; }

        public IList<LogFileInfo> LogFiles { get; set; }

        public Bookmark()
        {
            LogFiles = new List<LogFileInfo>();
        }

        public string Serialize()
        {
            return JsonConvert.SerializeObject(this);
        }

        public static Bookmark FromJson(string json)
        {
            return JObject.Parse(json).ToObject<Bookmark>();
        }
    }
}
