#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AchievementTracker.BaseClasses;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;

namespace AchievementTracker.Modules
{
    public class AchievementModule : BaseModule
    {
        [Command("info"), Description("Gets info about an achievement")]
        public async Task Info(CommandContext ctx, string achName)
        {
            Console.WriteLine("INFO WORKS");
            // get achievement details here

            /*Placeholder Display Message for Testing*/
            var embed = new DiscordEmbedBuilder()
            {
                Color = DiscordColor.Blurple,
                Title = achName,
                Description = "Ach Desc",
                ThumbnailUrl = "https://steamcdn-a.akamaihd.net/steamcommunity/public/images/apps/976730/4e250b0f9b7f6c2c0e3f7e1cfc05715698222097.jpg"
            };

            embed.AddField("Toaster", ":white_check_mark: ", true);
            embed.AddField("Onyx", ":x:", true);

            await ctx.RespondAsync(embed: embed.Build());
            await ctx.RespondAsync(embed: embed.Build());
        }
    }
}

#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member