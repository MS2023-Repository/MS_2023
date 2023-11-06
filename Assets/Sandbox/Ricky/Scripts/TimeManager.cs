using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OutGame.TimeManager
{
    using OutGame.PauseManager;

    public class TimeManager : MonoBehaviour
    {
        public static TimeManager instance;

        public float deltaTime { get; private set; }
        public float unscaledDeltaTime { get; private set; }

        private float timeSpeed;

        public void SetTimeSpeed(float speed)
        {
            timeSpeed = speed;
            timeSpeed = Mathf.Clamp01(timeSpeed);
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
            timeSpeed = 1;

            deltaTime = Time.deltaTime;
            unscaledDeltaTime = Time.unscaledDeltaTime;
        }

        // Update is called once per frame
        void Update()
        {
            if (PauseManager.instance.isPaused)
            {
                timeSpeed = 0;
            }

            Time.timeScale = timeSpeed;

            deltaTime = Time.deltaTime;
            unscaledDeltaTime = Time.unscaledDeltaTime;
        }
    }
}