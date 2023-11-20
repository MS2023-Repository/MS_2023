using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultScript : MonoBehaviour
{
    private RenderTexture resultTexture;
    [SerializeField] private Camera resultCamera;
    [SerializeField] private RawImage playerImage;
    [SerializeField] private GameObject panel;

    // Start is called before the first frame update
    void Start()
    {
        panel.SetActive(false);
        playerImage.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
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
}
