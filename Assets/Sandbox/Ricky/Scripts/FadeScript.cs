using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeScript : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] private Image image;
    private readonly int circleSizeId = Shader.PropertyToID("_CircleSize");

    public float circleSize = 0;

    public bool fadeOutState { get; private set; }
    public bool fadeInState { get; private set; }

    public void PlayFadeOut()
    {
        fadeOutState = true;
        anim.SetBool("FadeOutFlg", true);
    }

    public void PlayFadeIn()
    {
        fadeInState = true;
        anim.SetBool("FadeInFlg", true);
    }

    public void FinishedFadeOut()
    {
        fadeOutState = false;
        anim.SetBool("FadeOutFlg", false);
    }

    public void FinishedFadeIn()
    {
        fadeInState = false;
        anim.SetBool("FadeInFlg", false);
    }

    // Start is called before the first frame update
    void Start()
    {
        fadeOutState = false;
        fadeInState = false;
    }

    // Update is called once per frame
    void Update()
    {
        image.materialForRendering.SetFloat(circleSizeId, circleSize);
    }
}