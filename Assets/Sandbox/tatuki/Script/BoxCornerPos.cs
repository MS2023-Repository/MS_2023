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

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            _Player1LeftBoxCorner.transform.position = new Vector3(0, 0, 0);

            _Player1LeftBoxCorner.transform.position = _Player1LeftHand_Pos.transform.position;
            //_Player1RightBoxCorner.transform.position = _Player1RightHand_Pos.transform.position;
            //_Player2LeftBoxCorner.transform.position = _Player2LeftHand_Pos.transform.position;
            //_Player2RightBoxCorner.transform.position = _Player2RightHand_Pos.transform.position;
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
