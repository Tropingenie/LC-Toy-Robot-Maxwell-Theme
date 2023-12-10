using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Text;

namespace Maxwell.Patches
{
    [HarmonyPatch(typeof(StartOfRound))]
    internal class StartOfRoundPatch
    {
        [HarmonyPatch("Start")] // this prefix activates when loading into a game
        static void Prefix() => AudioLoad.Load();

        /*[HarmonyPatch("Start")]
        [HarmonyPostfix]
        static void Postfix()
        {

        }*/
    }
}
