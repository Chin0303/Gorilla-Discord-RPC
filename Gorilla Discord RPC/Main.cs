using System.Reflection;
using BepInEx;
using BepInEx.Configuration;
using Gorilla_Discord_RPC.Behaviours;
using HarmonyLib;
using UnityEngine;

namespace Gorilla_Discord_RPC
{
    [BepInPlugin(GUID, NAME, VERSION)]
    internal class Main : BaseUnityPlugin
    {
        internal static ConfigEntry<bool> showRoomName;

        internal const string
            GUID = "chin.discordrpc",
            NAME = "Gorilla Discord RPC",
            VERSION = "1.0.0";

        internal Main() 
        {
            new Harmony("chin.discordrpc").PatchAll(Assembly.GetExecutingAssembly());
            showRoomName = Config.Bind("General", "ShowRoomCode", true, "When set to false your room code will not be shown on your activity");
        }

        public static void Init()
        {
            new GameObject("DRPC_Manager").AddComponent<DiscordManager>();

            Debug.Log("Init Done - discord rpc ");
        }
    }

    [HarmonyPatch(typeof(GorillaTagger), "Awake")]
    internal class GorillaTagInitDone
    {
        public static void Postfix() => Main.Init();
    }
}

