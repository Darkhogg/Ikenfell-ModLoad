#pragma warning disable CS0626
using System;
using System.Collections;
using MonoMod;
using GameEngine;

namespace LittleWitch {

    class patch_TitleScreen : TitleScreen {

        [MonoModIgnore]
        private TextRenderer versionText;

        [MonoModIgnore]
        private patch_MenuController menuItems;

        public extern void orig_ctor();
        [MonoModConstructor]
        public void ctor() {
            DoSplashScreens = false;
            DoIntroAnimation = false;

            orig_ctor();
        }

        private extern IEnumerator orig_Startup();

        private IEnumerator Startup() {
            var mod = ((patch_Game) Engine).Mod;
            if (mod != null) {
                var modName = AddComponent(new TextRenderer(Game.MedFont, mod.Name + "  v" + mod.Version, TextAlign.Right, 0x59419cff));
                modName.Offset = new Vector(239f, 5f);
                modName.SetShadow(ColorF.Black);
                modName.SetTags(GameTags.UI);
            }

            yield return orig_Startup();
        }

        private extern void orig_CreateMenuItems ();
        private void CreateMenuItems () {
            orig_CreateMenuItems();
            var mod = ((patch_Game) Engine).Mod;

            var modId = (mod == null) ? null : mod.ID;
            if (modId != patch_GameSettings.ModId) {
                var item = (MenuItemLabel) menuItems.Remove(0);
                item.Label.Text = "(Needs Restart)";
            }
        }
    }
}