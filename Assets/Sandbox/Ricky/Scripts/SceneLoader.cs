using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OutGame.SceneManager
{
    using UnityEngine.SceneManagement;
    using UnityEngine.UI;
    using OutGame.TimeManager;

    public class SceneLoader : MonoBehaviour
    {
        public static SceneLoader instance { get; private set; }

        [SerializeField] private GameObject loadingScreenObj;

        private GameObject loadingObj;
        private Image fadePanel;

        private string sceneName;

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

        public void LoadScene(string sceneName)
        {
            StartCoroutine(FadeScreen(true, sceneName));
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
                yield return new WaitForSeconds(0.5f);
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
            Debug.Log("Scene Changed");

            sceneName = GetCurrentScene();

            StartCoroutine(FadeScreen(false, string.Empty));
        }

        IEnumerator FadeScreen(bool fadeOut, string sceneName)
        {
            var color = fadePanel.color;

            float targetA = fadeOut ? 1f : 0f;

            while (color.a != targetA)
            {
                color.a = Mathf.MoveTowards(color.a, targetA, TimeManager.instance.unscaledDeltaTime);
                fadePanel.color = color;
                Debug.Log(color.a);
                yield return null;
            }

            if (fadeOut)
            {
                loadingObj.SetActive(true);
                loadingObj.GetComponent<RectTransform>().localScale = Vector3.one;
                StartCoroutine(LoadSceneAsynchronously(sceneName));
            }
        }
    }
}