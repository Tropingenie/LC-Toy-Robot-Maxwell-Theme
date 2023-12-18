using System;
using System.Collections.Generic;
using System.Text;
using HarmonyLib;
using UnityEngine;

namespace Maxwell.Patches
{
    [HarmonyPatch(typeof(AnimatedItem))]
    internal class RobotToySound
    {
        [HarmonyPatch("EquipItem")]
        [HarmonyPrefix]
        static void Prefix(AnimatedItem __instance)
        {
            try
            {
                if (__instance.grabAudio.name != AudioLoad.Clip.name || __instance.grabAudio != null)
                {
                    if (__instance.grabAudio.name == "RobotToyCheer")
                    {
                        Plugin.LogInfo($"Set robot audio to maxwell");
                        __instance.grabAudio = AudioLoad.Clip;
                    }

                }
            }
            catch (Exception ex)
            {
                Plugin.LogError($"{ex.Message}\n{ex.StackTrace}");
            }
            /*
            if (((Component)(object)__instance).gameObject.GetComponent<MeshFilter>().mesh == __instance.alternateMesh)
            {
                Plugin.LogInfo($"Item is alt mesh. Audio name: {__instance.dropAudio.name}");
            }
            else if (__instance.grabAudio.name == AudioLoad.Clip.name)
            {
                Plugin.LogInfo($"{__instance.grabAudio.name} already {AudioLoad.Clip.name}");
            }
            else 
                __instance.grabAudio = AudioLoad.Clip;
            */
        }
    }
}
