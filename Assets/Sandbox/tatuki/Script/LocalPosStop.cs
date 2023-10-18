using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InGame.Player
{

    public class LocalPosStop : MonoBehaviour
    {
        private Vector3 initLocalPos;

        // Start is called before the first frame update
        void Start()
        {
            initLocalPos = transform.localPosition;
        }

        // Update is called once per frame
        void Update()
        {
            transform.localPosition = initLocalPos;
        }
    }
}
