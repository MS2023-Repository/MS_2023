using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioManager _instance;

    public static AudioManager Instance
    {
        get
        {
            if (_instance == null)
            {
                Debug.LogError("AudioManager is null");
            }
            return _instance;
        }
    }

    public struct SoundClip
    {
        public AudioClip clip;
        public float volume;
    }

    private AudioSource bgmAudioSource;
    private AudioSource seAudioSource;

    public SoundClip[] seClips;
    public SoundClip[] bgmClips;

    private void Awake() 
    {
        _instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        bgmAudioSource = transform.GetChild(0).GetComponent<AudioSource>();
        seAudioSource = transform.GetChild(1).GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlaySE(SoundClip insertedClip)
    {
        seAudioSource.PlayOneShot(insertedClip.clip, insertedClip.volume);
    }
}
