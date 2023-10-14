using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InGame.Player
{
    public class PlayerRotate : MonoBehaviour
    {
        public GameObject Box;

        public GameObject Player;

        private Vector3 rotate;

        [SerializeField]
        private bool forward;

        // Update is called once per frame
        void Update()
        {
            rotate = Box.transform.right;

            if (!forward)
            {
                rotate *= -1;
            }

            Player.transform.forward = rotate;
        }
    }
}
