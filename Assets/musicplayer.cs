using System;
using UnityEngine;

public class musicplayer : MonoBehaviour
{
    public static musicplayer Instance;
    public AudioSource audio;
    public AudioClip clip;

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
        audio.clip = clip;
        audio.Play();
    }
}
