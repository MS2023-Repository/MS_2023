using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OutGame.SceneManager
{
    using UnityEngine.SceneManagement;
    using UnityEngine.UI;

    public class SceneLoader : MonoBehaviour
    {
        public static SceneLoader instance { get; private set; }

        [SerializeField] private GameObject loadingScreenObj;

        private GameObject loadingObj;

        public string GetCurrentScene()
        {
            return SceneManager.GetActiveScene().name;
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
            SceneManager.activeSceneChanged += ResetLoadingScreen;

            if (GameObject.Find("LoadingScreen") == null)
            {
                loadingObj = Instantiate(loadingScreenObj);
                loadingObj.name = "LoadingScreen";
                loadingObj.SetActive(false);
            }
            else
            {
                loadingObj = GameObject.Find("LoadingScreen");
                loadingObj.SetActive(false);
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (loadingObj.transform.parent == null)
            {
                if (GameObject.Find("Canvas") != null)
                {
                    loadingObj.transform.SetParent(GameObject.Find("Canvas").transform);
                    loadingObj.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
                }
            }
        }

        public void LoadScene(string sceneName)
        {
            loadingObj.SetActive(true);
            loadingObj.GetComponent<RectTransform>().localScale = Vector3.one;
            StartCoroutine(LoadSceneAsynchronously(sceneName));
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
                yield return null;
            }

            loadingObj.SetActive(false);
        }

        private void ResetLoadingScreen(Scene current, Scene next)
        {
            if (GameObject.Find("LoadingScreen") == null)
            {
                loadingObj = Instantiate(loadingScreenObj);
                loadingObj.name = "LoadingScreen";
                loadingObj.SetActive(false);
            }
            else
            {
                loadingObj = GameObject.Find("LoadingScreen");
                loadingObj.SetActive(false);
            }
        }
    }
}