using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : MonoBehaviour
{
    [SerializeField] GameObject[] managerList;

    private void Start()
    {
        foreach (GameObject managerObj in managerList)
        {
            if (GameObject.Find(managerObj.name) == null)
            {
                Instantiate(managerObj, this.transform);
            }
            else
            {
                GameObject.Find(managerObj.name).transform.parent = this.transform;
            }
        }
    }
}