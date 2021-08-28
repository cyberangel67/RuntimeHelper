using System;
using System.Collections.Concurrent;
using UnityEngine;


namespace Studious.Helpers
{
    public class RuntimeHelper : Singleton<RuntimeHelper>
    {

        private static ConcurrentQueue<Action> MainThreadQueue = new ConcurrentQueue<Action>();

        /// <summary>
        /// 
        /// </summary>
        private static bool IsInitialized()
        {
            return Instance != null;
        }

        /// <summary>
        /// 
        /// </summary>
        public static void RunOnMainThread(Action action)
        {
            if (action == null)
                throw new ArgumentNullException("action");

            if (!IsInitialized())
            {
                Debug.LogError("Using RunOnMainThread without initializing Helper.");
                return;
            }

            lock (MainThreadQueue)
            {
                MainThreadQueue.Enqueue(action);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void Update()
        {
            if (!MainThreadQueue.IsEmpty)
            {
                while (MainThreadQueue.TryDequeue(out var action))
                {
                    action?.Invoke();
                }
            }
        }
    }
}