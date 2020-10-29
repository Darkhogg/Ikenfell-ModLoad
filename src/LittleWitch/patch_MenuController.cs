#pragma warning disable CS0626
using System;
using System.Collections;
using System.Collections.Generic;
using MonoMod;
using GameEngine;

namespace LittleWitch {

    class patch_MenuController : MenuController {

        [MonoModIgnore]
        private List<MenuItem> items;
        public int Count {
            get { return items.Count; }
            private set {}
        }

        public patch_MenuController(bool canCancel) : base(canCancel) {}


        public MenuItem Get (int index) {
            return items[index];
        }

        public T Insert<T> (int index, T item) where T : MenuItem {
            items.Insert(index, item);
            Engine.AddEntity(item);
            return item;
        }
        public T Insert<T> (int index, T item, string helpText) where T : MenuItem {
            item.HelpText = helpText;
            return Insert(index, item);
        }

        public MenuItem Remove (int index) {
            var item = items[index];
            items.RemoveAt(index);
            return item;
        }
    }
}