using BepInEx;
using BepInEx.Configuration;
using HarmonyLib;
using System;

namespace Maxwell
{
    [BepInPlugin(modGUID, modName, modVersion)]
    public class Plugin : BaseUnityPlugin
    {
        private const string modGUID = "com.bepinex.plugin.MaxwellAlarm";
        private const string modName = "Maxwell Alarm";
        private const string modVersion = "1.0.0";

        private readonly Harmony harmony = new(modGUID);

        private static Plugin Instance;

        private ConfigEntry<bool> IsEnabled;


        private void Awake()
        {
            if (Instance == null)
                Instance = this;

            Logger.LogInfo($"{nameof(Plugin)} is loaded!");
        }

        public static void LogInfo(string message) => Instance.Logger.Log(BepInEx.Logging.LogLevel.Info, message);
        public static void LogWarning(string message) => Instance.Logger.Log(BepInEx.Logging.LogLevel.Warning, message);
        public static void LogError(string message) => Instance.Logger.Log(BepInEx.Logging.LogLevel.Error, message);
        public static void LogError(Exception ex) => Instance.Logger.Log(BepInEx.Logging.LogLevel.Error, ex);
    }
}
