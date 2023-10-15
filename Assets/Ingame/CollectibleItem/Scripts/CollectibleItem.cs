using UnityEngine;

namespace InGame.CollectibleItem
{
    public class CollectibleItem : MonoBehaviour
    {
        // 移動する距離
        [SerializeField]
        private float _moveDistance = 2f;
        // イージングにかける時間
        [SerializeField]
        private const float EaseDuration = 1f;
        
        // 回転を制御するブール型の変数
        [SerializeField]
        public bool rotateEnabled = true; 
        // 回転速度（度/秒）
        [SerializeField]
        public float rotationSpeed = 45f;
        
        //開始位置
        private Vector3 _startPosition;
        //開始時間
        private float _startTime;

        /// <summary>
        /// 初期化処理
        /// </summary>
        void Start()
        {
            _startPosition = transform.position;
            _startTime = Time.time;
        }

        /// <summary>
        /// 更新処理
        /// </summary>
        void Update()
        {
            float elapsedTime = Time.time - _startTime;
            float t = Mathf.SmoothStep(0, 1, elapsedTime / EaseDuration);

            // イージングを考慮した位置計算
            float newY = Mathf.Lerp(_startPosition.y, _startPosition.y + _moveDistance, t);

            // オブジェクトを移動
            transform.position = new Vector3(transform.position.x, newY, transform.position.z);

            // 上下の移動をループする
            if (elapsedTime >= EaseDuration)
            {
                _startTime = Time.time;
                _startPosition = transform.position;
                _moveDistance = -_moveDistance; // 逆方向に移動
            }
            
            if (rotateEnabled)
            {
                transform.Rotate(Vector3.up * rotationSpeed * Time.deltaTime);
            }
        }

        /// <summary>
        /// 外部から位置を設定するためのメソッド
        /// </summary>
        /// <param name="newPosition">新しい位置</param>
        public void SetPosition(Vector3 newPosition)
        {
            _startPosition = newPosition;
        }
    }
}