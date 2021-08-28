using System;
using UnityEngine;

using Studious.Helpers;

namespace Studious.helpers
{
    public static class RuntimeManager
    {
        private static bool mIsInitialized = false;

        #region Public API
        /// <summary>
        /// </summary>
        public static event Action Initialized;

        /// <summary>
        /// </summary>
        public static void Init()
        {
            if (IsInitialized())
            {
                return;
            }

            if (Application.isPlaying)
            {
                var go = new GameObject("CronusMangement");
                Configure(go);

                // Done init.
                mIsInitialized = true;

                // Raise the event.
                if (Initialized != null)
                    Initialized();
            }
        }

        /// <summary>
        /// Determines whether the runtime manager has been initialized or not.
        /// </summary>
        /// <value><c>true</c> if is initialized; otherwise, <c>false</c>.</value>
        public static bool IsInitialized()
        {
            return mIsInitialized;
        }
        #endregion

        #region Internal Stuff

        /// <summary>
        /// 
        /// </summary>
        private static void Configure(GameObject go)
        {
            // This game object must prevail.
            GameObject.DontDestroyOnLoad(go);
            go.AddComponent<RuntimeHelper>();
        }

        /// <summary>
        /// 
        /// </summary>
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        static void OnBeforeSceneLoadRuntimeMethod()
        {
            Init();
        }

        #endregion
    }
}