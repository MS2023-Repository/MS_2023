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

        public void MoveLStick(int playerNum, Vector2 stickValue)
        {
            Vector3 tempPos;
            tempPos = player[playerNum].transform.position;
            tempPos.x += stickValue.x * Time.deltaTime;
            tempPos.z += stickValue.y * Time.deltaTime;
            player[playerNum].transform.position = tempPos;
        }

        public void MoveKeyboard(int playerNum, MoveDirection direction)
        {
            Vector3 tempPos;
            tempPos = player[playerNum].transform.position;

            switch (direction)
            {
                case MoveDirection.Left:
                    tempPos.x -= Time.deltaTime;
                    break;
                case MoveDirection.Right:
                    tempPos.x += Time.deltaTime;
                    break;
                case MoveDirection.Forward:
                    tempPos.z += Time.deltaTime;
                    break;
                case MoveDirection.Back:
                    tempPos.z -= Time.deltaTime;
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
                player[0].transform.position += vec1 * Time.deltaTime;
                player[1].transform.position += vec2 * Time.deltaTime;
            }
            else if(distancePlayer < minDistance)
            {
                player[0].transform.position -= vec1 * Time.deltaTime;
                player[1].transform.position -= vec2 * Time.deltaTime;
            }
        }
    }
}