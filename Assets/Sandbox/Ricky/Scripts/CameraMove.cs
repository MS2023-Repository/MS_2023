using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    private GameObject playerObj;
    private Vector3 dir;

    // Start is called before the first frame update
    void Start()
    {
        playerObj = GameObject.FindGameObjectWithTag("Player");

        dir = new Vector3(0, 0.2f, -0.6f);
        dir *= 10.0f;
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = playerObj.transform.position + dir;
        transform.LookAt(playerObj.transform);
    }
}