using System.Collections;
using Unity.VisualScripting;
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
        
        // 開始位置
        private Vector3 _startPosition;
        // 開始時間
        private float _startTime;

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
            if (!pickedUp)
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
            else
            {

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

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Player")
            {
                Debug.Log(other.gameObject.name);
                StartCoroutine(PickUpObject(other.gameObject));
            }
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.gameObject.tag == "Player")
            {
                pickedUp = true;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.tag == "Player")
            {
                //pickedUp = false;
                //this.transform.parent.GetComponent<Rigidbody>().useGravity = false;
            }
        }

        private void OnCollisionExit(Collision other)
        {
            
        }

        IEnumerator PickUpObject(GameObject playerObj)
        {
            if (!pickedUp)
            {
                this.transform.parent.position = playerObj.transform.position + new Vector3(0, 1, 0);
                this.transform.parent.GetComponent<Rigidbody>().useGravity = true;

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