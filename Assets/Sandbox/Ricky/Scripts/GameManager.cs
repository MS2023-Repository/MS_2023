using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace OutGame.GameManager
{
    public class GameManager : MonoBehaviour
    {
        // マネージャーのシングルトン
        public static GameManager instance { get; private set; }

        private int collectedItems;
        private int maxItems;

        private void AddCollectedItems(int num)
        {
            collectedItems += num;
        }

        public float GetCollectedItemPercentage()
        {
            float num = (float)collectedItems / (float)maxItems;
            return num;
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

            DontDestroyOnLoad(this);
        }

        // Start is called before the first frame update
        void Start()
        {
            collectedItems = 0;

            maxItems = 10;
        }

        // Update is called once per frame
        void Update()
        {
            //if (Input.GetKeyDown(KeyCode.UpArrow))
            //{
            //    collectedItems++;
            //}
        }
    }
}