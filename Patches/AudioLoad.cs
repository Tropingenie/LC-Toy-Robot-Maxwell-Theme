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

        static readonly string FilePath = Path.Combine(Paths.PluginPath, "RobotToySound", "maxwell.wav");

        public static AudioClip Clip = null;

        public static bool FinishedLoading = false;

        public static bool FileFound = true;

        public static void Load()
        {
            if (FirstRun)
            {
                FirstRun = false;

                if (!File.Exists(FilePath)) // Clip will remain null here
                {
                    FileFound = false;
                    Plugin.LogError($"File {FilePath} not found");
                    return;
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
