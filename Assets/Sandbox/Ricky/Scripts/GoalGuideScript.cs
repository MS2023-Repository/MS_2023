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

        private GameObject cameraObj;
        private GameObject goalObj;

        // Start is called before the first frame update
        void Start()
        {
            progressNum = 0;
            realProgressNum = 0;

            scaleScalar = 3.15f;

            cameraObj = Camera.main.gameObject;
            goalObj = GameObject.FindGameObjectWithTag("Goal");
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

        private void FixedUpdate()
        {
            UpdatePosition();
        }

        void UpdatePosition()
        {
            Vector3 dir = goalObj.transform.position - cameraObj.transform.position;
            Vector2 screenDir = new Vector2(dir.x, dir.z);

            screenDir.Normalize();

            RaycastHit hit;

            if (Physics.Raycast(cameraObj.transform.position, dir.normalized, out hit, Mathf.Infinity))
            {
                if (hit.transform.gameObject == goalObj)
                {
                    transform.GetChild(0).gameObject.SetActive(false);
                    transform.GetChild(1).gameObject.SetActive(false);
                }
                else
                {
                    transform.GetChild(0).gameObject.SetActive(true);
                    transform.GetChild(1).gameObject.SetActive(true);
                }
            }

            screenDir *= 1200.0f;

            screenDir.y = Mathf.Clamp(screenDir.y, -355, 355);
            screenDir.x = Mathf.Clamp(screenDir.x, -830, 830);

            this.GetComponent<RectTransform>().anchoredPosition = screenDir;

            
        }
    }
}