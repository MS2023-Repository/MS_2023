using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OutGame.InputManager
{
    using UnityEngine.InputSystem;
    using UnityEngine.SceneManagement;

    public class InputManager : MonoBehaviour
    {
        public static InputManager instance;

        private MainInputControls inputControls;

        private bool inGameState;

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
            inputControls.PauseMenu.Disable();

            if (SceneManager.GetActiveScene().name.Contains("Level"))
            {
                inputControls.Player.Enable();
                inputControls.Menu.Disable();

                inGameState = true;
            }
            else
            {
                inputControls.Menu.Enable();
                inputControls.Player.Disable();

                inGameState = false;
            }
        }

        // Update is called once per frame
        void Update()
        {
            if (inGameState)
            {
                GameUpdate();
            }
            else
            {
                MenuUpdate();
            }
        }

        private void GameUpdate()
        {
            
        }

        private void MenuUpdate()
        {

        }
    }
}