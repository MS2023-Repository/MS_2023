using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OutGame.SceneManager
{
    using UnityEngine.SceneManagement;
    using UnityEngine.UI;
    using OutGame.TimeManager;
    using OutGame.Audio;

    public class SceneLoader : MonoBehaviour
    {
        public static SceneLoader instance { get; private set; }

        [SerializeField] private GameObject loadingScreenObj;

        private GameObject loadingObj;
        private Image fadePanel;

        private string sceneName;

        private bool isLoading;

        public string GetCurrentScene()
        {
            return SceneManager.GetActiveScene().name;
        }

        public bool SceneChanged()
        {
            return sceneName != GetCurrentScene();
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
            sceneName = string.Empty;
            ResetLoadingScreen();
        }

        // Update is called once per frame
        void Update()
        {
            if (SceneChanged())
            {
                ResetLoadingScreen();
            }

            if (loadingObj.transform.parent.parent == null)
            {
                if (GameObject.Find("Canvas") != null)
                {
                    loadingObj.transform.parent.SetParent(GameObject.Find("Canvas").transform);
                    loadingObj.transform.parent.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
                }
            }
        }

        private void LateUpdate()
        {
            sceneName = GetCurrentScene();
        }

        public void LoadScene(string sceneName)
        {
            if (!isLoading)
            {
                AudioManager.instance.ChangeBGM();
                StartCoroutine(FadeScreen(true, sceneName));
                isLoading = true;
            }
        }

        public void ReloadScene()
        {
            LoadScene(SceneManager.GetActiveScene().name);
        }

        IEnumerator LoadSceneAsynchronously(string name)
        {
            AsyncOperation operation = SceneManager.LoadSceneAsync(name);
            while (!operation.isDone)
            {
                Debug.Log(operation.progress);
                loadingObj.transform.GetChild(1).GetComponent<Slider>().value = operation.progress;
                yield return new WaitForSeconds(2f);
            }

            loadingObj.SetActive(false);
            StartCoroutine(FadeScreen(false, string.Empty));
        }

        private void ResetLoadingScreen()
        {
            if (GameObject.Find("LoadingScreen") == null)
            {
                loadingObj = Instantiate(loadingScreenObj);
                loadingObj.name = "LoadingScreen";
            }
            else
            {
                loadingObj = GameObject.Find("LoadingScreen");
            }

            fadePanel = loadingObj.transform.GetChild(0).GetComponent<Image>();
            var tempColor = fadePanel.color;
            tempColor.a = 1;
            fadePanel.color = tempColor;
            loadingObj = loadingObj.transform.GetChild(1).gameObject;
            loadingObj.SetActive(false);

            if (loadingObj.transform.parent.parent == null)
            {
                if (GameObject.Find("Canvas") != null)
                {
                    loadingObj.transform.parent.SetParent(GameObject.Find("Canvas").transform);
                    loadingObj.transform.parent.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
                }
            }

            StartCoroutine(FadeScreen(false, string.Empty));

            GameObject.FindObjectOfType<AudioManager>().ChangeBGM();

            isLoading = true;
        }

        IEnumerator FadeScreen(bool fadeOut, string sceneName)
        {
            if (fadeOut)
            {
                GameObject.FindObjectOfType<FadeScript>().PlayFadeOut();

                while (GameObject.FindObjectOfType<FadeScript>().fadeOutState)
                {
                    yield return null;
                }
            }
            else
            {
                GameObject.FindObjectOfType<FadeScript>().PlayFadeIn();

                while (GameObject.FindObjectOfType<FadeScript>().fadeInState)
                {
                    yield return null;
                }
            }

            if (fadeOut)
            {
                loadingObj.SetActive(true);
                loadingObj.GetComponent<RectTransform>().localScale = Vector3.one;
                StartCoroutine(LoadSceneAsynchronously(sceneName));
            }
            else
            {
                isLoading = false;
            }
        }
    }
}