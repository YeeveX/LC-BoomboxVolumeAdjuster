using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

namespace LethalCompanyTemplate.Patches
{
    [HarmonyPatch(typeof(BoomboxItem))]
    internal class BoomboxVolumePatch
    {
        [HarmonyPatch("StartMusic")]
        [HarmonyPrefix]
        static void StartMusic(bool startMusic, ref AudioSource ___boomboxAudio)
        {
            if (startMusic)
            {
                BoomboxVolumeAdjusterPlugin.LogInfo($"Volume: {Config.BoomboxVolume * 100}");

                ___boomboxAudio.volume = Config.BoomboxVolume;
            }
        }
    }
}
