using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace JsonFlier.Context.Bookmarks
{
    public class Bookmark
    {
        public bool OpenOnStartup { get; set; }

        public string Name { get; set; }

        public IList<string> FilePaths { get; set; }

        public Bookmark()
        {
            FilePaths = new List<string>();
        }

        public string Serialize()
        {
            return JsonConvert.SerializeObject(this);
        }

        public override string ToString()
        {
            if (FilePaths.Count == 1)
                return ToStringSingle();
            else
                return ToStringMultifile();
        }

        private string ToStringSingle()
        {
            if (FilePaths[0] == Name || string.IsNullOrEmpty(Name))
                return FilePaths[0];
            else
                return $"{Name} ({FilePaths[0]})";
        }

        private string ToStringMultifile()
        {
            return $"{Name} ({string.Join(", ", FilePaths)})";
        }

        public static Bookmark FromJson(string json)
        {
            return JObject.Parse(json).ToObject<Bookmark>();
        }
    }
}
