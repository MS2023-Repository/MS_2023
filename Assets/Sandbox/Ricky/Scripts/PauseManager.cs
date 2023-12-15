using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace OutGame.PauseManager
{
    using OutGame.Audio;
    using OutGame.InputManager;
    using OutGame.SceneManager;
    using OutGame.TimeManager;

    public class PauseManager : MonoBehaviour
    {
        enum MENUOPTIONS
        {
            RESUME,
            RETRY,
            EXIT,

            MAX
        }

        public static PauseManager instance;

        public bool isPaused {get; private set;}

        private Image blackPanel;
        [SerializeField] GameObject[] buttonObjs;

        private MENUOPTIONS currentSelected;

        public void PauseGame()
        {
            if (!isPaused)
            {
                isPaused = true;
                blackPanel.gameObject.SetActive(true);
            }
        }

        public void ResumeGame()
        {
            if (isPaused)
            {
                isPaused = false;
                blackPanel.gameObject.SetActive(false);

                AudioManager.instance.PlaySE("CancelSE");
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
        }

        // Start is called before the first frame update
        void Start()
        {
            isPaused = false;

            blackPanel = transform.GetChild(0).GetComponent<Image>();

            blackPanel.gameObject.SetActive(false);

            this.transform.SetParent(GameObject.Find("Canvas").transform);
            this.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;

            currentSelected = MENUOPTIONS.RESUME;
        }

        // Update is called once per frame
        void Update()
        {
            if (isPaused)
            {
                if (InputManager.instance.GetMenuMovement().y > 0)
                {
                    switch (currentSelected)
                    {
                        case MENUOPTIONS.RETRY:
                            currentSelected = MENUOPTIONS.RESUME;
                            break;
                        case MENUOPTIONS.EXIT:
                            currentSelected = MENUOPTIONS.RETRY;
                            break;
                    }

                    AudioManager.instance.PlaySE("MoveButton");
                }
                else if (InputManager.instance.GetMenuMovement().y < 0)
                {
                    switch (currentSelected)
                    {
                        case MENUOPTIONS.RESUME:
                            currentSelected = MENUOPTIONS.RETRY;
                            break;
                        case MENUOPTIONS.RETRY:
                            currentSelected = MENUOPTIONS.EXIT;
                            break;
                    }

                    AudioManager.instance.PlaySE("MoveButton");
                }

                UpdateButtonPositions();

                if (InputManager.instance.pMenuSelectedState)
                {
                    switch (currentSelected)
                    {
                        case MENUOPTIONS.RESUME:
                            ResumeGame();
                            AudioManager.instance.PlaySE("PauseSE");
                            break;
                        case MENUOPTIONS.RETRY:
                            SceneLoader.instance.ReloadScene();
                            AudioManager.instance.PlaySE("SelectButton");
                            isPaused = false;
                            blackPanel.gameObject.SetActive(false);
                            break;
                        case MENUOPTIONS.EXIT:
                            SceneLoader.instance.LoadScene("Title");
                            AudioManager.instance.PlaySE("SelectButton");
                            isPaused = false;
                            blackPanel.gameObject.SetActive(false);
                            break;
                    }
                }
            }
        }

        private void UpdateButtonPositions()
        {
            int buttonId;
            switch (currentSelected)
            {
                case MENUOPTIONS.RESUME:
                    buttonId = 0;
                    break;
                case MENUOPTIONS.RETRY:
                    buttonId = 1;
                    break;
                case MENUOPTIONS.EXIT:
                    buttonId = 2;
                    break;
                default:
                    buttonId = 0;
                    break;
            }

            for (int i = 0; i < 3; i++)
            {
                if (i == buttonId)
                {
                    buttonObjs[i].GetComponent<Image>().color = Color.gray;
                }
                else
                {
                    buttonObjs[i].GetComponent<Image>().color = Color.white;
                }
            }
        }
    }
}