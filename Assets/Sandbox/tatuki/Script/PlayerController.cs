using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace InGame.Player
{
    public class PlayerController : MonoBehaviour
    {
        //スクリプト
        PlayerMove _PlayerMoveScript;
        HandPos[] _HandPosScript = new HandPos[2];
        HeadIK[] _HeadIKscript = new HeadIK[2];
        PlayerAnim[] _PlayerAnim = new PlayerAnim[2];

        //右スティックを倒しているか
        bool _IsRightStickTrigger;

        //プレイヤーのGameObject
        [SerializeField] private GameObject[] _Player;

        //腕のモーション
        [SerializeField] private float _HandHeightRange;
        [SerializeField] private float _InitHandHeight;
        [SerializeField] private float _InitHandWidth;
        [SerializeField] private float _MaxDistance;
        [SerializeField] private float _MinDistance;
        [Range(0, 1)] [SerializeField] private float _CreakHandRange;

        public Vector2 rightStickP1 { get; private set; }
        public Vector2 rightStickP2 { get; private set; }

        private float _BeforeLeftTriggerValue;
        private float _BeforeRightTriggerValue;

        //食べるモーション
        [SerializeField] private float _ResetSpeedRotateHead;
        [SerializeField] private float _SpeedScaleHead;
        [SerializeField] private Vector3 _ScaleUpHead;

        // Start is called before the first frame update
        void Start()
        {
            _IsRightStickTrigger = false;
            _PlayerMoveScript = GetComponent<PlayerMove>();
            
            rightStickP1 = Vector2.zero;
            rightStickP2 = Vector2.zero;

            _BeforeLeftTriggerValue = 0.0f;
            _BeforeRightTriggerValue = 0.0f;

            _HandPosScript[0] = _Player[0].GetComponent<HandPos>();
            _HandPosScript[1] = _Player[1].GetComponent<HandPos>();
            _HeadIKscript[0] = _Player[0].GetComponent<HeadIK>();
            _HeadIKscript[1] = _Player[1].GetComponent<HeadIK>();
            _PlayerAnim[0] = _Player[0].GetComponent<PlayerAnim>();
            _PlayerAnim[1] = _Player[1].GetComponent<PlayerAnim>();

            _HeadIKscript[0].InitChangeScaleNeckNum(_ScaleUpHead, _SpeedScaleHead);
            _HeadIKscript[1].InitChangeScaleNeckNum(_ScaleUpHead, _SpeedScaleHead);
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            // コントローラー操作
            // Gamepad.all で接続されているすべてのゲームパッドを列挙できる
            // TextObjects の数以上の情報は載せられないので、少ない方の数で for する
            for (int i = 0; i < Gamepad.all.Count; i++)
            {
                var gamepad = Gamepad.all[i];

                if (Gamepad.all.Count == 1)
                {
                    i = 1;
                }

                // 操作されたボタンなどの情報を取得
                var leftStickValue = gamepad.leftStick.ReadValue();
                var rightStickValue = gamepad.rightStick.ReadValue();
                var dpadValue = gamepad.dpad.ReadValue();

                if (i == 0)
                {
                    rightStickP1 = rightStickValue;
                }
                else if (i == 1)
                {
                    rightStickP2 = rightStickValue;
                }

                //左スティック
                if (leftStickValue.magnitude > 0f)
                {
                    //歩くときSE

                    _PlayerMoveScript.MoveLStick(i, leftStickValue.normalized);
                    _PlayerAnim[i].SetAnimWalk(_Player[i].transform.forward, leftStickValue.normalized);
                }
                else
                {
                    SetIdleOrHitBackAnim(i);
                }

                //右スティック
                if (rightStickValue.magnitude > 0f)
                {
                    _IsRightStickTrigger = true;

                    _PlayerAnim[i].StartAnimEat();

                    _HeadIKscript[i].SetNeckRotationY(-GetSignedAngle(
                        _Player[i].transform.forward,
                        new Vector3(rightStickValue.normalized.x,
                                    1,
                                    rightStickValue.normalized.y))
                    );
                }
                else
                {
                    _IsRightStickTrigger = false;

                    _HeadIKscript[i].ResetHeadRotation(_ResetSpeedRotateHead);

                    _PlayerAnim[i].SetAnimSpeedEat();
                }

                var leftTriggerValue = gamepad.leftTrigger.ReadValue();
                var rightTriggerValue = gamepad.rightTrigger.ReadValue();

                _HandPosScript[i].SetLeftHandHeight(_InitHandHeight, _InitHandWidth, leftTriggerValue, _HandHeightRange);
                _HandPosScript[i].SetRightHandHeight(_InitHandHeight, _InitHandWidth, rightTriggerValue, _HandHeightRange);

                if(Mathf.Abs(leftTriggerValue - _BeforeLeftTriggerValue) > _CreakHandRange)
                {
                    //軋むSE
                    float volume = Mathf.Abs(leftTriggerValue - _BeforeLeftTriggerValue);
                }
                if(Mathf.Abs(rightTriggerValue - _BeforeRightTriggerValue) > _CreakHandRange)
                {
                    //軋むSE
                    float volume = Mathf.Abs(rightTriggerValue - _BeforeRightTriggerValue);
                }


                _BeforeLeftTriggerValue = leftTriggerValue;
                _BeforeRightTriggerValue = rightTriggerValue;

            }

            // キーボード操作
            // 現在のキーボード情報
            var current = Keyboard.current;

            // キーボード接続チェック
            if (current == null)
            {
                // キーボードが接続されていないと
                // Keyboard.currentがnullになる
                Debug.Log("noneKeyboard");
                return;
            }

            var aKey = current.aKey;
            var wKey = current.wKey;
            var sKey = current.sKey;
            var dKey = current.dKey;
            var qKey = current.qKey;
            var eKey = current.eKey;

            var jKey = current.jKey;
            var iKey = current.iKey;
            var kKey = current.kKey;
            var lKey = current.lKey;
            var uKey = current.uKey;
            var oKey = current.oKey;

            var zkey = current.zKey;

            if(zkey.isPressed)
            {
                _HeadIKscript[0].SetNeckRotationY(90);
            }


            if (Gamepad.all.Count < 2)
            {
                if (aKey.isPressed)
                {
                    _PlayerMoveScript.MoveKeyboard(0, PlayerMove.MoveDirection.Left);
                }
                if (wKey.isPressed)
                {
                    _PlayerMoveScript.MoveKeyboard(0, PlayerMove.MoveDirection.Forward);
                }
                if (sKey.isPressed)
                {
                    _PlayerMoveScript.MoveKeyboard(0, PlayerMove.MoveDirection.Back);
                }
                if (dKey.isPressed)
                {
                    _PlayerMoveScript.MoveKeyboard(0, PlayerMove.MoveDirection.Right);
                }

                if (qKey.isPressed)
                { 
                    _HandPosScript[0].SetLeftHandHeight(_InitHandHeight, _InitHandWidth, 1.0f, _HandHeightRange);
                }
                else
                {
                    _HandPosScript[0].SetLeftHandHeight(_InitHandHeight, _InitHandWidth, 0.0f, _HandHeightRange);
                }

                if (eKey.isPressed)
                {
                    _HandPosScript[0].SetRightHandHeight(_InitHandHeight, _InitHandWidth, 1.0f, _HandHeightRange);
                }
                else
                {
                    _HandPosScript[0].SetRightHandHeight(_InitHandHeight, _InitHandWidth, 0.0f, _HandHeightRange);

                }
            }

            if (Gamepad.all.Count < 1)
            {

                if (jKey.isPressed)
                {
                    _PlayerMoveScript.MoveKeyboard(1, PlayerMove.MoveDirection.Left);
                }
                if (iKey.isPressed)
                {
                    _PlayerMoveScript.MoveKeyboard(1, PlayerMove.MoveDirection.Forward);
                }
                if (kKey.isPressed)
                {
                    _PlayerMoveScript.MoveKeyboard(1, PlayerMove.MoveDirection.Back);
                }
                if (lKey.isPressed)
                {
                    _PlayerMoveScript.MoveKeyboard(1, PlayerMove.MoveDirection.Right);
                }

                if (uKey.isPressed)
                {
                    _HandPosScript[1].SetLeftHandHeight(_InitHandHeight, _InitHandWidth, 1.0f, _HandHeightRange);
                }
                else
                {
                    _HandPosScript[1].SetLeftHandHeight(_InitHandHeight, _InitHandWidth, 0.0f, _HandHeightRange);
                }

                if (oKey.isPressed)
                {
                    _HandPosScript[1].SetRightHandHeight(_InitHandHeight, _InitHandWidth, 1.0f, _HandHeightRange);
                }
                else
                { 
                    _HandPosScript[1].SetRightHandHeight(_InitHandHeight, _InitHandWidth, 0.0f, _HandHeightRange);
                }
            }

            _PlayerMoveScript.PlayerPosCorrection(_MinDistance, _MaxDistance);

        }

        //空間上のベクトルを平面のベクトルに変換後
        //そのベクトルの為す角度を返す
        float GetSignedAngle(Vector3 from,Vector3 to)
        {
            // 平面の法線ベクトル（上向きベクトルとする）
            var planeNormal = Vector3.up;

            // 平面に投影されたベクトルを求める
            var planeFrom = Vector3.ProjectOnPlane(from, planeNormal);
            var planeTo = Vector3.ProjectOnPlane(to, planeNormal);

            // 平面に投影されたベクトル同士の符号付き角度
            // 時計回りで正、反時計回りで負
            var signedAngle = Vector3.SignedAngle(planeFrom, planeTo, planeNormal);
            return signedAngle;
        }

        public bool GetISRightStickTrigger()
        {
            return _IsRightStickTrigger;
        }

        public void SetIdleOrHitBackAnim(int i)
        {
            if (i == 0)
            {
                if (_PlayerAnim[1].GetWalkAnimID() != PlayerAnim.WalkAnimID.Idle &&
                    _PlayerAnim[1].GetWalkAnimID() != PlayerAnim.WalkAnimID.HitBack)
                {
                    _PlayerAnim[i].SetHitBackAnimWalk();
                }
                else
                {
                    _PlayerAnim[i].SetIdleAnimWalk();
                }
            }
            else if (i == 1)
            {
                if (_PlayerAnim[0].GetWalkAnimID() != PlayerAnim.WalkAnimID.Idle &&
                    _PlayerAnim[1].GetWalkAnimID() != PlayerAnim.WalkAnimID.HitBack)
                {
                    _PlayerAnim[i].SetHitBackAnimWalk();
                }
                else
                {
                    _PlayerAnim[i].SetIdleAnimWalk();
                }
            }
        }
    }
}
