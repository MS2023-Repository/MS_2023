using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace InGame.Player
{
    public class PlayerController : MonoBehaviour
    {
        //�X�N���v�g
        PlayerMove _PlayerMoveScript;
        HandPos[] _HandPosScript = new HandPos[2];
        HeadIK[] _HeadIKscript = new HeadIK[2];
        PlayerAnim[] _PlayerAnim = new PlayerAnim[2];

        //�E�X�e�B�b�N��|���Ă��邩
        bool _IsRightStickTrigger;

        //�v���C���[��GameObject
        [SerializeField] private GameObject[] _Player;

        //�r�̃��[�V����
        [SerializeField] private float _HandHeightRange;
        [SerializeField] private float _InitHandHeight;
        [SerializeField] private float _InitHandWidth;
        [SerializeField] private float _MaxDistance;
        [SerializeField] private float _MinDistance;

        //�H�ׂ郂�[�V����
        [SerializeField] private float _ResetSpeedRotateHead;
        [SerializeField] private float _SpeedScaleHead;
        [SerializeField] private Vector3 _ScaleUpHead;

        // Start is called before the first frame update
        void Start()
        {
            _IsRightStickTrigger = false;
            _PlayerMoveScript = GetComponent<PlayerMove>();
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
        void Update()
        {
            // �R���g���[���[����
            // Gamepad.all �Őڑ�����Ă��邷�ׂẴQ�[���p�b�h��񋓂ł���
            // TextObjects �̐��ȏ�̏��͍ڂ����Ȃ��̂ŁA���Ȃ����̐��� for ����
            for (int i = 0; i < Gamepad.all.Count; i++)
            {
                var gamepad = Gamepad.all[i];

                if (Gamepad.all.Count == 1)
                {
                    i = 1;
                }

                // ���삳�ꂽ�{�^���Ȃǂ̏����擾
                var leftStickValue = gamepad.leftStick.ReadValue();
                var rightStickValue = gamepad.rightStick.ReadValue();
                var dpadValue = gamepad.dpad.ReadValue();

                //���X�e�B�b�N
                if (leftStickValue.magnitude > 0f)
                {
                    _PlayerMoveScript.MoveLStick(i, leftStickValue.normalized);
                    _PlayerAnim[i] = _Player[i].GetComponent<PlayerAnim>();
                    _PlayerAnim[i].SetAnimWalk(_Player[i].transform.forward, leftStickValue.normalized);
                }
                else
                {
                    _PlayerAnim[i] = _Player[i].GetComponent<PlayerAnim>();
                    _PlayerAnim[i].SetIdleAnimWalk();
                }

                //�E�X�e�B�b�N
                if (rightStickValue.magnitude > 0f)
                {
                    _IsRightStickTrigger = true;

                    _PlayerAnim[i].StartAnimEat();

                    _HeadIKscript[i].SetNeckRotation(-GetSignedAngle(
                        _Player[i].transform.forward,
                        new Vector3(rightStickValue.normalized.x,
                                    1,
                                    rightStickValue.normalized.y))
                    );

                    //_HeadIKscript[i].ChangeScaleNeck(_ScaleUpHead,_SpeedScaleHead);
                }
                else
                {
                    _IsRightStickTrigger = false;

                    _HeadIKscript[i].ResetHeadRotation(_ResetSpeedRotateHead);
                    //_HeadIKscript[i].ResetScaleNeck();

                    _PlayerAnim[i].SetAnimSpeedEat();
                }

                var leftTriggerValue = gamepad.leftTrigger.ReadValue();
                var rightTriggerValue = gamepad.rightTrigger.ReadValue();

                _HandPosScript[i].SetLeftHandHeight(_InitHandHeight, _InitHandWidth, leftTriggerValue, _HandHeightRange);
                _HandPosScript[i].SetRightHandHeight(_InitHandHeight, _InitHandWidth, rightTriggerValue, _HandHeightRange);

            }

            // �L�[�{�[�h����
            // ���݂̃L�[�{�[�h���
            var current = Keyboard.current;

            // �L�[�{�[�h�ڑ��`�F�b�N
            if (current == null)
            {
                // �L�[�{�[�h���ڑ�����Ă��Ȃ���
                // Keyboard.current��null�ɂȂ�
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
                _HeadIKscript[0].SetNeckRotation(90);
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

        //��ԏ�̃x�N�g���𕽖ʂ̃x�N�g���ɕϊ���
        //���̃x�N�g���ׂ̈��p�x��Ԃ�
        float GetSignedAngle(Vector3 from,Vector3 to)
        {
            // ���ʂ̖@���x�N�g���i������x�N�g���Ƃ���j
            var planeNormal = Vector3.up;

            // ���ʂɓ��e���ꂽ�x�N�g�������߂�
            var planeFrom = Vector3.ProjectOnPlane(from, planeNormal);
            var planeTo = Vector3.ProjectOnPlane(to, planeNormal);

            // ���ʂɓ��e���ꂽ�x�N�g�����m�̕����t���p�x
            // ���v���Ő��A�����v���ŕ�
            var signedAngle = Vector3.SignedAngle(planeFrom, planeTo, planeNormal);
            return signedAngle;
        }

        public bool GetISRightStickTrigger()
        {
            return _IsRightStickTrigger;
        }
    }
}
