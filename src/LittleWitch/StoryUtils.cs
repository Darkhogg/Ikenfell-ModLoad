using System;
using System.IO;
using System.Collections.Generic;
using GameEngine;


namespace LittleWitch
{
    public static class StoryUtils {
        static string rootPath = Platform.MakeContentPath("Stories");

        public static List<StoryInfo> ListStories () {
            var stories = new List<StoryInfo>();

            try {
                var dirs = Directory.GetDirectories(rootPath);

                foreach (var dir in dirs) {
                    var story = GetStoryInfo(Path.GetFileName(dir));
                    if (story != null) {
                        stories.Add(story);
                    }
                }
            } catch (DirectoryNotFoundException) {
                // not important, there are no mods, that's it
            }

            return stories;
        }

        public static StoryInfo GetStoryInfo (string id) {
            try {
                var jsonData = Json.LoadObjectFile(FindAssetPath(id, "story.json"));

                return new StoryInfo(
                    Path.GetFileName(id),
                    jsonData.Get<string>("name"), 
                    jsonData.Get<string>("version")
                );
            } catch (Exception) {
                return null;
            }
        }

        public static string FindAssetPath (StoryInfo story, string asset) {
            return FindAssetPath(story.ID, asset);
        }

        public static string FindAssetPath (string id, string asset) {
            var path = Path.Join(FindStoryPath(id), asset);
            Console.WriteLine(path);
            Console.WriteLine(File.Exists(path));
            return File.Exists(path) ? path : null;
        }
        public static string FindStoryPath (StoryInfo story) {
            return FindStoryPath(story.ID);
        }
        public static string FindStoryPath (string id) {
            return Path.Join(rootPath, id);
        }
    }
}
