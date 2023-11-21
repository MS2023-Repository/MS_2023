using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace OutGame.GameManager
{
    using OutGame.TimeManager;

    public class GameManager : MonoBehaviour
    {
        // �}�l�[�W���[�̃V���O���g��
        public static GameManager instance { get; private set; }

        private int collectedItems;
        private int maxItems;

        [SerializeField] private float timeLimit = 210;
        private float elapsedTime;

        [SerializeField] private int targetItems = 5;

        public bool isInGame()
        {
            return elapsedTime < timeLimit;
        }

        public void AddCollectedItems(int num)
        {
            collectedItems += num;
        }

        public float GetCollectedItemPercentage()
        {
            float num = (float)collectedItems / (float)targetItems;
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

            DontDestroyOnLoad(this);
        }

        // Start is called before the first frame update
        void Start()
        {
            collectedItems = 0;

            maxItems = GameObject.FindGameObjectsWithTag("CollectibleObject").Count();

            targetItems = Mathf.Clamp(targetItems, 0, maxItems);

            elapsedTime = 0;
        }

        // Update is called once per frame
        void Update()
        {
            if (elapsedTime > timeLimit)
            {

            }
            else
            {
                elapsedTime += TimeManager.instance.deltaTime;
            }
        }
    }
}