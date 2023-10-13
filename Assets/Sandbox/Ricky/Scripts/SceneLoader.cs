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
            if (Input.GetKeyDown(KeyCode.Space))
            {
                LoadScene("SampleScene");
            }

            ResetLoadingScreen();

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
            StartCoroutine(LoadSceneAsynchronously(sceneName));
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

        private void ResetLoadingScreen()
        {
            if (GameObject.Find("LoadingScreen") == null)
            {
                loadingObj = Instantiate(loadingScreenObj);
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