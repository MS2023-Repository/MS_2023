using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine;

namespace InGame.Player
{

    public class PlayerMove : MonoBehaviour
    {
        //�v���C���[�I�u�W�F�N�g
        [SerializeField] private GameObject[] player;

        // �X�V�̓t���[�����Ƃ�1��Ăяo����܂�
        void Update()
        {
            Vector3 Pos;

            // �R���g���[���[����
            // Gamepad.all �Őڑ�����Ă��邷�ׂẴQ�[���p�b�h��񋓂ł���
            // TextObjects �̐��ȏ�̏��͍ڂ����Ȃ��̂ŁA���Ȃ����̐��� for ����
            for (int i = 0; i < Gamepad.all.Count; i++)
            {
                //Debug.Log(Gamepad.all.Count);
                var gamepad = Gamepad.all[i];

                if (Gamepad.all.Count == 1)
                {
                    i = 1;
                    //Debug.Log(i);
                }

                // ���삳�ꂽ�{�^���Ȃǂ̏����擾
                var leftStickValue = gamepad.leftStick.ReadValue();
                var rightStickValue = gamepad.rightStick.ReadValue();
                var dpadValue = gamepad.dpad.ReadValue();

                Pos = player[i].transform.position;

                //���X�e�B�b�N
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

            // �L�[�{�[�h����
            // ���݂̃L�[�{�[�h���
            var current = Keyboard.current;

            // �L�[�{�[�h�ڑ��`�F�b�N
            if (current == null)
            {
                // �L�[�{�[�h���ڑ�����Ă��Ȃ���
                // Keyboard.current��null�ɂȂ�
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