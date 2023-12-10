using System;
using System.Collections.Generic;
using System.Text;
using HarmonyLib;

namespace Maxwell.Patches
{
    [HarmonyPatch(typeof(AnimatedItem))]
    internal class RobotToySound
    {
        [HarmonyPatch("EquipItem")]
        [HarmonyPrefix]
        static void Prefix(AnimatedItem __instance)
        {
            if (__instance.grabAudio.name == AudioLoad.Clip.name)
                return;
            __instance.grabAudio = AudioLoad.Clip;
        }
    }
}
