using System;
using UnityEngine;

namespace Audio
{
    public class AudioDirector : MonoBehaviour
    {
        public static AudioDirector Instance;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(this);
            }
            else
            {
                Destroy(gameObject);
            }
        }

        private void OnEnable()
        {
            
        }

        private void OnDisable()
        {
            
        }
    }
}