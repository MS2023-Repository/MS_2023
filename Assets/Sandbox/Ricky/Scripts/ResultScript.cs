using System.Collections;
using System.Collections.Generic;
using OutGame.GameManager;
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

    private bool startSpawn;

    // Start is called before the first frame update
    void Start()
    {
        panel.SetActive(false);
        playerImage.gameObject.SetActive(true);
        startSpawn = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (resultPlayerSc.reachedPos)
        {
            if (!startSpawn)
            {
                StartCoroutine(SpawnFoods());
                startSpawn = true;
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
            var spawnedObj = Instantiate(GameManager.instance.collectedItems[i]);
            spawnedObj.transform.position = spawnPoint.transform.position;

            yield return new WaitForSeconds(0.5f);
        }
    }
}
