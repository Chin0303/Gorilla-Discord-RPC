using Discord;
using UnityEngine;

namespace Gorilla_Discord_RPC.Behaviours
{
    internal class DiscordManager : MonoBehaviour
    {
        #region IdkCoolName

        internal long applicationID;

        internal string
            details = "Gorilla Tag Discord RPC",
            state = "Current Map: ",
            largeImage = "Image",
            argeText = "Gorilla Tag";

        internal long time;

        internal Discord.Discord discord;
        #endregion

        void Awake()
        {

        }

        void Start()
        {
            discord = new Discord.Discord(applicationID, (System.UInt64)Discord.CreateFlags.NoRequireDiscord);

            time = System.DateTimeOffset.Now.ToUnixTimeMilliseconds();
        }

    }
}
