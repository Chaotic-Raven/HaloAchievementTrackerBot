using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using AchievementTracker.Utils;
using DSharpPlus;
using DSharpPlus.CommandsNext;

namespace AchievementTracker
{
    class Startup
    {
        /// <summary>
        /// Global reference to the application config.
        /// </summary>
        public static AppConfig Config { get; private set; }

        /// <summary>
        /// Global reference to the <see cref="DiscordClient"/> client instance.
        /// </summary>
        public static DiscordClient Client { get; private set; }

        // DSharpPlus Commands Module
        private static CommandsNextModule m_Commands;

        static void Main(string[] args)
        {
            // Load the application config.
            Config = AppConfig.Load();

            // Initialize the bot and connect to Discord.
            BotStartup().ConfigureAwait(false).GetAwaiter().GetResult();
        }

        private static async Task BotStartup()
        {
            Client = new DiscordClient(new DiscordConfiguration
            {
                Token = Config.Token,
                TokenType = TokenType.Bot,
                LogLevel = LogLevel.Debug,
                UseInternalLogHandler = false,
                DateTimeFormat = "dd-MM-yyyy HH:mm:ss zzz"
            });

            // Register internal logging system
            Client.DebugLogger.LogMessageReceived += Debug.LogMessageReceived;

            m_Commands = Client.UseCommandsNext(new CommandsNextConfiguration
            {
                CaseSensitive = false,
                EnableMentionPrefix = true,
                StringPrefix = Config.Prefix
            });

            // Load all modules under AchievementTracker.Modules namespace into CommandsNext
            foreach (Type module in Helpers.GetTypesInNamespace(Assembly.GetExecutingAssembly(), "AchievementTracker.Modules"))
            {
                // Check if this module derives from AchievementTracker.BaseClasses.BaseModule
                if (module.BaseType == typeof(BaseClasses.BaseModule))
                {
                    // Register the module
                    m_Commands.RegisterCommands(module);
                }
            }

            // Connect to Discord
            await Client.ConnectAsync();
            await Task.Delay(-1);
        }
    }
}
