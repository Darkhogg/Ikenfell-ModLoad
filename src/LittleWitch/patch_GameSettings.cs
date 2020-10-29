#pragma warning disable CS0626
using System;
using System.Collections.Generic;
using System.Text;
using MonoMod;
using GameEngine;

namespace LittleWitch {

    class patch_GameSettings {
        public static string ModId;

        public static extern StringBuilder orig_SaveToBuilder();

        public static StringBuilder SaveToBuilder () {
            StringBuilder builder = orig_SaveToBuilder();
            if (ModId != null) {
                builder.Append("mod_id=");
                builder.AppendLine(ModId);
            }
            return builder;
        }

        public static extern void orig_LoadFromString(string data);

        public static void LoadFromString(string data) {
            orig_LoadFromString(data);

            // copied from original
            string[] lines = data.Split("\n");
            Dictionary<string, string> dict = new Dictionary<string, string>();
            foreach (string text in lines) {
                if (text.Length > 0) {
                    int eqIdx = text.IndexOf('=');
                    dict[text.Substring(0, eqIdx)] = text.Substring(eqIdx + 1);
                }
            }

            // new settings
            if (dict.ContainsKey("mod_id")) {
		        ModId = (dict["mod_id"] == "" ? null : dict["mod_id"].Trim());
            }
        }

    }
}