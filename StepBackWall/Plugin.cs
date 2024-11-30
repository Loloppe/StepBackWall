﻿using Hive.Versioning;
using IPA;
using IPA.Config;
using IPA.Loader;
using StepBackWall.Gameplay;
using StepBackWall.Settings;
using StepBackWall.Settings.UI;
using UnityEngine;
using IPALogger = IPA.Logging.Logger;

namespace StepBackWall
{
    [Plugin(RuntimeOptions.DynamicInit)]
    public class Plugin
    {
        public static string PluginName => "StepBackWall";
        public static Version PluginVersion { get; private set; } = Version.Zero;

        [Init]
        public void Init(IPALogger logger, Config config, PluginMetadata metadata)
        {
            Logger.log = logger;
            Configuration.Init(config);

            if (metadata?.HVersion != null)
            {
                PluginVersion = metadata.HVersion;
            }
        }

        [OnEnable]
        public void OnEnable() => Load();
        [OnDisable]
        public void OnDisable() => Unload();

        private void OnGameSceneLoaded()
        {
            if (Configuration.EnableStepBackWalls
                && (BS_Utils.Plugin.LevelData.Mode == BS_Utils.Gameplay.Mode.None || !BS_Utils.Plugin.LevelData.GameplayCoreSceneSetupData.beatmapKey.beatmapCharacteristic.containsRotationEvents))
            {
                new GameObject(PluginName).AddComponent<StepBackWallEnabler>();
            }
        }

        private void Load()
        {
            Configuration.Load();
            BeatSaberMarkupLanguage.Util.MainMenuAwaiter.MainMenuInitializing += MainMenuInit;
            AddEvents();

            Logger.log.Info($"{PluginName} v.{PluginVersion} has started.");
        }

        private void MainMenuInit()
        {
            SettingsUI.CreateMenu();
        }

        private void Unload()
        {
            RemoveEvents();
            Configuration.Save();
            SettingsUI.RemoveMenu();
        }

        private void AddEvents()
        {
            RemoveEvents();
            BS_Utils.Utilities.BSEvents.gameSceneLoaded += OnGameSceneLoaded;
        }

        private void RemoveEvents()
        {
            BS_Utils.Utilities.BSEvents.gameSceneLoaded -= OnGameSceneLoaded;
        }
    }
}
