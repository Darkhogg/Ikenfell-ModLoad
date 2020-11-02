#pragma warning disable CS0626
using System;
using System.Collections.Generic;
using System.Text;
using MonoMod;
using GameEngine;

namespace LittleWitch {

    class patch_GameSettings {
        private static string StorySettingName = "story";

        public static string StoryId;

        public static extern StringBuilder orig_SaveToBuilder();

        public static StringBuilder SaveToBuilder () {
            StringBuilder builder = orig_SaveToBuilder();
            if (StoryId != null) {
                builder.Append(StorySettingName);
                builder.Append("=");
                builder.AppendLine(StoryId);
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
            if (dict.ContainsKey(StorySettingName)) {
		        StoryId = (dict[StorySettingName] == "" ? null : dict[StorySettingName].Trim());
            }
        }

    }
}