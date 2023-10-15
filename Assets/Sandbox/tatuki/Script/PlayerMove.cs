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
            Vector3 Pos;

            // コントローラー操作
            // Gamepad.all で接続されているすべてのゲームパッドを列挙できる
            // TextObjects の数以上の情報は載せられないので、少ない方の数で for する
            for (int i = 0; i < Gamepad.all.Count; i++)
            {
                //Debug.Log(Gamepad.all.Count);
                var gamepad = Gamepad.all[i];

                if (Gamepad.all.Count == 1)
                {
                    i = 1;
                    //Debug.Log(i);
                }

                // 操作されたボタンなどの情報を取得
                var leftStickValue = gamepad.leftStick.ReadValue();
                var rightStickValue = gamepad.rightStick.ReadValue();
                var dpadValue = gamepad.dpad.ReadValue();

                Pos = player[i].transform.position;

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

            // キーボード操作
            // 現在のキーボード情報
            var current = Keyboard.current;

            // キーボード接続チェック
            if (current == null)
            {
                // キーボードが接続されていないと
                // Keyboard.currentがnullになる
                return;
            }

            Pos = player[0].transform.position;

            var aKey = current.aKey;
            var wKey = current.wKey;
            var sKey = current.sKey;
            var dKey = current.dKey;

            if (aKey.isPressed)
            {
                Pos.x -= Time.deltaTime;
            }
            if (wKey.isPressed)
            {
                Pos.z += Time.deltaTime;
            }
            if (sKey.isPressed)
            {
                Pos.z -= Time.deltaTime;
            }
            if (dKey.isPressed)
            {
                Pos.x += Time.deltaTime;
            }

            player[0].transform.position = Pos;
        }
    }
}