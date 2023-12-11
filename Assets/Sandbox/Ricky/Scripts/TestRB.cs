using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestRB : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody>().AddForce(0, 5, 0, ForceMode.Impulse);       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
