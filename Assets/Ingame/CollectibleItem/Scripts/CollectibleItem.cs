using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Serialization;
using OutGame.GameManager;

namespace InGame.CollectibleItem
{
    public class CollectibleItem : MonoBehaviour
    {
        // 移動する距離
        [SerializeField]
        private int _scorePoint = 1;

        // 移動する距離
        [SerializeField]
        private float _moveDistance = 2f;

        // イージングにかける時間
        [SerializeField]
        private float EaseDuration = 1f;

        // 回転を制御するブール型の変数
        [SerializeField]
        public bool isHitGroundEnabled = true;

        // 回転速度（度/秒）
        [SerializeField]
        public float rotationSpeed = 45f;

        // "Goal"に吸い込まれる速度
        [SerializeField]
        public float suctionSpeed = 2f;

        // X-Z平面回転に関するパラメータ
        [SerializeField]
        private float rotationAroundGoalSpeed = 45f;
        [SerializeField]
        private float rotationAroundGoalDistance = 2f;

        // 開始位置
        private Vector3 _startPosition;

        // 開始時間
        private float _startTime;

        // 吸い込み中フラグ
        private bool _beingSucked = false;

        // 吸い込まれる対象の位置
        private Vector3 _goalPosition;

        // 拾われた状況
        private bool pickedUp;

        /// <summary>
        /// 初期化処理
        /// </summary>
        void Start()
        {
            _startPosition = transform.position;
            _startTime = Time.time;

            pickedUp = false;
        }

        /// <summary>
        /// 更新処理
        /// </summary>
        void Update()
        {
            if (!_beingSucked)
            {
                this.transform.GetChild(0).GetComponent<Collider>().enabled = true;
                if (!pickedUp)
                {
                    this.transform.GetComponent<Rigidbody>().useGravity = false;
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
                }
            }
            else
            {
                this.transform.GetComponent<Rigidbody>().useGravity = false;
                this.transform.GetChild(0).GetComponent<Collider>().enabled = false;
                RotateAroundXZPlane(transform.position, _goalPosition, rotationAroundGoalDistance);
                // "Goal"に向かって徐々に移動
                float step = suctionSpeed * Time.deltaTime;
                transform.position = Vector3.MoveTowards(transform.position, _goalPosition, step);
                transform.RotateAround(_goalPosition, Vector3.up, rotationAroundGoalSpeed * Time.deltaTime);
                // "Goal"に向かってY方向にも徐々に移動
                float newY = Mathf.Lerp(transform.position.y, _goalPosition.y, step);
                transform.position = new Vector3(transform.position.x, newY, transform.position.z);
            }

            if (isHitGroundEnabled)
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

        /// <summary>
        /// トリガーコライダーに衝突した際に実行されるメソッド
        /// </summary>
        /// <param name="other">衝突したコライダー</param>
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Goal"))
            {
                _goalPosition = other.transform.position;
                _beingSucked = true;
            }
            if (other.CompareTag("GoalHolePoint"))
            {
                other.transform.parent.GetComponent<Goal.CollectibleItem>().HitCollectibleItem(0.02f);
                GameManager.instance.AddCollectedItems(_scorePoint);
                Destroy(this.gameObject);
            }

            if (other.gameObject.tag == "Player")
            {
                StartCoroutine(PickUpObject(other.gameObject));
            }
        }

        /// <summary>
        /// トリガーコライダーに入った際に実行されるメソッド
        /// </summary>
        /// <param name="other">衝突したコライダー</param>
        private void OnTriggerStay(Collider other)
        {
            if (other.CompareTag("Cage"))
            {
                // "Cage"に入った際、rotateEnabledを常にtrueに設定
                isHitGroundEnabled = true;
            }

            if (other.gameObject.tag == "Player")
            {
                pickedUp = true;
            }
        }

        /// <summary>
        /// トリガーコライダーから出た際に実行されるメソッド
        /// </summary>
        /// <param name="other">衝突したコライダー</param>
        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Cage"))
            {
                // "Cage"から出た際、rotateEnabledをfalseに設定
                isHitGroundEnabled = false;
            }
        }
        private Vector3 RotateAroundXZPlane(Vector3 currentPosition, Vector3 goalPosition, float Length)
        {
            // 現在位置からゴール位置までの方向ベクトルを計算
            Vector3 direction = goalPosition - currentPosition;

            // 方向ベクトルをX-Z平面に射影
            direction.y = 0;

            // 回転角度を計算
            float angleInRadians = Mathf.Atan2(direction.z, direction.x);

            // 引数として渡されたposを基準に半径Lengthの場所をX-Z平面で回転
            float newX = currentPosition.x + Length * Mathf.Cos(angleInRadians);
            float newZ = currentPosition.z + Length * Mathf.Sin(angleInRadians);

            // 新しい位置を返す
            return new Vector3(newX, currentPosition.y, newZ);
        }

        IEnumerator PickUpObject(GameObject playerObj)
        {
            if (!pickedUp)
            {
                this.transform.position = playerObj.transform.parent.position + new Vector3(0, 0.5f, 0);
                this.transform.GetComponent<Rigidbody>().useGravity = true;

                this.transform.GetChild(1).gameObject.SetActive(false);

                yield return new WaitForSeconds(0.5f);

                pickedUp = true;
            }
            else
            {
                yield return null;
            }
        }
    }
}