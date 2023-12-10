using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using BoomboxVolumeAdjuster.Patches;

namespace BoomboxVolumeAdjuster
{
    [BepInPlugin(PluginInfo.PLUGIN_GUID, PluginInfo.PLUGIN_NAME, PluginInfo.PLUGIN_VERSION)]
    public class BoomboxVolumeAdjusterPlugin : BaseUnityPlugin
    {
        private readonly Harmony _harmony = new Harmony(PluginInfo.PLUGIN_GUID);
        public static BoomboxVolumeAdjusterPlugin Instance { get; private set; }
        public ManualLogSource ModLogger => Logger;

        private void Awake()
        {
            if(Instance == null)
            {
                Instance = this;
            }

            // Plugin startup logic
            Logger.LogInfo($"Plugin {PluginInfo.PLUGIN_GUID} is loaded!");

            Patches.Config.Init();

            _harmony.PatchAll(typeof(BoomboxVolumeAdjusterPlugin));
            _harmony.PatchAll(typeof(BoomboxVolumePatch));
        }

        internal static void LogInfo(string message)
        {
            Instance.ModLogger.LogInfo(message);
        }
    }
}