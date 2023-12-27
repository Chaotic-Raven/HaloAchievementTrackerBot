using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using AchievementTracker.BaseClasses;
using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using SteamWebAPI2;
using SteamWebAPI2.Utilities;
using SteamWebAPI2.Interfaces;
using Steam.Models.SteamStore;
using SteamWebAPI2.Models.SteamStore;
using Steam.Models.SteamPlayer;
using System.Net.Http;
using System.Collections;

namespace AchievementTracker.Modules
{
    class LoadModule : BaseModule
    {
        private static SteamWebInterfaceFactory steamInterface = new SteamWebInterfaceFactory(/*Steam Web API Key*/);
        private static SteamUser steam = null;
        
        [Command("update"), Description("Updates current achieved achievements.")]
        public async Task Update(CommandContext ctx)
        {

            Console.WriteLine("UPDATE ACTIVATED");
            steam = steamInterface.CreateSteamWebInterface<SteamUser>(new HttpClient());
            var steamStats = steamInterface.CreateSteamWebInterface<SteamUserStats>(new HttpClient());
            
            var achievementsToaster = await steamStats.GetPlayerAchievementsAsync(976730, /*Insert Steam64ID Here*/);
            var achievementsOnyx = await steamStats.GetPlayerAchievementsAsync(976730, /*Insert Second Steam64ID Here*/);

            var tAchEnum = achievementsToaster.Data.Achievements.GetEnumerator();
            var oAchEnum = achievementsOnyx.Data.Achievements.GetEnumerator();
            //Toaster Enumerator
            tAchEnum.MoveNext();
            //Onyx Enumerator
            oAchEnum.MoveNext();

            /*
                0 = General Achievments
                1 = Halo CE Achievements
                2 = Halo 2 Achievements
                3 = Halo 3 Achievements
                4 = Halo 4 Achievements
                5 = Halo Reach Achievements
                6 = Halo ODST Achievemnts

                This string array contains the temporary messages for each Halo Game.
            */
            string[] strArray = new string[7] {"", "", "", "", "", "", ""};
            /*
             * To minimize 
             */
            ArrayList genArrayList = new ArrayList();
            ArrayList hCEArrayList = new ArrayList();
            ArrayList h2ArrayList = new ArrayList();
            ArrayList h3ArrayList = new ArrayList();
            ArrayList h4ArrayList = new ArrayList();
            ArrayList hRArrayList = new ArrayList();
            ArrayList hODSTArrayList = new ArrayList();
            string returnString;
            string tempDesc;
            string tAchieved;
            string oAchieved;

            while (tAchEnum.Current != null)
            {
                tempDesc = tAchEnum.Current.Description;
                
                if (tAchEnum.Current.Achieved == 0)
                {
                    tAchieved = ":x:";
                }
                else
                {
                    tAchieved = ":white_check_mark:";
                }

                if (oAchEnum.Current.Achieved == 0)
                {
                    oAchieved = ":x:";
                }
                else
                {
                    oAchieved = ":white_check_mark:";
                }

                returnString = "\n" + tempDesc + "\n[Toast: " + tAchieved + "] : [Onyx: " + oAchieved + "]\n";

                if (tempDesc.Contains("Halo: Reach: "))
                {
                    if ((returnString.Length + strArray[5].Length) > 1998)
                    {
                        strArray[5] += "\n";
                        hRArrayList.Add(strArray[5]);
                        strArray[5] = "";
                        strArray[5] += returnString;
                    }
                    else
                    {
                        strArray[5] += returnString;
                    }
                }
                else if (tempDesc.Contains("Halo CE: ") || tempDesc.Contains("Halo: CE:"))
                {

                    if ((returnString.Length + strArray[1].Length) > 1998)
                    {
                        strArray[1] += "\n";
                        hCEArrayList.Add(strArray[1]);
                        strArray[1] = "";
                        strArray[1] += returnString;
                    }
                    else
                    {
                        strArray[1] += returnString;
                    }
                }
                else if (tempDesc.Contains("Halo 2: ") || tempDesc.Contains("Halo 2A MP: "))
                {
                    if ((returnString.Length + strArray[2].Length) > 1998)
                    {
                        strArray[2] += "\n";
                        h2ArrayList.Add(strArray[2]);
                        strArray[2] = "";
                        strArray[2] += returnString;
                    }
                    else
                    {
                        strArray[2] += returnString;
                    }
                }
                else if (tempDesc.Contains("Halo 3: "))
                {
                    if ((returnString.Length + strArray[3].Length) > 1998)
                    {
                        strArray[3] += "\n";
                        h3ArrayList.Add(strArray[3]);
                        strArray[3] = "";
                        strArray[3] += returnString;
                    }
                    else
                    {
                        strArray[3] += returnString;
                    }
                }
                else if (tempDesc.Contains("Halo 4: "))
                {
                    if ((returnString.Length + strArray[4].Length) > 1998)
                    {
                        strArray[4] += "\n";
                        h4ArrayList.Add(strArray[4]);
                        strArray[4] = "\n";
                        strArray[4] += returnString;
                    }
                    else
                    {
                        strArray[4] += returnString;
                    }
                }
                else if (tempDesc.Contains("H3: ODST: "))
                {
                    if ((returnString.Length + strArray[6].Length) > 1998)
                    {
                        strArray[6] += "\n";
                        hODSTArrayList.Add(strArray[6]);
                        strArray[6] = "";
                        strArray[6] += returnString;
                    }
                    else
                    {
                        strArray[6] += returnString;
                    }
                }
                else
                {
                    if((returnString.Length + strArray[0].Length) > 1998)
                    {
                        strArray[0] += "\n";
                        genArrayList.Add(strArray[0]);
                        strArray[0] = "";
                        strArray[0] += returnString;
                    }
                    else
                    {
                        strArray[0] += returnString;
                    }
              
                }

                tAchEnum.MoveNext();
                oAchEnum.MoveNext();
            }
            h4ArrayList.Add(strArray[4]);
            Console.WriteLine(strArray[0]);
            foreach (string s in h4ArrayList)
            {

                await ctx.RespondAsync(s);
                

            }

            var mess = await ctx.Channel.GetMessagesAsync(100);
            await ctx.Channel.DeleteMessagesAsync(mess);
            ulong test1Server = /*Discord Server ID*/;
            var halo2 = await ctx.Client.GetChannelAsync(test1Server);
            await chnl.SendMessageAsync("TEST");

        }


    }
}
