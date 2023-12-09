using System.Collections;
using System.Collections.Generic;
using OutGame.GameManager;
using OutGame.TimeManager;
using UnityEngine;
using UnityEngine.UI;

public class ResultScript : MonoBehaviour
{
    private RenderTexture resultTexture;
    [SerializeField] private Camera resultCamera;
    [SerializeField] private RawImage playerImage;
    [SerializeField] private GameObject panel;

    [SerializeField] private ResultPlayerMovement resultPlayerSc;
    [SerializeField] private Transform spawnPoint;

    [SerializeField] private GameObject rightConfetti;
    [SerializeField] private GameObject leftConfetti;

    [SerializeField] private GameObject blurPanel;
    private Material blurObj;
    private float blurT;

    private bool startSpawn;

    // Start is called before the first frame update
    void Start()
    {
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
    }

    // Update is called once per frame
    void Update()
    {
        if (!GameManager.instance.isInGame())
        {
            if (blurT == 1.5f)
            {
                panel.SetActive(true);
                if (resultPlayerSc.reachedPos)
                {
                    if (!startSpawn)
                    {
                        StartCoroutine(SpawnFoods());
                        startSpawn = true;
                    }
                }
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
        }
    }

    private void OnGUI()
    {
        if (resultCamera.targetTexture != null)
        {
            resultCamera.targetTexture.Release();
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
            resultTexture = null;
        }
    }

    IEnumerator SpawnFoods()
    {
        for (int i = 0; i < GameManager.instance.collectedItems.Count; i++)
        {
            var spawnedObj = GameManager.instance.collectedItems[i];
            spawnedObj.transform.parent = spawnPoint.transform;
            spawnedObj.transform.localPosition = Vector3.zero;

            spawnedObj.GetComponent<Rigidbody>().useGravity = true;
            spawnedObj.transform.localScale *= 1.2f;

            yield return new WaitForSeconds(0.5f);
        }

        rightConfetti.SetActive(true);
        leftConfetti.SetActive(true);
    }
}
