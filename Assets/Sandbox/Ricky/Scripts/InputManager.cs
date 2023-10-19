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
            if (SceneManager.GetActiveScene().name.Contains("Level"))
            {
                inputControls.Player.Enable();
                inputControls.PauseMenu.Disable();
            }
            else
            {
                
            }
        }

        // Update is called once per frame
        void Update()
        {
            
        }
    }
}