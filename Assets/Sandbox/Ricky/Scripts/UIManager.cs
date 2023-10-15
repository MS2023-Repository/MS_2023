using System.Collections;
using System.Collections.Generic;
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

            foreach (var element in UIElements) 
            {
                var newUI = Instantiate(element, canvasObj.transform);
            }
        }

        // Update is called once per frame
        void Update()
        {
            CheckForCanvas();
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
    }
}
