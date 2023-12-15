using OutGame.GameManager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using OutGame.TimeManager;
using Unity.VisualScripting;
using OutGame.Audio;

public class CountdownScript : MonoBehaviour
{
    [SerializeField] private RectTransform three;
    [SerializeField] private RectTransform two;
    [SerializeField] private RectTransform one;
    [SerializeField] private RectTransform startTxt;
    [SerializeField] private RectTransform endTxt;

    private bool startPhase = true;

    public void StartEndCountdown()
    {
        StartCoroutine(CountDown());
    }

    // Start is called before the first frame update
    void Start()
    {
        three.gameObject.SetActive(false);
        two.gameObject.SetActive(false);
        one.gameObject.SetActive(false);
        startTxt.gameObject.SetActive(false);
        endTxt.gameObject.SetActive(false);

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

        AudioManager.instance.PlaySE("CountdownSE");

        while (imageToShow.sizeDelta.x != 350)
        {
            imageToShow.sizeDelta = Vector2.MoveTowards(imageToShow.sizeDelta, new Vector2(350, 350), TimeManager.instance.deltaTime * 2000.0f);
            yield return null;
        }

        while (imageToShow.sizeDelta.x != 300)
        {
            imageToShow.sizeDelta = Vector2.MoveTowards(imageToShow.sizeDelta, new Vector2(300, 300), TimeManager.instance.deltaTime * 1000.0f);
            yield return null;
        }
    }

    IEnumerator ShowStart()
    {
        startTxt.gameObject.SetActive(true);
        var startTrans = startTxt.GetComponent<RectTransform>();

        startTrans.sizeDelta = new Vector2(0, 0);
        var targetSize = new Vector2(1200, 600);
        
        while (startTrans.sizeDelta != targetSize)
        {
            startTrans.sizeDelta = Vector2.MoveTowards(startTrans.sizeDelta, targetSize, TimeManager.instance.deltaTime * 10000.0f);
            yield return null;
        }

        yield return new WaitForSeconds(1.5f);
        startTxt.gameObject.SetActive(false);

        startPhase = false;
    }

    IEnumerator ShowEnd()
    {
        endTxt.gameObject.SetActive(true);
        var endTrans = endTxt.GetComponent<RectTransform>();

        endTrans.sizeDelta = new Vector2(0, 0);
        var targetSize = new Vector2(1600, 800);

        while (endTrans.sizeDelta != targetSize)
        {
            endTrans.sizeDelta = Vector2.MoveTowards(endTrans.sizeDelta, targetSize, TimeManager.instance.deltaTime * 10000.0f);
            yield return null;
        }

        yield return new WaitForSeconds(2.0f);
        endTxt.gameObject.SetActive(false);
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

        if (startPhase)
        {
            StartCoroutine(ShowStart());
            AudioManager.instance.PlaySE("StartSE");
            GameManager.instance.StartGame();
        }
        else
        {
            StartCoroutine(ShowEnd());
            AudioManager.instance.PlaySE("TimeUp");
        }
    }
}
