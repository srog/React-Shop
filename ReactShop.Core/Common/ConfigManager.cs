using System.Configuration;

namespace ReactShop.Core.Common
{
    public class ConfigManager : IConfigManager
    {
        public static AppSettingsReader reader = new AppSettingsReader();

        public string GetValue(string configName)
        {
            return reader.GetValue(configName, typeof(string)).ToString();
        }
    }
}
