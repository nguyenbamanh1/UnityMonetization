using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
namespace ManhhdcPackage
{
    internal class GameDispatcher : MonoBehaviour
    {
        [RuntimeInitializeOnLoadMethod]
        static void Init()
        {
            GameDispatcher.Instance.ExecuteOnUpdate(() => Debug.Log("Init"));
        }


        private static object lockobj = new object();
        private static GameDispatcher instance;
        public static GameDispatcher Instance
        {
            get
            {
                lock (lockobj) {
                    if (instance == null)
                    {
                        instance = FindObjectOfType<GameDispatcher>();
                        if (instance == null)
                        {
                            GameObject g = new GameObject();
                            DontDestroyOnLoad(g);
                            instance = g.AddComponent<GameDispatcher>();
                        }
                    }
                    return instance;
                }
            }
        }

        private Queue<Action> queue = new Queue<Action>();

        private void Update()
        {
            lock (queue)
            {
                while (queue.Count > 0)
                {
                    Action action = queue.Dequeue();
                    if (action != null)
                        action();
                }
            }
        }

        public void ExecuteOnUpdate(Action action)
        {
            lock (queue)
            {
                queue.Enqueue(action);
            }
        }
    }
}
