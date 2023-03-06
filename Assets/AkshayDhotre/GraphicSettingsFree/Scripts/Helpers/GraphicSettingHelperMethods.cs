using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AkshayDhotre.GraphicSettingsMenu
{
    /// <summary>
    /// Contains methods to help apply the graphic settings
    /// </summary>
    public static class GraphicSettingHelperMethods
    {
        /// <summary>
        /// Change the screenmode,ExclusiveFullScreen(screenmode = 0), FullScreenWindow(1), MaximizedWindow(2), Windowed(3)
        /// </summary>
        /// <param name="screenMode"></param>
        public static void ChangeScreenMode(int screenMode)
        {
            Screen.fullScreenMode = (FullScreenMode)screenMode;
            
        }

        /// <summary>
        /// Changes the screen resolution
        /// </summary>
        /// <param name="x">Screen Width</param>
        /// <param name="y">Sreen Height</param>
        public static void ChangeResolution(int x, int y)
        {
            Screen.SetResolution(x, y, Screen.fullScreenMode);
        }

        /// <summary>
        /// Changes the quality settings as defined in Edit/ProjectSettings/Quality
        /// </summary>
        /// <param name="qualityLevelIndex"></param>
        public static void ChangeQualitySettings(int qualityLevelIndex)
        {
            QualitySettings.SetQualityLevel(qualityLevelIndex);
        }
    }
}

