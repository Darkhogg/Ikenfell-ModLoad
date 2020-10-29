#pragma warning disable CS0626
using System;
using System.Collections;
using System.Text;
using MonoMod;
using GameEngine;

namespace LittleWitch {

    class patch_Game : Game {

        public ModInfo Mod {
            get;
            private set;
        }

        protected extern IEnumerator orig_LoadAssets();

        protected override IEnumerator LoadAssets() {
            yield return orig_LoadAssets();

            if (patch_GameSettings.ModId != null) {
                Mod = ModUtils.GetModInfo(patch_GameSettings.ModId);
            }
        }
    }
}