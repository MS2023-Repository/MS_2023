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

        [SerializeField] private AudioSource bgmAudioSource;
        [SerializeField] private AudioSource seAudioSource;

        public SoundClip[] SEClips;
        public SoundClip[] BGMClips;

        private string currentSceneName;

        private bool changeBgmFlg;
        private float targetVolume;

        private float volumeT;

        public AudioClip GetAudioClip(string clipName)
        {
            AudioClip clipToReturn = null;

            foreach (var clip in SEClips)
            {
                if (clip.name == clipName)
                {
                    clipToReturn = clip.clip;
                    break;
                }
            }

            return clipToReturn;
        }

        public void ChangeBGM()
        {
            if (!changeBgmFlg)
            {
                StopAllCoroutines();
                changeBgmFlg = true;
                StartCoroutine(FadeOut());
            }
        }

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
            bgmAudioSource.loop = true;

            var audioObjs = GameObject.FindObjectsOfType<AudioListener>();
            foreach (var current in audioObjs)
            {
                if (current.name != "Main Camera")
                {
                    Destroy(current);
                }
            }

            currentSceneName = "  ";

            bgmAudioSource.volume = 0.0f;
            changeBgmFlg = false;

            volumeT = 0;
        }

        // Update is called once per frame
        void Update()
        {
            bgmAudioSource.volume = Mathf.Lerp(0, targetVolume, volumeT);

            if (SceneLoader.instance.SceneChanged())
            {
                StartCoroutine(FadeIn());
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
            while (volumeT > 0.0f)
            {
                volumeT -= TimeManager.instance.deltaTime;
                yield return null;
            }

            bgmAudioSource.volume = 0.0f;
        }

        IEnumerator FadeIn()
        {
            currentSceneName = SceneLoader.instance.GetCurrentScene();

            foreach (var clip in BGMClips)
            {
                if (currentSceneName.Contains("Title") || currentSceneName.Contains("StageSelect"))
                {
                    if (clip.name == currentSceneName)
                    {
                        bgmAudioSource.clip = clip.clip;
                        targetVolume = clip.volume;
                        break;
                    }
                }
                else
                {
                    if (clip.name == "Game")
                    {
                        bgmAudioSource.clip = clip.clip;
                        targetVolume = clip.volume;
                        break;
                    }
                }
            }

            bgmAudioSource.Play();

            while (volumeT < 1)
            {
                volumeT += TimeManager.instance.deltaTime / 2;
                yield return null;
            }

            volumeT = 1;

            changeBgmFlg = false;
        }
    }
}