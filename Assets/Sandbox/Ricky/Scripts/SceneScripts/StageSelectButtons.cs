using System.Collections;
using System.Collections.Generic;
using OutGame.TimeManager;
using UnityEngine;

public class StageSelectButtons : MonoBehaviour
{
    private float progressBar;

    private float playerCount;

    private float t;

    private Material mat;

    // Start is called before the first frame update
    void Start()
    {
        progressBar = -5;

        playerCount = 0;

        t = 0;
        mat = transform.GetChild(1).GetComponent<Renderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerCount == 2)
        {
            t += Time.deltaTime;
        }
        else
        {
            t = 0;
        }

        t = Mathf.Clamp(t, 0, 2);

        progressBar = Mathf.Lerp(-5, 5, t / 2);

        mat.SetFloat("_FillRate", progressBar);
    }

    private void OnTriggerEnter(Collider other) 
    {
        if (other.gameObject.tag == "PlayerObj")
        {
            playerCount++;
        }
    }

    private void OnTriggerExit(Collider other) 
    {
        if (other.gameObject.tag == "PlayerObj")
        {
            playerCount--;
        }
    }
}