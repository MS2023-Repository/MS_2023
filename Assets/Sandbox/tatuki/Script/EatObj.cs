using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InGame.Player
{
    public class EatObj : MonoBehaviour
    {
        [SerializeField] HeadIK HeadIKScript;

        // Update is called once per frame
        void Update()
        {
            transform.localRotation = Quaternion.Euler(0, -HeadIKScript.GetNeckAngleY(), 0);
        }
    }
}
