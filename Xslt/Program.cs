using System;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace Xslt
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Config config = GetConfig();

            if (config is null)
            {
                Exit.Error("Config file not found (config.json)");
                return;
            }

            if (!ConfigConfigured(config))
            {
                Exit.Error("Config file not configured properly");
                return;
            }

            FileWatcher fileWatcher = new(config);
            fileWatcher.WatchFile();
        }

        /// <summary>
        /// Gets configuration file (config.json) located in the same folder as executable
        /// </summary>
        /// <returns><see cref="Config" /> object if file exists, otherwise null</returns>
        static Config GetConfig()
        {
            try
            {
                string configFile = File.ReadAllText("config.json");
                return JsonSerializer.Deserialize<Config>(configFile);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return null;
        }

        /// <summary>
        /// Checks if fields are not null
        /// </summary>
        /// <param name="config"></param>
        /// <returns>boolean</returns>
        static bool ConfigConfigured(Config config)
        {
            return config.GetType().GetProperties().All(p => p.GetValue(config) is not null);
        }
    }
}
