using BepInEx.Configuration;
using BepInEx;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace LethalCompanyTemplate.Patches
{
    internal class Config
    {
        private const string CONFIG_FILE_NAME = "boomboxVolumeAdjuster.cfg";

        private static ConfigFile _config;

        private static ConfigEntry<bool> _dontUpdateAfterStart; //If true only checks the volume at startup

        private static ConfigEntry<float> _boomboxVolume;

        public static bool DontUpdateAfterStart => _dontUpdateAfterStart != null && _dontUpdateAfterStart.Value;

        /// <summary>
        /// Range [0-1]
        /// </summary>
        public static float BoomboxVolume {
            get
            {
                if (!DontUpdateAfterStart)
                {
                    BoomboxVolumeAdjusterPlugin.LogInfo($"Reloading volume...");
                    _config.Reload();
                    ReadVolume();
                }
                return _boomboxVolume != null ? Math.Clamp((float)_boomboxVolume.Value, 0f, 100f) / 100f : 1f;
            }
        }

        private static void ReadDontUpdateAfterStart()
        {
            _dontUpdateAfterStart = _config.Bind("Config", "Dont Update After Start", defaultValue: false, "If you are happy with the current volume you should set this to true for performance reasons\n (Avoids boombox volume update after launch)");
        }

        private static void ReadVolume()
        {
            _boomboxVolume = _config.Bind("Config", "Boombox Volume", defaultValue: 100f, "Set a value in range [0 - 100]");
        }

        public static void Init()
        {
            BoomboxVolumeAdjusterPlugin.LogInfo("Initializing config...");
            _config = new ConfigFile(Path.Combine(Paths.ConfigPath, CONFIG_FILE_NAME), saveOnInit: true);
            ReadDontUpdateAfterStart();
            ReadVolume();
            BoomboxVolumeAdjusterPlugin.LogInfo("Config initialized!");
        }

        private static void PrintConfig()
        {
            BoomboxVolumeAdjusterPlugin.LogInfo($"Dont update after start: {_dontUpdateAfterStart}");
            BoomboxVolumeAdjusterPlugin.LogInfo($"Boombox volume: {_boomboxVolume}");
        }
    }
}
