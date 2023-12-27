#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
using System;
using System.Collections.Generic;
using System.Text;
using AchievementTracker.BaseClasses;
using BaseModule = AchievementTracker.BaseClasses.BaseModule;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using System.Threading.Tasks;

namespace AchievementTracker.Modules
{
    public class BasicCommandsModule : BaseModule
    {
        [Command("ping"), Description("Check the ping of the bot.")]
        public async Task Ping(CommandContext ctx)
        {
            await ctx.TriggerTypingAsync();
            await ctx.RespondAsync($"{ctx.Client.Ping} ms");
        }
    }
}
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member