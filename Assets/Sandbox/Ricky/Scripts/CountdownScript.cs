using OutGame.GameManager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using OutGame.TimeManager;
using Unity.VisualScripting;

public class CountdownScript : MonoBehaviour
{
    [SerializeField] private RectTransform three;
    [SerializeField] private RectTransform two;
    [SerializeField] private RectTransform one;
    [SerializeField] private RectTransform startTxt;

    // Start is called before the first frame update
    void Start()
    {
        three.gameObject.SetActive(false);
        two.gameObject.SetActive(false);
        one.gameObject.SetActive(false);
        startTxt.gameObject.SetActive(false);

        StartCoroutine(CountDown());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator ShowNumber(RectTransform imageToShow)
    {
        imageToShow.gameObject.SetActive(true);

        imageToShow.sizeDelta = new Vector2(0, 0);

        while (imageToShow.sizeDelta.x != 450.0f)
        {
            imageToShow.sizeDelta = Vector2.MoveTowards(imageToShow.sizeDelta, new Vector2(450.0f, 450.0f), TimeManager.instance.deltaTime * 2000.0f);
            yield return null;
        }

        while (imageToShow.sizeDelta.x != 400.0f)
        {
            imageToShow.sizeDelta = Vector2.MoveTowards(imageToShow.sizeDelta, new Vector2(400.0f, 400.0f), TimeManager.instance.deltaTime * 500.0f);
            yield return null;
        }
    }

    IEnumerator ShowStart()
    {
        startTxt.gameObject.SetActive(true);
        var startTrans = startTxt.GetComponent<RectTransform>();

        startTrans.sizeDelta = new Vector2(0, 0);
        var targetSize = new Vector2(1000, 500);
        
        while (startTrans.sizeDelta != targetSize)
        {
            startTrans.sizeDelta = Vector2.MoveTowards(startTrans.sizeDelta, targetSize, TimeManager.instance.deltaTime * 10000.0f);
            yield return null;
        }
    }

    IEnumerator CountDown()
    {
        yield return new WaitForSeconds(0.5f);

        StartCoroutine(ShowNumber(three));
        two.gameObject.SetActive(false);
        one.gameObject.SetActive(false);

        yield return new WaitForSeconds(1.0f);

        StartCoroutine(ShowNumber(two));
        three.gameObject.SetActive(false);
        one.gameObject.SetActive(false);

        yield return new WaitForSeconds(1.0f);

        StartCoroutine(ShowNumber(one));
        three.gameObject.SetActive(false);
        two.gameObject.SetActive(false);

        yield return new WaitForSeconds(1.0f);

        three.gameObject.SetActive(false);
        two.gameObject.SetActive(false);
        one.gameObject.SetActive(false);

        StartCoroutine(ShowStart());

        GameManager.instance.StartGame();

        yield return new WaitForSeconds(1.5f);

        startTxt.gameObject.SetActive(false);
    }
}
