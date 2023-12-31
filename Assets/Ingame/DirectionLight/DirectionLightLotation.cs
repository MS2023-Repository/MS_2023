using OutGame.TimeManager;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace Ingame.DirectionLight
{
    public class DirectionLightRotation : MonoBehaviour
    {
        private Color morningColor = new Color(1f, 0.6f, 0.35f); // 朝日の色
        private Color middayColor = new Color(1f, 1f, 1f); // 昼間の色
        private Color eveningColor = new Color(1f, 0.6f, 0.35f); // 夕日の色
        private Color nightColor = new Color(0.3754005f, 0.3754005f, 0.7169812f); // 夜の色
        
        private float rotationSpeed = 360.0f / 1440.0f; // 1分あたりの回転速度（1日で360度回転）
        public float initialTime = 6 * 60.0f; // 初期時間を分単位で指定（6時）

        public float minIntensity = 0.2f; // 最小Intensity
        public float maxIntensity = 1.2f; // 最大Intensity

        private Light sunLight; // Lightコンポーネントへの参照
        private float shadowTransitionSpeed = 0.5f; // 影の強度変化の速度

        private Volume globalVolume;
        private Toguchi.Rendering.Flare flare;
        private FloatParameter flareStartSize; 

        private void Start()
        {
            // Lightコンポーネントを取得
            sunLight = GetComponent<Light>();
            // Global Volumeの参照を取得
            globalVolume = FindObjectOfType<Volume>();
            if (globalVolume != null && globalVolume.profile.TryGet<Toguchi.Rendering.Flare>(out flare))
            {
              
            }
            else
            {
               
            }
            flareStartSize = flare.flareSize;
        }

        private void Update()
        {
            // TimeManagerの経過時間を取得
            float currentTime = TimeManager.instance.GetCurrentMinute();

            // 時間の差を計算して回転角度を設定
            float timeDifference = currentTime - initialTime;
            float rotationAngle = timeDifference * rotationSpeed;

            // GameObjectを回転させる
            transform.localRotation = Quaternion.Euler(rotationAngle, -90, 0.0f);

            // 太陽の高度角を計算
            float sunAltitude = Mathf.Sin(Mathf.Deg2Rad * rotationAngle);

            // 時間に応じてIntensityを調整
            float intensity = Mathf.Lerp(minIntensity, maxIntensity, sunAltitude);
            sunLight.intensity = Mathf.Max(intensity, minIntensity);
            // 影の強度を徐々に変化させる
            float targetShadowStrength = sunLight.intensity > minIntensity ? 1.0f : 0.0f;
            sunLight.shadowStrength = Mathf.Lerp(sunLight.shadowStrength, targetShadowStrength, shadowTransitionSpeed * Time.deltaTime);
            flare.flareSize = new FloatParameter((flareStartSize.value * sunLight.shadowStrength * 20),true);
            
            // 太陽の色を更新
            UpdateSunColor(rotationAngle);
        }

        private void UpdateSunColor(float rotationAngle)
        {
            if (rotationAngle < 45.0f) // 朝
            {
                sunLight.color = Color.Lerp(nightColor, morningColor, rotationAngle / 45.0f);
            }
            else if (rotationAngle < 135.0f) // 昼
            {
                sunLight.color = Color.Lerp(morningColor, middayColor, (rotationAngle - 45.0f) / 90.0f);
            }
            else if (rotationAngle < 225.0f) // 夕方
            {
                sunLight.color = Color.Lerp(middayColor, eveningColor, (rotationAngle - 135.0f) / 90.0f);
            }
            else // 夜
            {
                sunLight.color = Color.Lerp(eveningColor, nightColor, (rotationAngle - 225.0f) / 135.0f);
            }
        }
    }
}