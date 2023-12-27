using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Newtonsoft.Json;

namespace AchievementTracker.Utils
{
    /// <summary>
    /// Represents the application configuration file (config.json)
    /// for changing bot settings.
    /// </summary>
    public class AppConfig
    {
        private const string FILENAME = "config.json";

        /// <summary>
        /// The Discord Bot Token. Used for authenticating with the
        /// Discord API as the bot.
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// The prefix to use with the bot.
        /// </summary>
        public string Prefix { get; set; }

        /// <summary>
        /// Allows printing additional debug information to the console.
        /// </summary>
        public bool DebugMode { get; set; }

        /// <summary>
        /// Loads the app configuration settings from the config.json
        /// file.
        /// </summary>
        /// <returns>An <see cref="AppConfig"/> class instance.</returns>
        public static AppConfig Load()
        {
            Debug.Info($"Loading configuration file from: {new FileInfo(FILENAME).FullName}");
            // First check if the config file exists.
            if (!File.Exists(FILENAME))
            {
                // If the file doesn't exist, create a new one with
                // the default config options.
                return GetDefault();
            }
            else
            {
                // Read the configuration file and attempt to load
                // the data into an AppConfig instance.
                string data = File.ReadAllText(FILENAME);

                // Initialize our variable.
                AppConfig cfg = null;
                try
                {
                    // Attempt to deserialize the file contents to this class.
                    cfg = JsonConvert.DeserializeObject<AppConfig>(data);
                }
                catch (Exception ex)
                {
                    // Something borked!!!
                    Debug.Error(ex);
                    cfg = GetDefault();
                }
                // Return default or the actual config from the file.
                return cfg;
            }
        }

        /// <summary>
        /// Saves the current configuration to the config.json file.
        /// </summary>
        public void Save()
        {
            string data = JsonConvert.SerializeObject(this, Formatting.Indented);
            File.WriteAllText(FILENAME, data);
        }

        private static AppConfig GetDefault()
        {
            AppConfig cfg = new AppConfig()
            {
                DebugMode = false,
                Prefix = ".",
                Token = ""
            };
            cfg.Save();
            return cfg;
        }
    }
}
