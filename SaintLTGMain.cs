using System;
using System.Reflection;
using BepInEx;
using IL;
using Mono.Cecil;
using Mono.Cecil.Cil;
using MonoMod.Cil;
using On;
using UnityEngine;

namespace SaintLowTierGod
{
    [BepInPlugin(PLUGIN_GUID, PLUGIN_NAME, PLUGIN_VERSION)]
    public class SaintLTGMain : BaseUnityPlugin
    {
        public const string PLUGIN_GUID = "vixielune.saint_ltg"; //Unique ID of mod. Same as in modinfo.json!
        public const string PLUGIN_NAME = "You should ascend yourself, NOW!";
        public const string PLUGIN_VERSION = "1.0.2";
        private bool _initialized;

        //Refrencing the logger to make shite easier (less words to type = happy me)
        private void LogInfo(object data)
        {
            base.Logger.LogInfo(data);
        }

        private void OnEnable()
        {
            /* Method is called when mod is loaded into game! */
            this.LogInfo("Yippee! Saint LTG is now active!! Current version: " + PLUGIN_VERSION);
            this.LogInfo("Credit to BesoneWhite for the IL Hooking code, I can simply not figure out. Go check out their mod, 'Silly Ascension Sounds'!");
            // Begin subscribing to hooks when game enables the mod!
            On.RainWorld.OnModsInit += new On.RainWorld.hook_OnModsInit(RainWorld_OnModsInitialize);
        }

        private void RainWorld_OnModsInitialize(On.RainWorld.orig_OnModsInit orig, RainWorld self)
        {
           
            try
            {
                if (!this._initialized)
                {
                    this._initialized = true;
                    Sounds.InitializeSounds();
                    On.Player.ActivateAscension += PlayActivateSFXHook;
                    // f*cking help me god. dont know what argument it wants. UPDATE: Still hate this. UPDATE 2: Apparently you need to use
                    // namespace 'IL' and not 'On' fhuhddhjbnjhd lol ok
                    IL.Player.ClassMechanicsSaint += new ILContext.Manipulator(this.Player_ClassMechanicsSaint);
                    IL.Player.ClassMechanicsSaint += new ILContext.Manipulator(this.Player_ClassMechanicsSaint1);
                    IL.Player.ClassMechanicsSaint += new ILContext.Manipulator(this.Player_ClassMechanicsSaint2);
                    this.LogInfo("Initialization complete!");
                }
            }
            catch (Exception data)
            {
                this.LogInfo(data);
                throw;
            }
            finally { orig.Invoke(self); }
        }
        void PlayActivateSFXHook(On.Player.orig_ActivateAscension orig, Player self) {

            self.room.PlaySound(Sounds.SaintAYS_SFX, self.mainBodyChunk, false, 1f, 1.028f + UnityEngine.Random.value * 0.125f);
            orig(self);
        }
        private void Player_ClassMechanicsSaint2(ILContext il)
        {
            ILCursor cursor = new ILCursor(il);
            try
            {
                ILCursor ilcursor = cursor;
                MoveType moveType = MoveType.After;
                Func<Instruction, bool>[] array = new Func<Instruction, bool>[1];
                array[0] = (Instruction i) => ILPatternMatchingExt.MatchLdcR4(i, 0.5f);
                if (!ilcursor.TryGotoNext(moveType, array))
                {
                    base.Logger.LogError("FAILED TRYING TO MATCH Pitch1, THIS IS SO SILLY YOU MUST STOP HERE!!");
                }
                cursor.MoveAfterLabels();
                cursor.EmitDelegate<Func<float, float>>((float _) => 0.60f);
                Debug.Log("PITCH SET TO 0.60F");
            }
            catch (Exception exception)
            {
                Debug.LogError("SOUNDS CAN'T MATCH SaintClassMechanics1 Pitch1, PRAY FOR HELP!!!!");
                Debug.LogException(exception);
                Debug.LogError(il);
                throw;
            }
        }
        private void Player_ClassMechanicsSaint1(ILContext il)
        {
            ILCursor cursor = new ILCursor(il);
            try
            {
                ILCursor ilcursor = cursor;
                MoveType moveType = MoveType.After;
                Func<Instruction, bool>[] array = new Func<Instruction, bool>[1];
                array[0] = ((Instruction i) => ILPatternMatchingExt.MatchLdsfld(i, "SoundID", "Firecracker_Bang"));
                if (!ilcursor.TryGotoNext(moveType, array))
                {
                    base.Logger.LogError("FAILED TRYING TO MATCH Firecracker_Bang SOUND, TAKE A REST AND PRAY FOR THE DEBUG TO WORK!");
                }
                cursor.MoveAfterLabels();
                cursor.EmitDelegate<Func<SoundID, SoundID>>((SoundID _) => Sounds.SaintNow_SFX);
                Debug.Log("SOUND CHANGED BY THE DARK FORCE OF THE IL HOOK MAGIC!!");
            }
            catch (Exception exception)
            {
                Debug.LogError("SOUNDS CAN'T MATCH SaintClassMechanics1 SOUNDS, PRAY FOR HELP!!!!");
                Debug.LogException(exception);
                Debug.LogError(il);
                throw;
            }
        }
        private void Player_ClassMechanicsSaint(ILContext il)
        {
            ILCursor cursor = new ILCursor(il);
            try
            {
                ILCursor ilcursor = cursor;
                MoveType moveType = MoveType.After;
                Func<Instruction, bool>[] array = new Func<Instruction, bool>[1];
                array[0] = ((Instruction i) => ILPatternMatchingExt.MatchLdsfld(i, "SoundID", "SS_AI_Give_The_Mark_Boom"));
                if (!ilcursor.TryGotoNext(moveType, array))
                {
                    base.Logger.LogError("FAILED TRYING TO MATCH SaintClassMechanics SOUNDS, TAKE A REST AND PRAY FPR THE DEBUG TO WORK!");
                }
                cursor.MoveAfterLabels();
                cursor.EmitDelegate<Func<SoundID, SoundID>>((SoundID _) => Sounds.SaintThunder_SFX);
                Debug.Log("Sound applied!");
            }
            catch (Exception exception)
            {
                Debug.LogError("SOUNDS CAN'T MATCH SaintClassMechanics SOUNDS, PRAY FOR HELP!!!!");
                Debug.LogException(exception);
                Debug.LogError(il);
                throw;
            }
        }
    }
}