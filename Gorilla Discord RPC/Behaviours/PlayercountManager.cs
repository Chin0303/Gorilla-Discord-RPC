using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Photon.Pun;
using Photon.Realtime;

namespace Gorilla_Discord_RPC.Behaviours
{
    internal class PlayercountManager : MonoBehaviourPunCallbacks
    {
        public override void OnPlayerEnteredRoom(Player newPlayer)
        {
            base.OnPlayerEnteredRoom(newPlayer);
            DiscordManager.UpdatePlayerCount();
            
        }

        public override void OnPlayerLeftRoom(Player otherPlayer)
        {
            base.OnPlayerLeftRoom(otherPlayer);
            DiscordManager.UpdatePlayerCount();
        }

        public override void OnJoinedRoom()
        {
            base.OnJoinedRoom();
            DiscordManager.UpdatePlayerCount();
        }

        public override void OnLeftRoom()
        {
            base.OnLeftRoom();
            DiscordManager.UpdatePlayerCount();
        }
    }
}
