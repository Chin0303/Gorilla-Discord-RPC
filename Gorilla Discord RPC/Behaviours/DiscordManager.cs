using System;
using System.Threading;
using Discord;
using GorillaNetworking;
using UnityEngine;

namespace Gorilla_Discord_RPC.Behaviours
{
    internal class DiscordManager : MonoBehaviour
    {
        internal long applicationID = 1028260474904657940;
        internal string details;
        internal string state;
        internal string largeImage;
        internal long time;
        internal string map;

        internal Discord.Discord discord;

        private string GetCurrentMap()
        {
            if (PhotonNetworkController.Instance != null && PhotonNetworkController.Instance.currentJoinTrigger != null)
            {
                return PhotonNetworkController.Instance.currentJoinTrigger.gameModeName;
            }
            return "Not in a room";
        }

        private void Start()
        {
            Thread.Sleep(5000);

            discord = new Discord.Discord(applicationID, (System.UInt64)Discord.CreateFlags.NoRequireDiscord);

            time = System.DateTimeOffset.Now.ToUnixTimeMilliseconds();

            InvokeRepeating("CheckMap", 0, 3);
        }

        private void LateUpdate()
        {
            try
            {
                var activityManager = discord.GetActivityManager();
                var activity = new Discord.Activity
                {
                    Details = details,
                    State = state,
                    Assets = new ActivityAssets { LargeImage = largeImage },
                };

                activityManager.UpdateActivity(activity, (result) => { });

                switch (map)
                {
                    case "forest":
                        largeImage = "forest";
                        break;
                    case "cave":
                        largeImage = "cave";
                        break;
                    case "canyon":
                        largeImage = "canyon";
                        break;
                    case "city":
                        largeImage = "city";
                        break;
                    case "mountain":
                        largeImage = "mountain";
                        break;
                    case "clouds":
                        largeImage = "clouds";
                        break;
                    case "basement":
                        largeImage = "basement";
                        break;
                    case "beach":
                        largeImage = "beach";
                        break;
                }
            }
            catch (Exception e)
            {
                Debug.LogError("uh oh: " + e.Message);
            }
        }

        private void CheckMap()
        {
            map = GetCurrentMap();
        }
    }
}