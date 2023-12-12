using System.Collections;
using System.Collections.Generic;
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

        [SerializeField] RawImage goalImage;

        private float progressNum;
        private float realProgressNum;

        private float scaleScalar;

        private GameObject cameraObj;
        private GameObject goalObj;

        [SerializeField] private Camera goalCamera;
        private RenderTexture goalTex;
        private Vector3 offsetPos;

        // Start is called before the first frame update
        void Start()
        {
            this.transform.GetChild(0).gameObject.SetActive(true);
            this.transform.GetChild(1).gameObject.SetActive(true);
            this.transform.GetChild(2).gameObject.SetActive(true);

            progressNum = 0;
            realProgressNum = 0;

            scaleScalar = 3.15f;

            cameraObj = Camera.main.gameObject;

            goalObj = GameObject.FindGameObjectWithTag("Goal");

            offsetPos = new Vector3(0, 1, -2);
            goalCamera.transform.parent = null;
            goalCamera.transform.position = goalObj.transform.position + offsetPos;

            if (goalCamera.targetTexture != null)
            {
                goalCamera.targetTexture.Release();
            }

            goalTex = new RenderTexture(1920, 1080, 1);
            goalCamera.targetTexture = goalTex;
            goalImage.texture = goalTex;
        }

        // Update is called once per frame
        void Update()
        {
            if (GameManager.instance.isInGame())
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

                halfProgress.fillAmount = progressNum;

                transform.GetChild(0).gameObject.GetComponent<RectTransform>().localScale = new Vector3(scaleScalar, scaleScalar, scaleScalar);
            }
            else
            {
                this.gameObject.SetActive(false);
            }
        }

        private void FixedUpdate()
        {
            goalCamera.transform.LookAt(goalObj.transform);

            UpdatePosition();
        }

        void UpdatePosition()
        {
            Vector3 dir = goalObj.transform.position - cameraObj.transform.position;
            Vector2 screenDir = new Vector2(dir.x, dir.z);

            screenDir.Normalize();

            screenDir *= 1200.0f;

            screenDir.y = Mathf.Clamp(screenDir.y, -340, 340);
            screenDir.x = Mathf.Clamp(screenDir.x, -830, 830);

            this.GetComponent<RectTransform>().anchoredPosition = screenDir;
        }

        private void OnDisable() 
        {
            if (goalTex != null)
            {
                goalImage.texture = null;
                goalTex.Release();
                goalTex = null;
            }
        }
    }
}