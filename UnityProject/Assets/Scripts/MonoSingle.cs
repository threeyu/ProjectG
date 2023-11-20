using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ppy {
    public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoSingleton<T> {
        private static T mInstance;

        public static T I {
            get {
                if (mInstance == null) {
                    mInstance = Instantiate();
                }

                return mInstance;
            }
        }

        protected static T Instantiate() {
            if (mInstance == null) {
                mInstance = (T)FindObjectOfType(typeof(T));
                if (FindObjectsOfType(typeof(T)).Length > 1) {
                    return mInstance;
                }

                if (mInstance == null) {
                    GameObject singleton = new GameObject("[Singleton]" + typeof(T).Name);
                    if (singleton != null) {
                        mInstance = singleton.AddComponent<T>();
                        mInstance.InitSingleton();
                    }
                }
            }

            return mInstance;
        }

        protected virtual void InitSingleton() {
            if (Application.isPlaying)
                DontDestroyOnLoad(this.gameObject);
        }

        protected virtual void Awake() {
            if (mInstance == null) {
                mInstance = this as T;
            }
        }

        public virtual void OnApplicationQuit() {
            mInstance = null;
        }

        public static void Dispose() {
            if (mInstance != null && mInstance.gameObject != null)
                DestroyImmediate(mInstance.gameObject);
        }
    }
}