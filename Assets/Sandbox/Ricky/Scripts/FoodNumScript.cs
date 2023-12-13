using OutGame.TimeManager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FoodNumScript : MonoBehaviour
{
    [SerializeField] private GameObject[] numObj;

    private int scoreNum;
    private float numSpace;

    private Vector2 size;

    private List<GameObject> numObjLine;

    private float elapsedTime;
    private RectTransform rectTransform;

    private bool eraseObj;

    private bool startCount;

    public void SetNum(int num)
    {
        InitFunc();
        scoreNum = num;
        CreateNumber();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void InitFunc()
    {
        scoreNum = 0;
        numSpace = 45;

        size = new Vector2(60, 60);

        elapsedTime = 0;

        rectTransform = GetComponent<RectTransform>();

        eraseObj = false;
        startCount = false;

        numObjLine = new List<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        if (startCount)
        {
            elapsedTime += TimeManager.instance.deltaTime;
            rectTransform.anchoredPosition3D += Vector3.up;

            if (elapsedTime >= 1)
            {
                UpdateColor();
            }
        }
    }

    private void CreateNumber()
    {
        if (numObjLine.Count > 0)
        {
            foreach (GameObject obj in numObjLine)
            {
                Destroy(obj);
            }
        }

        numObjLine.Clear();

        int scoreNow = scoreNum;

        while (scoreNow > 0)
        {
            int currentDigit = scoreNow % 10;

            numObjLine.Add(Instantiate(numObj[currentDigit], this.transform));

            scoreNow /= 10;
        }

        numObjLine.Reverse();

        float startPos = 0;

        foreach (GameObject obj in numObjLine)
        {
            obj.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(startPos, 0, 0);
            obj.GetComponent<RectTransform>().sizeDelta = size;
            startPos += numSpace;
        }

        startCount = true;
    }

    private void UpdateColor()
    {
        foreach (GameObject obj in numObjLine)
        {
            Color color = obj.GetComponent<Image>().color;
            color.a -= Time.deltaTime * 2;

            color.a = Mathf.Clamp01(color.a);

            if (color.a <= 0 && !eraseObj)
            {
                eraseObj = true;

                StartCoroutine(DeleteObj());
            }

            obj.GetComponent<Image>().color = color;
        }
    }

    IEnumerator DeleteObj()
    {
        yield return new WaitForSeconds(0.3f);

        Destroy(this.gameObject);
    }
}
