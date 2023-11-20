using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ppy.ui {
    public class UIActiveGroup : MonoBehaviour {
        [Serializable]
        public class Group {
            public string name;
            public GameObject[] objs;
        }

        public Group[] groups;
        public string currentActive;

        private Dictionary<string, Group> mName2group = new Dictionary<string, Group>();

        private void Awake() {
            for (var i = 0; i < groups.Length; i++) {
                var g = groups[i];
                mName2group[g.name] = g;
            }
        }

        public void SetActive(string name) {
            currentActive = name;
            if (mName2group.TryGetValue(name, out var group)) {
                for (var i = 0; i < groups.Length; i++) {
                    var g = groups[i];
                    if (g.name == name) {
                        continue;
                    }

                    for (var j = 0; j < g.objs.Length; j++) {
                        g.objs[j].SetActive(false);
                    }
                }

                for (var i = 0; i < group.objs.Length; i++) {
                    group.objs[i].SetActive(true);
                }
            }
            else {
                Log.Error("[UIActiveGroup] No such group:", name);
            }
        }
    }
}