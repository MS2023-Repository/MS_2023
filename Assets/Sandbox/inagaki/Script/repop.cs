using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class repop : MonoBehaviour
{
    // Start is called before the first frame update
    private GameObject _child;

    void Start()
    {
        _child = transform.Find("item").gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            _child.SetActive(true);
            this.gameObject.transform.DetachChildren();

            Destroy(gameObject, 0.01f);
        }
    }

}
