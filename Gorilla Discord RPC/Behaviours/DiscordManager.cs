using System;
using System.Threading;
using Discord;
using GorillaNetworking;
using Photon.Pun;
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


        public static int PlayerCount;

        private string GetCurrentMap()
        {
            if (PhotonNetwork.InRoom)
            {
                return PhotonNetworkController.Instance.currentJoinTrigger.gameModeName;
            }
            return "noroom"; // sets the large image to gorilla if no room
        }

        private void Update()
        {
            try
            {
                discord.RunCallbacks();
            }
            catch
            {
                Destroy(gameObject);
            }
        }

        private void Start()
        {
            Thread.Sleep(5000);

            discord = new Discord.Discord(applicationID, (System.UInt64)Discord.CreateFlags.NoRequireDiscord);

            time = System.DateTimeOffset.Now.ToUnixTimeMilliseconds();

            InvokeRepeating("UpdateMap", 0, 3); // probabakly not needed but i dont wantr to check every frame bc may cause lag and theres no reason to check it every fucking frame
        }

        private void LateUpdate()
        {
            try
            {
                var activityManager = discord.GetActivityManager();
                var activity = new Activity
                {
                    Details = details,
                    State = "In Room: ",
                    Assets = new ActivityAssets { LargeImage = largeImage },
                };
                if (PhotonNetwork.InRoom)
                {
                    var party = new ActivityParty
                    {
                        Id = PhotonNetwork.CurrentRoom.Name,
                        
                        Size = new PartySize
                        {
                            CurrentSize = PhotonNetwork.CurrentRoom.Players.Count,
                            MaxSize = 10
                        }
                    };
                }

                activityManager.UpdateActivity(activity, (result) => { });

                largeImage = map;
            }
            catch (Exception e)
            {
                Debug.LogError("uh oh: " + e.Message);
            }
        }

        public void UpdateMap() => map = GetCurrentMap();
    }
}