using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using OutGame.InputManager;
using OutGame.SceneManager;
using OutGame.TimeManager;
using OutGame.Audio;

public class TitleScript : MonoBehaviour
{
    enum MENUSTATE
    {
        PRESS,
        MAIN,

        MAX
    }

    enum MENUS
    {
        START,
        QUIT,

        MAX
    }
    
    [SerializeField] private TextMeshProUGUI[] buttons;

    private MENUSTATE menuState;

    private MENUS selectedMenu;
    private float alphaTime;

    private bool increaseTime;

    private float t;

    private Color32 invisColor;
    private Color32 visibleColor;

    [SerializeField] private GameObject titleSphere;

    // Start is called before the first frame update
    void Start()
    {
        menuState = MENUSTATE.PRESS;

        selectedMenu = MENUS.START;

        alphaTime = 0.5f;
        increaseTime = false;

        invisColor = new Color32(255, 255, 255, 0);
        visibleColor = new Color32(255, 255, 255, 255);
    }

    // Update is called once per frame
    void Update()
    {
        titleSphere.transform.Rotate(0, 0, TimeManager.instance.deltaTime * 5);

        float menuDir = InputManager.instance.GetMenuMovement().y;

        t = alphaTime / 1.5f;
        t = t * t * (3f - 2f * t);

        if (increaseTime)
        {
            alphaTime += TimeManager.instance.unscaledDeltaTime;
        }
        else
        {
            alphaTime -= TimeManager.instance.unscaledDeltaTime;
        }

        if (alphaTime >= 1.0f)
        {
            increaseTime = false;
        }
        if (alphaTime <= 0.3f)
        {
            increaseTime = true;
        }

        switch (menuState)
        {
            case MENUSTATE.PRESS:
                buttons[0].gameObject.SetActive(true);
                buttons[1].gameObject.SetActive(false);
                buttons[2].gameObject.SetActive(false);

                if (InputManager.instance.menuSelectedState)
                {
                    AudioManager.instance.PlaySE("SelectButton");
                    menuState = MENUSTATE.MAIN;
                    alphaTime = 1.0f;
                }

                buttons[0].faceColor = Color32.Lerp(invisColor, visibleColor, t);

                break;
            case MENUSTATE.MAIN:
                buttons[0].gameObject.SetActive(false);
                buttons[1].gameObject.SetActive(true);
                buttons[2].gameObject.SetActive(true);

                if (menuDir > 0)
                {
                    switch (selectedMenu)
                    {
                        case MENUS.QUIT:
                            AudioManager.instance.PlaySE("MoveButton");
                            selectedMenu = MENUS.START;
                            alphaTime = 1.0f;
                            break;
                        default:
                            break;
                    }
                }
                else if (menuDir < 0)
                {
                    switch (selectedMenu)
                    {
                        case MENUS.START:
                            AudioManager.instance.PlaySE("MoveButton");
                            selectedMenu = MENUS.QUIT;
                            alphaTime = 1.0f;
                            break;
                        default:
                            break;
                    }
                }

                if (InputManager.instance.menuSelectedState)
                {
                    AudioManager.instance.PlaySE("SelectButton");

                    switch (selectedMenu)
                    {
                        case MENUS.START:
                            // �Q�[���J�n
                            SceneLoader.instance.LoadScene("StageSelect");
                            break;
                        case MENUS.QUIT:
                            Application.Quit();
                            break;
                    }
                }
                break;
        }

        UpdateButtons();
    }

    private void UpdateButtons()
    {
        int buttonIds = 0;

        switch (selectedMenu)
        {
            case MENUS.START:
                buttonIds = 0;
                break;
            case MENUS.QUIT:
                buttonIds = 1;
                break;
        }

        for (int i = 0; i < 2; i++)
        {
            if (i == buttonIds)
            {
                buttons[i + 1].faceColor = Color32.Lerp(invisColor, visibleColor, t);
            }
            else
            {
                buttons[i + 1].faceColor = visibleColor;
            }
        }
    }
}