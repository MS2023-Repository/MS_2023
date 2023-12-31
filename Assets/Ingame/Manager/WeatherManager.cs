using UnityEngine;
using OutGame.TimeManager;

public class WeatherManager : MonoBehaviour
{
    public static WeatherManager Instance { get; private set; }

    public Material[] materials;

    [SerializeField] private GameObject snowPrefab;

    private const float MaxSnowValue = 1.7f;
    private const int StartSnowHour = 12;
    private const int FullSnowHour = 18;

    private bool spawnSnow = true;

    public enum WeatherState
    {
        Clear,
        Snow,
        Rain
    }

    public WeatherState currentWeather;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        UpdateSnow();
    }

    void UpdateSnow()
    {
        int currentHour = TimeManager.instance.GetCurrentHour() + 6;
        int currentMinute = TimeManager.instance.GetCurrentMinuteTime();
        float totalMinutes = currentHour * 60 + currentMinute;

        if (totalMinutes >= StartSnowHour * 60 && totalMinutes <= FullSnowHour * 60)
        {
            float progress = (totalMinutes - StartSnowHour * 60) / ((FullSnowHour - StartSnowHour) * 60);
            float snowValue = Mathf.Lerp(0, MaxSnowValue, progress);
            SetSnowValue(snowValue);

            if (spawnSnow)
            {
                spawnSnow = false;
                var snowObj = Instantiate(snowPrefab);
                snowObj.transform.position = GameObject.FindGameObjectWithTag("Goal").transform.position + Vector3.up * 8.0f;
            }
        }
        else if (totalMinutes < StartSnowHour * 60 || totalMinutes > FullSnowHour * 60)
        {
            SetSnowValue(0);
        }
    }


    void SetSnowValue(float value)
    {
        foreach (var material in materials)
        {
            material.SetFloat("_Snow", value);
        }
    }

    public void UpdateMaterials()
    {
        foreach (var material in materials)
        {
            material.SetFloat("_Snow", currentWeather == WeatherState.Snow ? 1 : 0);
        }
    }
}