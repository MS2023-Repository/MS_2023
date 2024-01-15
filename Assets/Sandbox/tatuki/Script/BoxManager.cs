using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InGame.Player
{
    public class BoxManager : MonoBehaviour
    {
        [SerializeField] float _RotateX;
        [SerializeField] float _RotateZ;
        [SerializeField] float _InitHeight;

        PlayerController _PlayerControllerScript;

        Transform _Player1;
        Transform _Player2;

        // Start is called before the first frame update
        void Start()
        {
            _PlayerControllerScript = transform.parent.GetComponent<PlayerController>();
            _Player1 = GameObject.Find("Player1").transform;
            _Player2 = GameObject.Find("Player2").transform;
        }

        // Update is called once per frame
        void Update()
        {
            PositionBox();
            RotateBox();
        }

        public void RotateBox()
        {
            float rotateX = -(_PlayerControllerScript.GetLeftTriggerValue(1) * _RotateX) + _PlayerControllerScript.GetRightTriggerValue(1) * _RotateX
                + _PlayerControllerScript.GetLeftTriggerValue(0) * _RotateX + -(_PlayerControllerScript.GetRightTriggerValue(0) * _RotateX);
            float rotateZ = _PlayerControllerScript.GetLeftTriggerValue(1) * _RotateZ + _PlayerControllerScript.GetRightTriggerValue(1) * _RotateZ
                + -(_PlayerControllerScript.GetLeftTriggerValue(0) * _RotateZ + _PlayerControllerScript.GetRightTriggerValue(0) * _RotateZ);

            transform.LookAt(_Player1);
            transform.Rotate(0, -90, 0);
                                        
            transform.localEulerAngles = new Vector3(rotateX, transform.localEulerAngles.y, rotateZ);

            float DistanceXZ = Vector2.Distance(
                new Vector2(_Player1.position.x, _Player1.position.z),
                new Vector2(_Player2.position.x, _Player2.position.z)
                );
            float DistanceY = _Player1.position.y - _Player2.position.y;

            float rad = Mathf.Atan2(DistanceY, DistanceXZ);
            float degree = rad * Mathf.Rad2Deg;

            Vector3 Rot = transform.localEulerAngles;

            transform.localEulerAngles = new Vector3(Rot.x, Rot.y, Rot.z + degree);

            //float c = Mathf.Sqrt((a * a) + (b * b));

            //Debug.Log(Mathf.Acos(0.5f/*((a * a) + (c * c) - (b * b)) / (2 * a * c))*/));
        }

        public void PositionBox()
        {
            float posX = (_Player1.position.x + _Player2.position.x) / 2;
            float posY = (_Player1.position.y + _Player2.position.y) / 2;
            float posZ = (_Player1.position.z + _Player2.position.z) / 2;

            transform.position = new Vector3(posX,posY + _InitHeight,posZ);
        }
    }
}
