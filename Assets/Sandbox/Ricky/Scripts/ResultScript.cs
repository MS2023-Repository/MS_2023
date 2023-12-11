using System.Collections;
using System.Collections.Generic;
using InGame.CollectibleItem;
using OutGame.GameManager;
using OutGame.InputManager;
using OutGame.SceneManager;
using OutGame.TimeManager;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ResultScript : MonoBehaviour
{
    enum ANIMSTATE
    {
        BLUR,
        BACKGROUND,
        PLAYERMOVE,
        SCORECOUNT,
        MENU
    }

    private ANIMSTATE animState;

    private RenderTexture resultTexture;
    [SerializeField] private Camera resultCamera;
    [SerializeField] private RawImage playerImage;
    [SerializeField] private GameObject panel;

    [SerializeField] private ResultPlayerMovement resultPlayerSc;
    [SerializeField] private Transform spawnPoint;

    [SerializeField] private GameObject rightConfetti;
    [SerializeField] private GameObject leftConfetti;

    [SerializeField] private GameObject resultBackground;
    private Vector3 resultBackgroundTarget;

    [SerializeField] private GameObject testObj;

    [SerializeField] private GameObject blurPanel;
    private Material blurObj;
    private float blurT;

    enum MENUSTATE
    {
        SELECT,
        NEXT
    }    

    [SerializeField] private TextMeshProUGUI selectTxt;
    [SerializeField] private TextMeshProUGUI nextTxt;
    private MENUSTATE selectedMenu;
    private float menuT;
    private bool increaseTime;
    private Color32 invisColor, visibleColor;

    private bool startSpawn;

    // Start is called before the first frame update
    void Start()
    {
        animState = ANIMSTATE.BLUR;

        panel.SetActive(false);
        playerImage.gameObject.SetActive(true);
        startSpawn = false;

        rightConfetti.SetActive(false);
        leftConfetti.SetActive(false);

        if (GameObject.Find("BlurPanel") == null)
        {
            blurObj = Instantiate(blurPanel, Camera.main.transform).GetComponent<Renderer>().materials[0];
        }
        else
        {
            blurObj = GameObject.Find("BlurPanel").GetComponent<Renderer>().materials[0];
        }

        blurT = 0;

        resultBackgroundTarget = new Vector3(-300, 85, 0);
        resultBackground.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(resultBackgroundTarget.x, resultBackgroundTarget.y + 1000, resultBackgroundTarget.z);

        selectedMenu = MENUSTATE.NEXT;
        menuT = 1;
        increaseTime = false;
        invisColor = new Color32(255, 255, 255, 0);
        visibleColor = new Color32(255, 255, 255, 255);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.isEndGame())
        {
            switch (animState)
            {
                case ANIMSTATE.BLUR:
                    if (blurT == 1.5f)
                    {
                        panel.SetActive(true);
                        animState = ANIMSTATE.BACKGROUND;
                    }
                    else
                    {
                        blurT += TimeManager.instance.unscaledDeltaTime;

                        var ti = blurT / 1.5f;
                        ti = ti * ti * (3f - 2f * ti);
                        var valueToSet = Mathf.Lerp(0, 0.15f, ti);
                        blurObj.SetFloat("_BlurX", valueToSet);
                        blurObj.SetFloat("_BlurY", valueToSet);

                        blurT = Mathf.Clamp(blurT, 0, 1.5f);
                    }
                    break;
                case ANIMSTATE.BACKGROUND:
                    if (resultBackground.GetComponent<RectTransform>().anchoredPosition3D == resultBackgroundTarget)
                    {
                        animState = ANIMSTATE.PLAYERMOVE;
                        resultPlayerSc.StartMove();
                        transform.GetChild(0).GetChild(1).SetParent(null);
                    }
                    else
                    {
                        resultBackground.GetComponent<RectTransform>().anchoredPosition3D = Vector3.MoveTowards(
                            resultBackground.GetComponent<RectTransform>().anchoredPosition3D, resultBackgroundTarget, TimeManager.instance.unscaledDeltaTime * 500);
                    }
                    break;
                case ANIMSTATE.PLAYERMOVE:
                    if (resultPlayerSc.reachedPos)
                    {
                        animState = ANIMSTATE.SCORECOUNT;
                    }
                    break;
                case ANIMSTATE.SCORECOUNT:
                    if (!startSpawn)
                    {
                        StartCoroutine(SpawnFoods());
                        startSpawn = true;
                    }
                    break;
                case ANIMSTATE.MENU:
                    var menuDir = InputManager.instance.GetMenuMovement();

                    if (menuDir.x > 0)
                    {
                        switch (selectedMenu)
                        {
                            case MENUSTATE.SELECT:
                                selectedMenu = MENUSTATE.NEXT;
                                menuT = 1;
                                break;
                            case MENUSTATE.NEXT:
                                break;
                        }
                    }
                    else if (menuDir.x < 0)
                    {
                        switch (selectedMenu)
                        {
                            case MENUSTATE.NEXT:
                                selectedMenu = MENUSTATE.SELECT;
                                menuT = 1;
                                break;
                            case MENUSTATE.SELECT:
                                break;
                        }
                    }

                    if (increaseTime)
                    {
                        menuT += TimeManager.instance.unscaledDeltaTime;
                    }
                    else
                    {
                        menuT -= TimeManager.instance.unscaledDeltaTime;
                    }

                    if (menuT >= 1)
                    {
                        increaseTime = false;
                    }
                    else if (menuT < 0)
                    {
                        increaseTime = true;
                    }

                    menuT = Mathf.Clamp01(menuT);

                    var t = menuT / 1.5f;
                    t = t * t * (3f - 2f * t);

                    switch (selectedMenu)
                    {
                        case MENUSTATE.SELECT:
                            selectTxt.faceColor = Color32.Lerp(invisColor, visibleColor, t);
                            nextTxt.faceColor = visibleColor;
                            break;
                        case MENUSTATE.NEXT:
                            nextTxt.faceColor = Color32.Lerp(invisColor, visibleColor, t);
                            selectTxt.faceColor = visibleColor;
                            break;
                    }

                    if (InputManager.instance.menuSelectedState)
                    {
                        switch (selectedMenu)
                        {
                            case MENUSTATE.SELECT:
                                SceneLoader.instance.LoadScene("StageSelect");
                                break;
                            case MENUSTATE.NEXT:
                                SceneLoader.instance.ReloadScene();
                                break;
                        }
                    }

                    break;
            }
        }
    }

    private void OnGUI()
    {
        if (resultCamera.targetTexture != null)
        {
            resultCamera.targetTexture.Release();
            resultCamera.targetTexture.DiscardContents();
            resultCamera.targetTexture = null;
        }

        resultTexture = new RenderTexture(1920, 1080, 1);
        resultCamera.targetTexture = resultTexture;
        playerImage.texture = resultTexture;
    }

    private void OnDisable() 
    {
        if (resultTexture != null)
        {
            resultCamera.targetTexture = null;
            playerImage.texture = null;
            resultTexture.Release();
            resultTexture.DiscardContents();
            resultTexture = null;
        }
    }

    IEnumerator SpawnFoods()
    {
        for (int i = 0; i < GameManager.instance.collectedItems.Count; i++)
        {
            var spawnedObj = Instantiate(testObj);
            //Destroy(spawnedObj.GetComponent<CollectibleItem>());
            spawnedObj.transform.position = spawnPoint.position;

            spawnedObj.GetComponent<Rigidbody>().useGravity = true;
            spawnedObj.GetComponent<Rigidbody>().velocity = Vector3.zero;
            spawnedObj.GetComponent<Rigidbody>().drag = 20f;

            spawnedObj.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);

            yield return new WaitForSeconds(0.5f);
        }

        selectTxt.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        nextTxt.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.25f);

        animState = ANIMSTATE.MENU;
    }
}