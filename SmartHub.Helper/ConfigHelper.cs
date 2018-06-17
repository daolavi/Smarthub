using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace SmartHub.Helper
{
    public static class ConfigHelper
    {
        private static IDictionary<string, string> _appSettings = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

        public static string AppSettings(string name, string defaultValue = null)
        {
            if (_appSettings.ContainsKey(name))
            {
                return _appSettings[name];
            }
            return _appSettings[name] = (ConfigurationManager.AppSettings[name] ?? defaultValue);
        }

        public static T AppSettings<T>(string name, T defaultValue = default(T)) where T : struct
        {
            var setting = AppSettings(name);
            if (string.IsNullOrWhiteSpace(setting))
            {
                return defaultValue;
            }
            try
            {
                return (T)Convert.ChangeType(setting, typeof(T));
            }
            catch (InvalidCastException)
            {
                return defaultValue;
            }
        }
    }
}
