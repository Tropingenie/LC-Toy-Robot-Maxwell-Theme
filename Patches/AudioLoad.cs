using BepInEx;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.Networking;

namespace Maxwell.Patches
{
    internal static class AudioLoad
    {
        static bool FirstRun = true;

        static readonly List<string> FilePaths = new()
        {
            Path.Combine(Paths.PluginPath, "RobotToySound", "maxwell.wav"),
            Path.Combine(Paths.PluginPath, "Oni_Hazza-ToyRobot_MaxwellTheme", "RobotToySound", "maxwell.wav"),
            Path.Combine(Paths.PluginPath, "Oni_Hazza-ToyRobot_MaxwellTheme", "maxwell.wav")
        };

        static string FilePath = null;

        public static AudioClip Clip = null;

        public static bool FinishedLoading = false;

        public static bool FileFound = false;

        public static void Load()
        {
            if (FirstRun)
            {
                FirstRun = false;

                foreach (string p in FilePaths)
                {
                    if (File.Exists(p))
                    {
                        Plugin.LogInfo($"File {p} found");
                        FilePath = p;
                        FileFound = true;
                        break;
                    }
                    else Plugin.LogWarning($"File {p} not found");
                }

                if (!FileFound)
                {
                    Plugin.LogError("File maxwell.wav not found!");
                    return; // Clip will remain null here
                }

                SharedCoroutineStarter.StartCoroutine(LoadAudioClip(FilePath));
            }
        }

        private static IEnumerator LoadAudioClip(string filePath) // loads audio file into memory 
        {
            Plugin.LogInfo($"Loading {FilePath}");

            var loader = UnityWebRequestMultimedia.GetAudioClip(filePath, AudioType.WAV);

            loader.SendWebRequest();

            while (true)
            {
                if (loader.isDone) break;
                yield return null;
            }

            if (loader.error != null)
            {
                Plugin.LogError($"Error loading {FilePath}\n{loader.error}");
                FileFound = false;
                yield break;
            }

            var clip = DownloadHandlerAudioClip.GetContent(loader);
            if (clip && clip.loadState == AudioDataLoadState.Loaded)
            {
                Plugin.LogInfo($"Loaded {FilePath}");
                clip.name = Path.GetFileName(filePath);
                Clip = clip;
                FinishedLoading = true;
                yield break;
            }
        }
    }
}
