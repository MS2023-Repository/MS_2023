using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            GetComponent<Rigidbody>().AddForce(new Vector3(-1, 0, 0));
        }
        if (Input.GetKey(KeyCode.D))
        {
            GetComponent<Rigidbody>().AddForce(new Vector3(1, 0, 0));
        }
        if (Input.GetKey(KeyCode.W))
        {
            GetComponent<Rigidbody>().AddForce(new Vector3(0, 0, 1));
        }
        if (Input.GetKey(KeyCode.S))
        {
            GetComponent<Rigidbody>().AddForce(new Vector3(0, 0, -1));
        }
    }
}
