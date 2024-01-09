using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLOSScript : MonoBehaviour
{
    private GameObject playerObj;

    // Start is called before the first frame update
    void Start()
    {
        playerObj = GameObject.FindGameObjectWithTag("Box");
    }

    // Update is called once per frame
    void Update()
    {
        var objHit = Physics.RaycastAll(this.transform.position, playerObj.transform.position - this.transform.position, 100.0f);

        if (objHit.Length > 0)
        {
            foreach(var hit in objHit)
            {
                hit.transform.gameObject.GetComponent<Material>().color = new Color(1, 1, 1, 0);
            }
        }
    }
}
