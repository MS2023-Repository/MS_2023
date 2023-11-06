using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using OutGame.InputManager;
using OutGame.SceneManager;

public class TitleScript : MonoBehaviour
{

    enum MENUS
    {
        START,
        QUIT,

        MAX
    }
    
    [SerializeField] private Image[] buttons;

    private MENUS selectedMenu;

    // Start is called before the first frame update
    void Start()
    {
        selectedMenu = MENUS.START;
    }

    // Update is called once per frame
    void Update()
    {
        float menuDir = InputManager.instance.GetMenuMovement().y;

        if (menuDir > 0)
        {
            switch (selectedMenu)
            {
                case MENUS.QUIT:
                    selectedMenu = MENUS.START;
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
                    selectedMenu = MENUS.QUIT;
                    break;
                default:
                    break;
            }
        }

        if (InputManager.instance.menuSelectedState)
        {
            switch (selectedMenu)
            {
                case MENUS.START:
                    // ƒQ[ƒ€ŠJŽn
                    SceneLoader.instance.LoadScene("ProtoScene");
                    break;
                case MENUS.QUIT:
                    Application.Quit();
                    break;
            }
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
                buttons[i].color = Color.gray;
            }
            else
            {
                buttons[i].color = Color.white;
            }
        }
    }
}