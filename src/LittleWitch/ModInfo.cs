using System;
using System.Collections.Generic;
using GameEngine;

namespace LittleWitch
{
    public class ModInfo {
        public string ID {
            get;
            private set;
        }

        public string Name {
            get;
            private set;
        }

        public string Version {
            get;
            private set;
        }

        public ModInfo (string id, string name, string version) {
            ID = id;
            Name = name;
            Version = version;
        }
    }
}
