﻿using System.Collections.Generic;

namespace StepBackWall.Settings.Utilities
{
    internal class PluginConfig
    {
        public bool RegenerateConfig = true;

        public bool ReEnableStepBackWall = true;

        public Dictionary<string, object> Logging = new Dictionary<string, object>()
        {
            { "ShowCallSource", false },
        };
    }
}