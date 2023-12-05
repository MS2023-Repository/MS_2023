using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace InGame.Player
{
    public class PlayerController : MonoBehaviour
    {
        PlayerMove _PlayerMoveScript;
        HandPos _HandPosScript;

        PlayerAnim[] _PlayerAnim = new PlayerAnim[2];

        [SerializeField] private GameObject[] _Player;

        [SerializeField] private float _HandHeightRange;
        [SerializeField] private float _InitHandHeight;
        [SerializeField] private float _InitHandWidth;
        [SerializeField] private float _MaxDistance;
        [SerializeField] private float _MinDistance;

        // Start is called before the first frame update
        void Start()
        {
            _PlayerMoveScript = GetComponent<PlayerMove>();
            _PlayerAnim[0] = _Player[0].GetComponent<PlayerAnim>();
            _PlayerAnim[1] = _Player[1].GetComponent<PlayerAnim>();

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
                    _PlayerAnim[i].StartAnimEat();
                }

                var leftTriggerValue = gamepad.leftTrigger.ReadValue();
                var rightTriggerValue = gamepad.rightTrigger.ReadValue();

                _HandPosScript = _Player[i].GetComponent<HandPos>();
                _HandPosScript.SetLeftHandHeight(_InitHandHeight, _InitHandWidth, leftTriggerValue, _HandHeightRange);

                _HandPosScript = _Player[i].GetComponent<HandPos>();
                _HandPosScript.SetRightHandHeight(_InitHandHeight, _InitHandWidth, rightTriggerValue, _HandHeightRange);

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
                    _HandPosScript = _Player[0].GetComponent<HandPos>();
                    _HandPosScript.SetLeftHandHeight(_InitHandHeight, _InitHandWidth, 1.0f, _HandHeightRange);
                }
                else
                {
                    _HandPosScript = _Player[0].GetComponent<HandPos>();
                    _HandPosScript.SetLeftHandHeight(_InitHandHeight, _InitHandWidth, 0.0f, _HandHeightRange);
                }

                if (eKey.isPressed)
                {
                    _HandPosScript = _Player[0].GetComponent<HandPos>();
                    _HandPosScript.SetRightHandHeight(_InitHandHeight, _InitHandWidth, 1.0f, _HandHeightRange);
                }
                else
                {
                    _HandPosScript = _Player[0].GetComponent<HandPos>();
                    _HandPosScript.SetRightHandHeight(_InitHandHeight, _InitHandWidth, 0.0f, _HandHeightRange);

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
                    _HandPosScript = _Player[1].GetComponent<HandPos>();
                    _HandPosScript.SetLeftHandHeight(_InitHandHeight, _InitHandWidth, 1.0f, _HandHeightRange);
                }
                else
                {
                    _HandPosScript = _Player[1].GetComponent<HandPos>();
                    _HandPosScript.SetLeftHandHeight(_InitHandHeight, _InitHandWidth, 0.0f, _HandHeightRange);
                }

                if (oKey.isPressed)
                {
                    _HandPosScript = _Player[1].GetComponent<HandPos>();
                    _HandPosScript.SetRightHandHeight(_InitHandHeight, _InitHandWidth, 1.0f, _HandHeightRange);
                }
                else
                {
                    _HandPosScript = _Player[1].GetComponent<HandPos>();
                    _HandPosScript.SetRightHandHeight(_InitHandHeight, _InitHandWidth, 0.0f, _HandHeightRange);
                }
            }

            _PlayerMoveScript.PlayerPosCorrection(_MinDistance, _MaxDistance);

        }
    }
}
