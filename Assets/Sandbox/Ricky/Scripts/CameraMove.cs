using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    [SerializeField] private float distance = 5.0f;

    private GameObject playerObj;
    private Vector3 dir;

    // Start is called before the first frame update
    void Start()
    {
        playerObj = GameObject.FindGameObjectWithTag("Box");

        dir = new Vector3(0, 0.2f, -0.6f);
        dir *= distance;
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = playerObj.transform.position + dir;
        transform.LookAt(playerObj.transform);
    }
}