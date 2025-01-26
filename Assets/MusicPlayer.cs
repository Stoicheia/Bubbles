using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    public static MusicPlayer Instance;
    
    [SerializeField] private AudioClip _clip;
    [SerializeField] private AudioSource _source;
    
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
        _source.clip = _clip;
        _source.Play();
    }
}
