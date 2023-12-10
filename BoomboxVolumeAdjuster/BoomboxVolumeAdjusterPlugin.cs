using BepInEx;
using BepInEx.Logging;
using HarmonyLib;
using LethalCompanyTemplate.Patches;

namespace LethalCompanyTemplate
{
    [BepInPlugin(GUID, NAME, VERSION)]
    public class BoomboxVolumeAdjusterPlugin : BaseUnityPlugin
    {
        public const string GUID = "com.github.Yeevex.LC-BoomboxVolumeAdjuster";
        public const string NAME = "BoomboxVolumeAdjuster";
        public const string VERSION = "1.0.0";

        private readonly Harmony _harmony = new Harmony(GUID);
        public static BoomboxVolumeAdjusterPlugin Instance { get; private set; }
        public ManualLogSource ModLogger => Logger;

        private void Awake()
        {
            if(Instance == null)
            {
                Instance = this;
            }

            // Plugin startup logic
            Logger.LogInfo($"Plugin {GUID} is loaded!");

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