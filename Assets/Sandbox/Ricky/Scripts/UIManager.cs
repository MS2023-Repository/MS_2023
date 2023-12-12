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

        private string sceneName;

        // Start is called before the first frame update
        void Start()
        {
            sceneName = SceneLoader.instance.GetCurrentScene();

            CheckForCanvas();

            if (sceneName != "Title" && sceneName != "StageSelect")
            {
                if (UIElements.Length > 0)
                {
                    foreach (var element in UIElements)
                    {
                        if (GameObject.Find(element.name) == null)
                        {
                            var newUI = Instantiate(element, canvasObj.transform);
                        }
                    }
                }
            }
        }

        private void CheckForCanvas()
        {
            if (GameObject.Find("Canvas") != null)
            {
                canvasObj = GameObject.Find("Canvas");
            }
            else
            {
                canvasObj = Instantiate(canvasPrefab);
                canvasObj.name = "Canvas";
            }

            SetWorldCamera();
        }

        private void SetWorldCamera()
        {
            canvasObj.GetComponent<Canvas>().worldCamera = Camera.main;
            canvasObj.GetComponent<Canvas>().renderMode = RenderMode.ScreenSpaceCamera;
            canvasObj.GetComponent<Canvas>().planeDistance = 1;
        }
    }
}
