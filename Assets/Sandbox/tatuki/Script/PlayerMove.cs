using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

namespace InGame.Player
{

    public class PlayerMove : MonoBehaviour
    {
        //プレイヤーオブジェクト
        [SerializeField] private GameObject[] player;

        // 更新はフレームごとに1回呼び出されます
        void Update()
        {

            // Gamepad.all で接続されているすべてのゲームパッドを列挙できる
            // TextObjects の数以上の情報は載せられないので、少ない方の数で for する
            for (int i = 0; i < Gamepad.all.Count; i++)
            {
                var gamepad = Gamepad.all[i];

                // 操作されたボタンなどの情報を取得
                var leftStickValue = gamepad.leftStick.ReadValue();
                var rightStickValue = gamepad.rightStick.ReadValue();
                var dpadValue = gamepad.dpad.ReadValue();

                Vector3 Pos = player[i].transform.position;

                //左スティック
                if (leftStickValue.magnitude > 0f)
                {
                    Vector2 stickValue = leftStickValue.normalized;
                    Pos.x += stickValue.x * Time.deltaTime;
                    Pos.z += stickValue.y * Time.deltaTime;
                    player[i].transform.position = Pos;
                }

                var leftTriggerValue = gamepad.leftTrigger.ReadValue();
                var rightTriggerValue = gamepad.rightTrigger.ReadValue();

                if (leftTriggerValue > 0 || rightTriggerValue > 0)
                {

                }
            }
        }
    }
}