#pragma warning disable CS0626
using System;
using System.Collections.Generic;
using System.Text;
using MonoMod;

namespace GameEngine {

    class patch_Assets {
        [MonoModIgnore]
        private static List<Sprite> sprites;

        [MonoModIgnore]
        private static Dictionary<string, Sprite> spriteLookup;


        [MonoModIgnore]
        private static List<Tileset> tilesets;

        [MonoModIgnore]
        private static Dictionary<string, Tileset> tilesetLookup;

        [MonoModIgnore]
        private static Dictionary<Sprite, Tileset> spriteToTileset;


        public static Sprite AddSprite (Sprite sprite) {
            if (spriteLookup.ContainsKey(sprite.Name)) {
                var sprIndex = sprites.FindIndex(spr => spr.Name == sprite.Name);
                sprites[sprIndex] = sprite;
            } else {
                sprites.Add(sprite);
            }
            spriteLookup[sprite.Name] = sprite;
            return sprite;
        }

        public static Tileset AddTileset(Tileset tileset) {
            if (tilesetLookup.ContainsKey(tileset.Name)) {
                var tsIndex = sprites.FindIndex(ts => ts.Name == tileset.Name);
                tilesets[tsIndex] = tileset;
            } else {
                tilesets.Add(tileset);
            }
            tilesetLookup[tileset.Name] = tileset;
            foreach (Sprite sprite in tileset.Sprites) {
                spriteToTileset[sprite] = tileset;
            }
            return tileset;
        }
    }
}