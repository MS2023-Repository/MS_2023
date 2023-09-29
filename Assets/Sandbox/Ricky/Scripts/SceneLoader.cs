using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private GameObject loadingScreenObj;

    private GameObject loadingObj;

    // Start is called before the first frame update
    void Start()
    {
        if (GameObject.Find("LoadingScreen") == null)
        {
            loadingObj = Instantiate(loadingScreenObj);
        }
        else
        {
            loadingObj = GameObject.Find("LoadingScreen");
        }

        loadingObj.transform.SetParent(GameObject.Find("Canvas").transform);
        loadingObj.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
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
            loadingObj.transform.GetChild(1).GetComponent<Slider>().value = operation.progress;
            yield return null;
        }

        loadingObj.SetActive(false);
    }
}