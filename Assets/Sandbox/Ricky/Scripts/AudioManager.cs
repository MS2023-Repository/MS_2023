using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OutGame.Audio
{
    public class AudioManager : MonoBehaviour
    {
        public static AudioManager instance { get; private set; }

        public struct SoundClip
        {
            public string name;
            public AudioClip clip;
            public float volume;
        }

        private AudioSource bgmAudioSource;
        private AudioSource seAudioSource;

        public SoundClip[] seClips;
        public SoundClip[] bgmClips;

        private void Awake()
        {
            if (instance != null && instance != this)
            {
                Destroy(this);
            }
            else
            {
                instance = this;
            }

            DontDestroyOnLoad(this);
        }

        // Start is called before the first frame update
        void Start()
        {
            bgmAudioSource = transform.GetChild(0).GetComponent<AudioSource>();
            seAudioSource = transform.GetChild(1).GetComponent<AudioSource>();

            bgmAudioSource.loop = true;
            bgmAudioSource.clip = bgmClips[0].clip;
            bgmAudioSource.volume = bgmClips[0].volume;

            bgmAudioSource.Play();
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void PlaySE(string clipName)
        {
            foreach (var clip in seClips)
            {
                if (clip.name == clipName)
                {
                    seAudioSource.PlayOneShot(clip.clip, clip.volume);
                    break;
                }
            }

            Debug.LogError("No coressponding SE clip found");
        }
    }
}