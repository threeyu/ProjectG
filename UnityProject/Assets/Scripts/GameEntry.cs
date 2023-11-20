using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace ppy {
    public class GameEntry : MonoBehaviour {
        private void OnGUI() {
            // if (GUILayout.Button("Test", GUILayout.Width(200f), GUILayout.Height(150f))) {
            // }
        }

        private void Start() {
            Debug.Log("[GameEntry.Start] hello world");
            AssetManager.I.Init();
            
            GameData.I.Init().Forget();
        }
    }
}