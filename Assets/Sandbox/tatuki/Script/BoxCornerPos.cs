using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InGame.Box
{
    public class BoxCornerPos : MonoBehaviour
    {
        [SerializeField] private GameObject _Player1LeftBoxCorner;
        [SerializeField] private GameObject _Player1RightBoxCorner;
        [SerializeField] private GameObject _Player2LeftBoxCorner;
        [SerializeField] private GameObject _Player2RightBoxCorner;

        [SerializeField] private GameObject _Player1LeftHand_Pos;
        [SerializeField] private GameObject _Player1RightHand_Pos;
        [SerializeField] private GameObject _Player2LeftHand_Pos;
        [SerializeField] private GameObject _Player2RightHand_Pos;

        [SerializeField] private float _MoveSpeed;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            
            //Vector3 vecLeftHand = (_Player1LeftHand_Pos.transform.position - _Player1LeftBoxCorner.transform.position).normalized;
            //Vector3 vecRightHand = (_Player1RightHand_Pos.transform.position - _Player1RightBoxCorner.transform.position).normalized;

            //_Player1LeftBoxCorner.transform.position += vecLeftHand * Time.deltaTime * _MoveSpeed;
            //_Player1RightBoxCorner.transform.position += vecRightHand * Time.deltaTime * _MoveSpeed;

            //vecLeftHand = (_Player2LeftHand_Pos.transform.position - _Player2LeftBoxCorner.transform.position).normalized;
            //vecRightHand = (_Player2RightHand_Pos.transform.position - _Player2RightBoxCorner.transform.position).normalized;

            //_Player2LeftBoxCorner.transform.position += vecLeftHand * Time.deltaTime * _MoveSpeed;
            //_Player2RightBoxCorner.transform.position += vecRightHand * Time.deltaTime * _MoveSpeed;
        }

        public void SetBoxCornerPos()
        {
            _Player1LeftBoxCorner.transform.position = _Player1LeftHand_Pos.transform.position;
            _Player1RightBoxCorner.transform.position = _Player1RightHand_Pos.transform.position;

            _Player2LeftBoxCorner.transform.position = _Player2LeftHand_Pos.transform.position;
            _Player2RightBoxCorner.transform.position = _Player2RightHand_Pos.transform.position;

        }
    }
}
