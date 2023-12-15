using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InGame.CollectibleItem
{
    public class RespawnCollectibleItems : MonoBehaviour
    {
        [SerializeField] float _RespawnTime;
        [SerializeField] GameObject _RespawnItem;
        [SerializeField] float _RespawnRange;

        private float _NowTime;  

        // Start is called before the first frame update
        void Start()
        {
            _NowTime = 0.0f;
        }

        private void FixedUpdate()
        {
            _NowTime += Time.deltaTime;

            if (_NowTime >= _RespawnTime)
            {
                Debug.Log("afafafa");

                Vector3 randamPos = new Vector3(Random.Range(0.0f, _RespawnRange),0.0f,Random.Range(0.0f, _RespawnRange));

                Vector3 respawnPos = transform.position + randamPos;

                Instantiate(
                    _RespawnItem,
                    respawnPos,
                    Quaternion.identity
                );

                _NowTime = 0.0f;
            }
        }
    }
}
