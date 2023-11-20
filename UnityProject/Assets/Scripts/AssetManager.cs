using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace ppy {
    public class AssetManager : MonoSingleton<AssetManager> {
        public void Init() {
            BetterStreamingAssets.Initialize();
        }

        public UniTask<AssetBundle> LoadAsync(string path) {
            var ret = BetterStreamingAssets.LoadAssetBundleAsync(path);
            return ret.ToUniTask();
        }

        public byte[] ReadAllBytes(string path) {
            var ret = BetterStreamingAssets.ReadAllBytes(path);
            return ret;
        }

        public string ReadAllText(string path) {
            var ret = BetterStreamingAssets.ReadAllText(path);
            return ret;
        }
    }
}