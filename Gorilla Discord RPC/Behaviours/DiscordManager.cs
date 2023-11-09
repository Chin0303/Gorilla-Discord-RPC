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

        internal string largeImage;
        internal string smallImage;

        internal long time;

        internal string gameMode;

        internal string roomState;

        internal Discord.Discord discord;

        internal Activity activity;

        public static int PlayerCount;

        private string GetCurrentMap()
        {
            if (PhotonNetwork.InRoom)
            {
                return PhotonNetworkController.Instance.currentJoinTrigger.gameModeName;
            }
            return "stump"; // sets the smoll image to stump image if no room
        }

        private string GetGameMode()
        {
            switch (GorillaComputer.instance.currentGameMode)
            {
                case "CASUAL":
                    return "Playing Casual";
                case "INFECTION":
                    return "Playing Infection";
                case "HUNT":
                    return "Playing Hunt";
                case "BATTLE":
                    return "Playing PaintBrawl";
                default:
                    return "In A Modded Lobby"; // too lazy to also write down the modded gamemode and it will take up alot of space on the activity
            }
        }

        private string GetRoomState()
        {
            if(PhotonNetworkController.Instance.isPrivate)
            {
                return "In A Private Room: ";
            }
            else 
                return "In A Public Room: ";
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
            InvokeRepeating("CheckRoomState", 0, 10);
            InvokeRepeating("CheckGameMode", 0, 10);
        }

        private void LateUpdate()
        {
            try
            {
                var activityManager = discord.GetActivityManager();

                if (PhotonNetwork.InRoom)
                {
                    activity = new Activity
                    {
                        Details = gameMode,
                        State = roomState + PhotonNetwork.CurrentRoom.Name,
                    };
                }

                activity.Assets = new ActivityAssets
                {
                    LargeImage = "default",
                    LargeText = "Gorilla Tag",

                    SmallImage = smallImage,
                    SmallText = char.ToUpper(smallImage[0]) + smallImage.Substring(1), // to capitalize the first letter of the string so it looks better on the activity
                };

                activity.Timestamps = new ActivityTimestamps
                {
                    Start = time
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

            }
            catch (Exception e)
            {
                Debug.LogError("uh oh: " + e.Message);
            }
        }

        public void UpdateMap() => smallImage = GetCurrentMap();
        public void CheckRoomState() => roomState = GetRoomState();
        public void CheckGameMode() => gameMode = GetGameMode();
    }
}