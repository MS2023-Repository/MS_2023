using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OutGame.Audio
{
    [System.Serializable]
    public struct SoundClip
    {
        public string name;
        public AudioClip clip;
        public float volume;
    }

    public class AudioManager : MonoBehaviour
    {
        public static AudioManager instance { get; private set; }

        private AudioSource bgmAudioSource;
        private AudioSource seAudioSource;

        [SerializeField] private SoundClip[] SEClips;
        [SerializeField] private SoundClip[] BGMClips;

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

            var audioObjs = GameObject.FindObjectsOfType<AudioListener>();
            foreach (var current in audioObjs)
            {
                if (current.name != "MainCamera")
                {
                    Destroy(current);
                }
            }
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void PlaySE(string clipName)
        {
            foreach (var clip in SEClips)
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