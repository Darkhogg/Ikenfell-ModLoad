using System;
using System.IO;
using System.Collections.Generic;
using GameEngine;

namespace LittleWitch
{
    public static class ModUtils {
        public static List<ModInfo> ListMods () {
            var mods = new List<ModInfo>();

            var modDirs = Directory.GetDirectories(Platform.MakeContentPath("Mods"));
            for (var i = 0; i < modDirs.Length; i++) {
                var mod = TryLoadModInfo(modDirs[i]);
                if (mod != null) {
                    mods.Add(mod);
                }
            }

            return mods;
        }

        public static ModInfo GetModInfo (string id) {
            return TryLoadModInfo(Path.Join(Platform.MakeContentPath("Mods"), id));
        }

        private static ModInfo TryLoadModInfo (string path) {
            try {
                var jsonData = Json.LoadObjectFile(Path.Join(path, "mod.json"));

                return new ModInfo(
                    Path.GetFileName(path), 
                    jsonData.Get<string>("name"), 
                    jsonData.Get<string>("version")
                );
            } catch (Exception exc) {
                return null;
            }
        }

    }
}
