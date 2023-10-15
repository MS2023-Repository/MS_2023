using UnityEngine;

namespace InGame.Goal
{
    public class CollectibleItem : MonoBehaviour
    {
        [SerializeField]
        private float easeDurationGrow = 1.0f; // 成長にかける時間
        [SerializeField]
        private float easeDurationShrink = 1.0f; // 縮小にかける時間
        [SerializeField]
        private GameObject _holeImage; // 拡大するGameObject

        private Vector3 initialScale; // 初期のスケール
        private Vector3 targetScale; // 目標のスケール
        private float elapsedTime; // 経過時間
        private bool isGrowing = false; // 成長中かどうか
        private bool isShrinking = false; // 縮小中かどうか

        private void Start()
        {
            initialScale = _holeImage.transform.localScale; // _holeImageのスケールを初期値とする
            targetScale = initialScale;
        }

        private void Update()
        {
            // 成長中のイージング処理
            if (isGrowing)
            {
                elapsedTime += Time.deltaTime;
                float t = Mathf.SmoothStep(0, 1, elapsedTime / easeDurationGrow);
                _holeImage.transform.localScale = Vector3.Lerp(initialScale, targetScale, t);

                if (elapsedTime >= easeDurationGrow)
                {
                    // 成長が完了したら縮小へ
                    isGrowing = false;
                    isShrinking = true;
                    elapsedTime = 0;
                }
            }
            // 縮小中のイージング処理
            else if (isShrinking)
            {
                elapsedTime += Time.deltaTime;
                float t = Mathf.SmoothStep(0, 1, elapsedTime / easeDurationShrink);
                _holeImage.transform.localScale = Vector3.Lerp(targetScale, initialScale, t);

                if (elapsedTime >= easeDurationShrink)
                {
                    // 縮小が完了したら縮小終了
                    isShrinking = false;
                }
            }
        }

        // 外部から呼び出されるメソッドで指定されたサイズを加算する
        public void HitCollectibleItem(float size)
        {
            targetScale.x += size;
            targetScale.z += size;

            // 成長を開始
            isGrowing = true;
            elapsedTime = 0;
        }
    }
}
