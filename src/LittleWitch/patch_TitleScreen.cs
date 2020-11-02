#pragma warning disable CS0626
using System;
using System.Collections;
using MonoMod;
using GameEngine;

namespace LittleWitch {

    class patch_TitleScreen : TitleScreen {

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
            var story = ((patch_Game) Engine).Story;
            if (story != null) {
                var storyName = AddComponent(new TextRenderer(Game.MedFont, story.Name + "  v" + story.Version, TextAlign.Right, 0x59419cff));
                storyName.Offset = new Vector(239f, 5f);
                storyName.SetShadow(ColorF.Black);
                storyName.SetTags(GameTags.UI);
            }

            yield return orig_Startup();
        }

        private extern void orig_CreateMenuItems ();
        private void CreateMenuItems () {
            orig_CreateMenuItems();
            var story = ((patch_Game) Engine).Story;

            var storyId = (story == null) ? null : story.ID;
            if (storyId != patch_GameSettings.StoryId) {
                var item = (MenuItemLabel) menuItems.Remove(0);
                item.Label.Text = "(Needs Restart)";
            }
        }
    }
}