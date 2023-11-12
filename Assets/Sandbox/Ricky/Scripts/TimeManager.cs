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

        private float currentTime = 0.0f; // 現在の経過時間（分）

        public float daytimescale = 1.0f; // 1日が経過するのにかかる秒数

        private const float minutesInADay = 1440.0f; // 1日の合計分数

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

            // 現在の経過時間を更新
            currentTime += deltaTime / daytimescale * minutesInADay;

            // 1日（1440分）に達した場合、時間をリセット
            if (currentTime >= minutesInADay)
            {
                currentTime = 0.0f;
            }
        }

        // 現在の時間（分）を取得
        public int GetCurrentHour()
        {
            return Mathf.FloorToInt(currentTime / 60);
        }

        public int GetCurrentMinute()
        {
            return Mathf.FloorToInt(currentTime);
        }
    }
}