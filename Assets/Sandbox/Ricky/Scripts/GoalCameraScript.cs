using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalCameraScript : MonoBehaviour
{
    private void OnDisable() 
    {
        var cam = this.GetComponent<Camera>();

        if (cam.targetTexture != null)
        {
            cam.targetTexture.Release();
            cam.targetTexture = null;
        }
    }
}
