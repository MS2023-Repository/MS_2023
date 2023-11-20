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

        resultCamera.targetTexture = new RenderTexture(1920, 1080, 1);
        resultTexture = resultCamera.targetTexture;
        playerImage.texture = resultTexture;
    }
}
