using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace DataAccess.Data
{
    public static class SettingsContext
    {
        private static Settings _settings { get; set; }

        public static async void GetSettingsInformation()
        {
            var settingsFile = "{\"status\": [\"new\", \"ongoing\", \"closed\"], \"maxItemsCount\": 10}";
            _settings = JsonConvert.DeserializeObject<Settings>(settingsFile);
        }


        public static async Task<IEnumerable<string>> GetStatus()
        {
            var list = new List<string>();

            foreach (var status in _settings.status)
            {
                list.Add(status);
            }

            return list;
        }

        public static int GetMaxItemsCount()
        {
            return _settings.maxItemsCount;
        }
    }



    public class Settings
    {
        public string[] status { get; set; }
        public int maxItemsCount { get; set; }
    }
}
