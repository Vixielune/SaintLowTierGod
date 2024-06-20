using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaintLowTierGod
{
    public static class Sounds
    {

        public static SoundID SaintAYS_SFX { get; private set;}
        public static SoundID SaintNow_SFX { get; private set; }
        public static SoundID SaintThunder_SFX { get; private set; }
        public static SoundID SaintTRumble_SFX { get; private set; }

        internal static void InitializeSounds()
        {
            try
            {
                // Initialize sounds. 'tRumble' to be used... soon.
                SaintAYS_SFX = new SoundID("Saint_Ascend_Yourself", true);
                SaintNow_SFX = new SoundID("Saint_NOW", true);
                SaintThunder_SFX = new SoundID("Saint_aThunder", true);
                // SaintTRumble_SFX = new SoundID("Saint_tRumble", true);
                UnityEngine.Debug.Log("All sounds initialized correctly!");
            }
            catch (Exception ex)
            {
                UnityEngine.Debug.LogError("Error during sound initialization! Exeption: " + ex);
            }
        }
    }
}
