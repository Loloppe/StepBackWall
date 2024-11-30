using BeatSaberMarkupLanguage.GameplaySetup;
using BeatSaberMarkupLanguage.Settings;

namespace StepBackWall.Settings.UI
{
    internal class SettingsUI
    {
        public static bool created = false;

        public static void CreateMenu()
        {
            if (!created)
            {
                BSMLSettings.Instance.AddSettingsMenu("StepBack Wall", "StepBackWall.Settings.UI.Views.mainsettings.bsml", MainSettings.instance);
                GameplaySetup.Instance.AddTab("StepBack Wall", "StepBackWall.Settings.UI.Views.mainmodifiers.bsml", MainModifiers.instance);
                created = true;
            }
        }

        public static void RemoveMenu()
        {
            if (created)
            {
                BSMLSettings.Instance.RemoveSettingsMenu(MainSettings.instance);
                GameplaySetup.Instance.RemoveTab("StepBack Wall");
                created = false;
            }
        }
    }
}
