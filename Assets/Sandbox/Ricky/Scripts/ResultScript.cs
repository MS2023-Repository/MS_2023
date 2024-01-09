using System.Collections;
using System.Collections.Generic;
using InGame.CollectibleItem;
using OutGame.Audio;
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
    [SerializeField] private Image blackPanel;

    [SerializeField] private ResultPlayerMovement resultPlayerSc;
    [SerializeField] private Transform spawnPoint;

    [SerializeField] private GameObject rightConfetti;
    [SerializeField] private GameObject leftConfetti;

    [SerializeField] private GameObject resultBackground;
    private Vector3 resultBackgroundTarget;

    [SerializeField] private GameObject[] fallingFoodObj;

    [SerializeField] private ScoreCounter scoreCounter;

    [SerializeField] private GameObject foodNumObj;

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

        var panelColor = blackPanel.color;
        panelColor.a = 0;
        blackPanel.color = panelColor;

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

        resultBackgroundTarget = new Vector3(-390, 85, 0);
        resultBackground.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(resultBackgroundTarget.x, resultBackgroundTarget.y + 1000, resultBackgroundTarget.z);

        selectedMenu = MENUSTATE.NEXT;
        menuT = 1;
        increaseTime = false;
        invisColor = new Color32(255, 255, 255, 0);
        visibleColor = new Color32(255, 255, 255, 255);

        resultTexture = new RenderTexture(1920, 1080, 1);
        resultCamera.targetTexture = resultTexture;
        playerImage.texture = resultTexture;
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

                        var panelColor = blackPanel.color;
                        panelColor.a = Mathf.Lerp(0, 0.6f, ti);

                        blurT = Mathf.Clamp(blurT, 0, 1.5f);
                    }
                    break;
                case ANIMSTATE.BACKGROUND:
                    if (resultBackground.GetComponent<RectTransform>().anchoredPosition3D == resultBackgroundTarget)
                    {
                        animState = ANIMSTATE.PLAYERMOVE;
                        resultPlayerSc.StartMove();
                        panel.transform.GetChild(1).SetParent(null);
                    }
                    else
                    {
                        resultBackground.GetComponent<RectTransform>().anchoredPosition3D = Vector3.MoveTowards(
                            resultBackground.GetComponent<RectTransform>().anchoredPosition3D, resultBackgroundTarget, TimeManager.instance.unscaledDeltaTime * 1000);
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
                        AudioManager.instance.PlaySE("MoveButton");
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
                        AudioManager.instance.PlaySE("MoveButton");
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
                    else if (menuT < 0.3f)
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
                        AudioManager.instance.PlaySE("SelectButton");

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

    private void OnDisable() 
    {
        if (resultTexture != null)
        {
            if (resultCamera != null)
            {
                if (resultCamera.targetTexture != null)
                {
                    resultCamera.targetTexture = null;
                    Destroy(resultCamera.targetTexture);
                }
            }
            
            playerImage.texture = null;
            resultTexture.Release();
            resultTexture.DiscardContents();
            resultTexture = null;
            Destroy(resultTexture);
        }
    }

    IEnumerator SpawnFoods()
    {
        for (int i = 0; i < GameManager.instance.collectedItems.Count; i++)
        {
            string name = GameManager.instance.collectedItems[i].name;
            GameObject objToSpawn = null;

            if (name.Contains("Berry"))
            {
                objToSpawn = fallingFoodObj[0];
            }
            else if (name.Contains("Cherry"))
            {
                objToSpawn = fallingFoodObj[1];
            }
            else if (name.Contains("Lemon"))
            {
                objToSpawn = fallingFoodObj[2];
            }
            else if (name.Contains("Peach"))
            {
                objToSpawn = fallingFoodObj[3];
            }
            else if (name.Contains("Tomato"))
            {
                objToSpawn = fallingFoodObj[4];
            }
            else
            {
                objToSpawn = fallingFoodObj[4];
            }

            var spawnedObj = Instantiate(objToSpawn);
            spawnedObj.transform.position = new Vector3(spawnPoint.position.x + Random.Range(-0.02f, 0.02f), spawnPoint.position.y, spawnPoint.position.z + Random.Range(-0.02f, 0.02f));

            Vector3 screenPos = resultCamera.WorldToScreenPoint(spawnedObj.transform.position);
            var scoreObj = Instantiate(foodNumObj, panel.transform.GetChild(0));
            scoreObj.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(screenPos.x - 860, screenPos.y - 650, 0);
            var scoreToAdd = GameManager.instance.collectedItems[i].GetComponent<CollectibleItem>().GetScoreNum();
            scoreObj.GetComponent<FoodNumScript>().SetNum(scoreToAdd);
            scoreCounter.AddScore(scoreToAdd);

            spawnedObj.GetComponent<Rigidbody>().useGravity = true;
            spawnedObj.GetComponent<Rigidbody>().velocity = Vector3.zero;
            spawnedObj.GetComponent<Rigidbody>().drag = 20f;

            spawnedObj.transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);

            AudioManager.instance.PlaySE("ScoreCountSE");

            yield return new WaitForSeconds(0.5f);
        }

        yield return new WaitForSeconds(0.5f);

        selectTxt.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        nextTxt.gameObject.SetActive(true);

        animState = ANIMSTATE.MENU;
    }
}