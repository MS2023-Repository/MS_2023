using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace OutGame.GameManager
{
    using InGame.CollectibleItem;
    using OutGame.TimeManager;

    public class GameManager : MonoBehaviour
    {
        // �}�l�[�W���[�̃V���O���g��
        public static GameManager instance { get; private set; }

        private int collectedNum;
        private int maxItems;

        private bool startGameState;

        [SerializeField] private float timeLimit = 210;
        private float elapsedTime;

        [SerializeField] private int targetItems = 5;

        public List<GameObject> collectedItems { get; private set; }

        public bool isInGame()
        {
            return elapsedTime < timeLimit && startGameState;
        }

        public bool isEndGame()
        {
            return elapsedTime > timeLimit;
        }

        public void StartGame()
        {
            startGameState = true;
        }

        public void AddCollectedItems(int num, GameObject itemToInsert)
        {
            itemToInsert.transform.position = new Vector3(100, 100, 100);

            collectedNum += num;
            collectedItems.Add(itemToInsert);
        }

        public float GetCollectedItemPercentage()
        {
            float num = (float)collectedNum / (float)targetItems;
            num = Mathf.Clamp01(num);
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
        }

        // Start is called before the first frame update
        void Start()
        {
            startGameState = false;

            collectedNum = 0;

            maxItems = GameObject.FindGameObjectsWithTag("CollectibleObject").Count();

            targetItems = Mathf.Clamp(targetItems, 0, maxItems);

            elapsedTime = 0;

            collectedItems = new List<GameObject>();
        }

        // Update is called once per frame
        void Update()
        {
            if (startGameState)
            {
                if (elapsedTime < timeLimit)
                {
                    elapsedTime += TimeManager.instance.deltaTime;
                }
            }
        }
    }
}