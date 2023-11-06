using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OutGame.InputManager
{
    using UnityEngine.InputSystem;
    using UnityEngine.SceneManagement;
    using OutGame.PauseManager;
    using OutGame.TimeManager;

    public class InputManager : MonoBehaviour
    {
        public static InputManager instance;

        private MainInputControls inputControls;

        private bool inGameState;

        private Vector2 pMenuMovement;
        public bool pMenuSelectedState { get; private set; }
        public bool pMenuCanceledState { get; private set; }

        private Vector2 menuMovement;
        public bool menuSelectedState { get; private set; }
        public bool menuCanceledState { get; private set; }

        private float input_delay;

        public Vector2 GetMenuMovement()
        {
            Vector2 vecToReturn = Vector2.zero;

            if (input_delay <= 0)
            {
                if (inGameState)
                {
                    if (PauseManager.instance.isPaused)
                    {
                        vecToReturn = pMenuMovement;
                        
                    }
                }
                else
                {
                    vecToReturn = menuMovement;
                }
            }

            return vecToReturn;
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

            inputControls = new MainInputControls();
        }

        // Start is called before the first frame update
        void Start()
        {
            inputControls.PauseMenu.Disable();

            inputControls.PauseMenu.Select.performed += PauseMenuSelectInput;
            inputControls.PauseMenu.Cancel.performed += PauseMenuCancelInput;
            inputControls.Menu.Select.performed += MenuSelectInput;
            inputControls.Menu.Cancel.performed += MenuCancelInput;

            inputControls.Player.Pause.performed += PlayerPauseInput;

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

            input_delay = 0;

            pMenuMovement = Vector2.zero;
            menuMovement = Vector2.zero;
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
            if (PauseManager.instance.isPaused)
            {
                if (!inputControls.PauseMenu.enabled)
                {
                    inputControls.PauseMenu.Enable();
                    inputControls.Player.Disable();
                }

                if (pMenuMovement != Vector2.zero)
                {
                    if (input_delay < 0.3f)
                    {
                        input_delay += TimeManager.instance.unscaledDeltaTime;
                    }
                    else
                    {
                        input_delay = 0;
                    }
                }
                else
                {
                    input_delay = 0;
                }

                pMenuMovement = inputControls.PauseMenu.Move.ReadValue<Vector2>();
            }
            else
            {
                if (!inputControls.Player.enabled)
                {
                    inputControls.Player.Enable();
                    inputControls.PauseMenu.Disable();
                }
            }
        }

        private void MenuUpdate()
        {
            menuMovement = inputControls.Menu.Move.ReadValue<Vector2>();
        }

        private void LateUpdate()
        {
            ResetAllParams();
        }

        private void ResetAllParams()
        {
            pMenuSelectedState = false;
            pMenuCanceledState = false;
            menuSelectedState = false;
            menuCanceledState = false;
        }

        private void PauseMenuSelectInput(InputAction.CallbackContext obj)
        {
            pMenuSelectedState = true;
        }

        private void PauseMenuCancelInput(InputAction.CallbackContext obj)
        {
            pMenuCanceledState = true;
        }

        private void MenuSelectInput(InputAction.CallbackContext obj)
        {
            menuSelectedState = true;
        }

        private void MenuCancelInput(InputAction.CallbackContext obj)
        {
            menuCanceledState = true;
        }

        private void PlayerPauseInput(InputAction.CallbackContext obj)
        {
            PauseManager.instance.PauseGame();
        }
    }
}