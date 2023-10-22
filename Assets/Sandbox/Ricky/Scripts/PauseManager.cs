using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OutGame.PauseManager
{
    public class PauseManager : MonoBehaviour
    {
        public static PauseManager instance;

        public bool isPaused {get; private set;}

        public void PauseGame()
        {
            isPaused = true;
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

            DontDestroyOnLoad(this);
        }

        // Start is called before the first frame update
        void Start()
        {
            isPaused = false;
        }

        // Update is called once per frame
        void Update()
        {
            if (isPaused)
            {

            }
            else
            {

            }
        }
    }
}