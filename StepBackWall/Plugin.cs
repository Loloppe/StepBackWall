﻿using BeatSaberMarkupLanguage.Settings;
using IPA;
using IPA.Config;
using IPA.Loader;
using StepBackWall.Gameplay;
using StepBackWall.Settings;
using StepBackWall.Settings.UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using IPALogger = IPA.Logging.Logger;
using LogLevel = IPA.Logging.Logger.Level;

namespace StepBackWall
{
    class Plugin : IBeatSaberPlugin, IDisablablePlugin
    {
        public static string PluginName = "StepBackWall";
        public static SemVer.Version PluginVersion = new SemVer.Version("0.0.0"); // Default

        public void Init(IPALogger logger, [Config.Prefer("json")] IConfigProvider cfgProvider, PluginLoader.PluginMetadata metadata)
        {
            Logger.log = logger;

            Configuration.Init(cfgProvider);

            if (metadata?.Version != null)
            {
                PluginVersion = metadata.Version;
            }
        }

        public void OnApplicationQuit() => Unload();
        public void OnEnable() => Load();
        public void OnDisable() => Unload();

        public void OnActiveSceneChanged(Scene prevScene, Scene nextScene)
        {
            if (nextScene.name == "GameCore")
            {
                if (Configuration.IsStepBackWallEnabled)
                {
                    new GameObject(PluginName).AddComponent<StepBackWallEnabler>();
                }
            }
            else if (nextScene.name == "MenuCore")
            {
                BSMLSettings.instance.AddSettingsMenu("StepBack Wall", "StepBackWall.Settings.UI.Views.mainsettings.bsml", MainSettings.instance);
            }
        }

        public void OnApplicationStart() { }
        public void OnSceneLoaded(Scene scene, LoadSceneMode sceneMode) { }
        public void OnSceneUnloaded(Scene scene) { }
        public void OnUpdate() { }
        public void OnFixedUpdate() { }

        private void Load()
        {
            Configuration.Load();
            Logger.Log($"{PluginName} v{PluginVersion} has started.", LogLevel.Info);
        }

        private void Unload()
        {
            Configuration.Save();
        }
    }
}
