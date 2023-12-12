using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreCounter : MonoBehaviour
{
    [SerializeField] private GameObject[] numObj;

    private int scoreNum;
    private float numSpace;

    private List<GameObject> numObjLine;

    public void AddScore(int num)
    {
        scoreNum += num;

        CreateNumber();
    }

    // Start is called before the first frame update
    void Start()
    {
        scoreNum = 1250;

        numSpace = 150.0f;

        numObjLine = new List<GameObject>();

        CreateNumber();
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

        int numToInsert = 0;
        int digitToCheck = 10000;
        int scoreNow = scoreNum;

        while (scoreNow > 0)
        {
            if (scoreNow >= digitToCheck)
            {
                numToInsert = scoreNow / digitToCheck;

                numObjLine.Add(Instantiate(numObj[numToInsert], this.transform));
            }

            while (scoreNow >= digitToCheck)
            {
                scoreNow -= digitToCheck;
            }

            if (scoreNow < 10)
            {
                numObjLine.Add(Instantiate(numObj[scoreNow], this.transform));
            }

            digitToCheck /= 10;
        }

        float startPos = 0;

        // ‹ô”
        if (numObjLine.Count % 2 == 0)
        {
            startPos -= numSpace / 2;
            startPos -= numSpace * (numObjLine.Count / 2 - 1);
        }
        else
        {
            startPos -= (numObjLine.Count - 1) / 2 * numSpace;
        }

        foreach (GameObject obj in numObjLine)
        {
            obj.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(startPos, -200, 0);
            startPos += numSpace;
        }
    }
}
