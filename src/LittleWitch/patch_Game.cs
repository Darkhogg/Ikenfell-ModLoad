#pragma warning disable CS0626
using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using MonoMod;
using GameEngine;

using System.Reflection;

namespace LittleWitch {

    class patch_Game : Game {

        public StoryInfo Story {
            get;
            private set;
        }

        [MonoModIgnore]
        private Dictionary<Cell3D, JsonObject> rooms;

        protected extern IEnumerator orig_LoadAssets();

        protected override IEnumerator LoadAssets() {
            yield return orig_LoadAssets();

            if (patch_GameSettings.StoryId != null) {
                Story = StoryUtils.GetStoryInfo(patch_GameSettings.StoryId);
                if (Story != null) {
                    LoadStory();
                }
            }
        }

        private void LoadStory () {
            Time.Push("===== LOADING MODDED STORY STUFF =====");
            Console.WriteLine("--- STORY: [" + Story.ID + "] " + Story.Name + " (v" + Story.Version + ")");
            
            var storyPath = StoryUtils.FindStoryPath(Story);
            var storyAssets = Directory.GetFiles(storyPath);
            foreach (var asset in storyAssets) {
                if (asset.EndsWith(".map.json")) {
                    Console.WriteLine("load mod story map: " + asset);
                    LoadMapFrom(asset);
                }

                if (asset.EndsWith(".atlas.img")) {
                    var binAsset = Path.ChangeExtension(asset, "bin");

                    Console.WriteLine("load mod story atlas: " + asset + ", " + binAsset);
                    Assets.AddAtlas(new Atlas(binAsset, asset, createTexture: true));
                }
            }

            Time.Pop("===== MODDED STORY STUFF LOADED =====");
        }

        private new void LoadMap () {
            LoadMapFrom(Platform.MakeContentPath("Data/map.json"));
            Console.WriteLine("Room count: " + rooms.Count);
        }

        private void LoadMapFrom (string path) {
            JsonArray jsonArray = Json.LoadArrayFile(path);
            foreach (JsonObject value in jsonArray.GetValues<JsonObject>())
            {
                Cell3D pos = Cell3D.Parse(value.Get<string>("room"));
                rooms[pos] = value;
            }
        }
    }
}