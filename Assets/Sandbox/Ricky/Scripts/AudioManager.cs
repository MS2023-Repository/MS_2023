using OutGame.SceneManager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct SoundClip
{
    public string name;
    public AudioClip clip;
    [Range(0.0f, 1.0f)] public float volume;
}

namespace OutGame.Audio
{
    using OutGame.TimeManager;

    public class AudioManager : MonoBehaviour
    {
        public static AudioManager instance { get; private set; }

        private AudioSource bgmAudioSource;
        private AudioSource seAudioSource;

        public SoundClip[] SEClips;
        public SoundClip[] BGMClips;

        private string currentSceneName;

        private bool changeBgmFlg;

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

            currentSceneName = "  ";

            bgmAudioSource.volume = 0.0f;
            changeBgmFlg = false;
        }

        // Update is called once per frame
        void Update()
        {
            if (currentSceneName != SceneLoader.instance.GetCurrentScene())
            {
                if (!changeBgmFlg)
                {
                    currentSceneName = SceneLoader.instance.GetCurrentScene();
                    changeBgmFlg = true;
                    StartCoroutine(FadeOut());
                }
            }
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

        IEnumerator FadeOut()
        {
            while (bgmAudioSource.volume > 0.0f)
            {
                bgmAudioSource.volume -= TimeManager.instance.deltaTime;
                yield return null;
            }

            bgmAudioSource.volume = 0.0f;

            foreach (var clip in BGMClips)
            {
                if (clip.name == currentSceneName)
                {
                    bgmAudioSource.clip = clip.clip;
                }
            }

            StartCoroutine(FadeIn());
        }

        IEnumerator FadeIn()
        {
            while (bgmAudioSource.volume < 1.0f)
            {
                bgmAudioSource.volume += TimeManager.instance.deltaTime;
                yield return null;
            }

            bgmAudioSource.volume = 1.0f;

            changeBgmFlg = false;
        }
    }
}