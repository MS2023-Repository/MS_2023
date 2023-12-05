using UnityEngine;

namespace OutGame.TimeManager
{
    using OutGame.PauseManager;
    using OutGame.SceneManager;

    public class TimeManager : MonoBehaviour
    {
        public static TimeManager instance;

        public float deltaTime { get; private set; }
        public float unscaledDeltaTime { get; private set; }

        private float timeSpeed;

        private float currentTime = 0.0f; // 現在の経過時間（分）

        public float daytimescale = 1.0f; // 1日が経過するのにかかる秒数

        private const float minutesInADay = 1440.0f; // 1日の合計分数

        private float setSpeed;

        private string sceneName;

        public void SetTimeSpeed(float speed)
        {
            setSpeed = speed;
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
            setSpeed = 1;
            timeSpeed = setSpeed;

            deltaTime = Time.deltaTime;
            unscaledDeltaTime = Time.unscaledDeltaTime;

            sceneName = SceneLoader.instance.GetCurrentScene();
        }

        // Update is called once per frame
        void Update()
        {
            if (sceneName != "Title" && sceneName != "StageSelect")
            {
                if (PauseManager.instance.isPaused)
                {
                    timeSpeed = 0;
                }
                else
                {
                    timeSpeed = setSpeed;
                }
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

        public int GetCurrentMinuteTime()
        {
            return Mathf.FloorToInt(currentTime % 60);
        }

        public float GetMinutesInADay()
        {
            return minutesInADay;
        }
    }
}