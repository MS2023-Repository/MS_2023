using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Content;
using UnityEngine;
using UnityEngine.UI;
using OutGame.GameManager;
using OutGame.TimeManager;
using DG.Tweening;

namespace InGame.GoalGuide
{
    public class GoalGuideScript : MonoBehaviour
    {
        [SerializeField] Image halfProgress;
        [SerializeField] Image fullProgress;

        private float progressNum;
        private float realProgressNum;

        private float scaleScalar;

        // Start is called before the first frame update
        void Start()
        {
            progressNum = 0;
            realProgressNum = 0;

            scaleScalar = 3.15f;
        }

        // Update is called once per frame
        void Update()
        {
            progressNum = GameManager.instance.GetCollectedItemPercentage();

            if (realProgressNum < progressNum)
            {
                if (scaleScalar < 3.45f)
                {
                    scaleScalar = Mathf.MoveTowards(scaleScalar, 3.45f, TimeManager.instance.deltaTime);
                }
                else
                {
                    realProgressNum = Mathf.MoveTowards(realProgressNum, progressNum, TimeManager.instance.deltaTime / 6.0f);

                    fullProgress.fillAmount = realProgressNum;
                }
            }
            else
            {
                scaleScalar = Mathf.MoveTowards(scaleScalar, 3.15f, TimeManager.instance.deltaTime);
            }

            transform.GetChild(0).gameObject.GetComponent<RectTransform>().localScale = new Vector3(scaleScalar, scaleScalar, scaleScalar);
        }
    }
}