using System.Collections;
using System.Collections.Generic;
using OutGame.SceneManager;
using UnityEngine;
using UnityEngine.UI;

namespace OutGame.UI
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private GameObject[] UIElements;
        [SerializeField] private GameObject canvasPrefab;

        private GameObject canvasObj;

        // Start is called before the first frame update
        void Start()
        {
            CheckForCanvas();

            string sceneName = SceneLoader.instance.GetCurrentScene();
            if (sceneName != "Title" && sceneName != "StageSelect")
            {
                if (UIElements.Length > 0)
                {
                    foreach (var element in UIElements)
                    {
                        var newUI = Instantiate(element, canvasObj.transform);
                        Debug.Log("created");
                    }
                }
            }
        }

        private void CheckForCanvas()
        {
            if (GameObject.Find("Canvas") != null)
            {
                canvasObj = GameObject.Find("Canvas");
                canvasObj.GetComponent<Canvas>().renderMode = RenderMode.ScreenSpaceCamera;
                canvasObj.GetComponent<Canvas>().worldCamera = Camera.main;
            }
            else
            {
                canvasObj = Instantiate(canvasPrefab);
                canvasObj.name = "Canvas";
                canvasObj.GetComponent<Canvas>().worldCamera = Camera.main;
            }
        }
    }
}
