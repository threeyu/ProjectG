using UnityEngine;

namespace ppy {
    public static class IOUtils {
        public static string StreamingAssetsPath() {
            return Application.streamingAssetsPath;
        }

        public static string StreamingAssetsPath(string relative) {
            return $"{Application.streamingAssetsPath}/{relative}";
        }
    }
}