using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

namespace InGame.Player
{
    public class PlayerMove : MonoBehaviour
    {
        public enum MoveDirection
        {
            Left,
            Right,
            Forward,
            Back,
        }

        //プレイヤーオブジェクト
        [SerializeField] private GameObject[] player;

        [SerializeField] private float _NomalSpeed;
        [SerializeField] private float _FatSpeed;
        [SerializeField] private float _ThinSpeed;

        private PlayerBodyShape[] _PlayerBodyShapeScript = new PlayerBodyShape[2];
        private float _Speed;

        private void Start()
        {
            _Speed = _NomalSpeed;
            _PlayerBodyShapeScript[0] = player[0].GetComponent<PlayerBodyShape>();
            _PlayerBodyShapeScript[1] = player[1].GetComponent<PlayerBodyShape>();
        }

        public void MoveLStick(int playerNum, Vector2 stickValue)
        {
            if(_PlayerBodyShapeScript[playerNum].GetBodyShapeType()
                == PlayerBodyShape.BodyShapeType.Nomal)
            {
                _Speed = _NomalSpeed;
            }
            else if(_PlayerBodyShapeScript[playerNum].GetBodyShapeType()
                == PlayerBodyShape.BodyShapeType.Thin)
            {
                _Speed = _ThinSpeed;
            }
            else if(_PlayerBodyShapeScript[playerNum].GetBodyShapeType()
                == PlayerBodyShape.BodyShapeType.Fat)
            {
                _Speed = _FatSpeed;
            }

            Vector3 tempPos;
            tempPos = player[playerNum].transform.position;
            tempPos.x += stickValue.x * Time.deltaTime * _Speed;
            tempPos.z += stickValue.y * Time.deltaTime * _Speed;
            player[playerNum].transform.position = tempPos;
        }

        public void MoveKeyboard(int playerNum, MoveDirection direction)
        {
            if (_PlayerBodyShapeScript[playerNum].GetBodyShapeType()
                == PlayerBodyShape.BodyShapeType.Nomal)
            {
                _Speed = _NomalSpeed;
            }
            else if (_PlayerBodyShapeScript[playerNum].GetBodyShapeType()
                == PlayerBodyShape.BodyShapeType.Thin)
            {
                _Speed = _ThinSpeed;
            }
            else if (_PlayerBodyShapeScript[playerNum].GetBodyShapeType()
                == PlayerBodyShape.BodyShapeType.Fat)
            {
                _Speed = _FatSpeed;
            }

            Vector3 tempPos;
            tempPos = player[playerNum].transform.position;

            switch (direction)
            {
                case MoveDirection.Left:
                    tempPos.x -= Time.deltaTime * _Speed;
                    break;
                case MoveDirection.Right:
                    tempPos.x += Time.deltaTime * _Speed;
                    break;
                case MoveDirection.Forward:
                    tempPos.z += Time.deltaTime * _Speed;
                    break;
                case MoveDirection.Back:
                    tempPos.z -= Time.deltaTime * _Speed;
                    break;
            }
            player[playerNum].transform.position = tempPos;
        }


        public void PlayerPosCorrection(float minDistance,float maxDistance)
        {
            Vector3 posPl1 = player[0].transform.position;
            Vector3 posPl2 = player[1].transform.position;

            float distancePlayer = Vector3.Distance(posPl1, posPl2);

            Vector3 vec1 = (posPl2 - posPl1).normalized;
            Vector3 vec2 = (posPl1 - posPl2).normalized;

            if (distancePlayer > maxDistance)
            {
                player[0].transform.position += vec1 * Time.deltaTime * _Speed;
                player[1].transform.position += vec2 * Time.deltaTime * _Speed;
            }
            else if(distancePlayer < minDistance)
            {
                player[0].transform.position -= vec1 * Time.deltaTime * _Speed;
                player[1].transform.position -= vec2 * Time.deltaTime * _Speed;
            }
        }
    }
}