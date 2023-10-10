using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Managers : MonoBehaviour
{
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
        DontDestroyOnLoad(this);
    }

    // Start is called before the first frame update
    void SoundInit()
    {
        bgmAudioSource = transform.GetChild(0).GetComponent<AudioSource>();
        seAudioSource = transform.GetChild(1).GetComponent<AudioSource>();

        bgmAudioSource.loop = true;
        bgmAudioSource.clip = bgmClips[0].clip;
        bgmAudioSource.volume = bgmClips[0].volume;

        bgmAudioSource.Play();
    }

    // Update is called once per frame
    void SoundUpdate()
    {
        
    }

    public void PlaySE(SoundClip insertedClip)
    {
        seAudioSource.PlayOneShot(insertedClip.clip, insertedClip.volume);
    }
}
