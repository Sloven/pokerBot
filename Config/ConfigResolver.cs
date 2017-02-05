using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Config
{
    public class ConfigResolver
    {
        public static string GetSetting(string name)
        {
            var config = ReadConfig("Custom.config");
            
            string value = string.Empty;
            if(!String.IsNullOrEmpty(name))
                value = config.AppSettings.Settings[name].Value;
            return value;
        }

        private static Configuration ReadConfig(string configFilePath)
        {
            ExeConfigurationFileMap fileMap = new ExeConfigurationFileMap();
            fileMap.ExeConfigFilename = configFilePath;

            // Retrieve the config file.
            Configuration configuration = ConfigurationManager.OpenMappedExeConfiguration(fileMap, ConfigurationUserLevel.None);
            return configuration;
        }
    }
}
