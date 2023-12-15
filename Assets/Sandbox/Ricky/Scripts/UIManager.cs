using System.Collections;
using System.Collections.Generic;
using OutGame.SceneManager;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

namespace OutGame.UI
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private GameObject[] UIElements;
        [SerializeField] private GameObject canvasPrefab;

        [SerializeField] private GameObject UICamera;
        private GameObject uiCam;

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

                if (GameObject.FindGameObjectWithTag("UICamera") == null)
                {
                    uiCam = Instantiate(UICamera);
                }
                else
                {
                    uiCam = GameObject.FindGameObjectWithTag("UICamera");
                }
                
                Camera.main.GetUniversalAdditionalCameraData().cameraStack.Add(uiCam.GetComponent<Camera>());
            }

            SetWorldCamera();
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
        }

        private void SetWorldCamera()
        {
            canvasObj.GetComponent<Canvas>().renderMode = RenderMode.ScreenSpaceCamera;

            if (sceneName != "Title" && sceneName != "StageSelect")
            {
                canvasObj.GetComponent<Canvas>().worldCamera = uiCam.GetComponent<Camera>();
            }

            canvasObj.GetComponent<Canvas>().planeDistance = 1;
        }
    }
}
