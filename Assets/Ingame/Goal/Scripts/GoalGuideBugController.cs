using UnityEngine;

public class GoalGuideBugController : MonoBehaviour
{
    public float moveSpeed = 5.0f;
    public float lifeTime = 10.0f; // 秒単位での寿命
    public float yVariance = 1.0f; // Y座標の揺れ幅
    public float scaleTime = 0.5f; // サイズ変更にかかる時間

    private Transform target;
    private float timer;
    private Vector3 originalScale;
    private bool scalingDown = false;
    private Light lightComponent;
    private float originalIntensity;

    void Start()
    {
        originalScale = transform.localScale;
        transform.localScale = Vector3.zero; // 初期サイズを0に設定

        // Lightコンポーネントを取得
        lightComponent = GetComponent<Light>();
        if (lightComponent != null)
        {
            originalIntensity = lightComponent.intensity;
            lightComponent.intensity = 0; // 初期の光の強度を0に設定
        }
    }

    public void SetTarget(Transform targetTransform)
    {
        target = targetTransform;
    }

    void Update()
    {
        if (target != null)
        {
            MoveTowardsTarget();
        }

        timer += Time.deltaTime;

        // サイズと光の強度を徐々に変更
        if (timer < scaleTime)
        {
            float scaleLerp = timer / scaleTime;
            transform.localScale = Vector3.Lerp(Vector3.zero, originalScale, scaleLerp);
            if (lightComponent != null)
            {
                lightComponent.intensity = Mathf.Lerp(0, originalIntensity, scaleLerp);
            }
        }
        else if (timer >= lifeTime - scaleTime && !scalingDown)
        {
            scalingDown = true;
            timer = 0; // タイマーをリセット
        }

        // 消滅前にサイズと光の強度を小さくする
        if (scalingDown)
        {
            float scaleLerp = timer / scaleTime;
            transform.localScale = Vector3.Lerp(originalScale, Vector3.zero, scaleLerp);
            if (lightComponent != null)
            {
                lightComponent.intensity = Mathf.Lerp(originalIntensity, 0, scaleLerp);
            }
        }

        // 寿命が尽きたらオブジェクトを破棄
        if (timer >= lifeTime)
        {
            Destroy(gameObject);
        }
    }

    void MoveTowardsTarget()
    {
        Vector3 direction = (target.position - transform.position).normalized;
        // Y座標の揺れを追加
        direction.y += Mathf.Sin(Time.time) * yVariance;
        transform.position += direction * moveSpeed * Time.deltaTime;
    }
}

