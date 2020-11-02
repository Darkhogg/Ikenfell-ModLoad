#pragma warning disable CS0626
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using MonoMod;
using GameEngine;

namespace LittleWitch {

    class patch_OptionsScreen : OptionsScreen {

        [MonoModIgnore]
        private patch_MenuController menu;

        [MonoModIgnore]
		private float menuHeight;

        public extern void orig_ctor ();
        public extern void orig_ctor (bool title);

        [MonoModConstructor]
        public void ctor (bool title) {
            orig_ctor(title);
            postCtor();
        }

        [MonoModConstructor]
        public void ctor () {
            orig_ctor();
            postCtor();
        }

        private void postCtor () {
            OnLoad += delegate {
                var returnItem = menu.Get(menu.Count - 1);
                
                var stories = StoryUtils.ListStories();

                var storyItem = menu.Insert(
                    menu.Count - 1,
                    new MenuItemChoiceStories("Story", new Vector(120f, returnItem.Transform.Y), stories),
                    "Which Story to play. Requires restart."
                );
                storyItem.SetChoice(patch_GameSettings.StoryId);
                storyItem.OnChange += delegate {
                    patch_GameSettings.StoryId = (storyItem.Choice == null) ? null : storyItem.Choice.ID;
                };

                returnItem.Transform.Y += 16f;
                menuHeight += 16f;
            };
        }
    }
}