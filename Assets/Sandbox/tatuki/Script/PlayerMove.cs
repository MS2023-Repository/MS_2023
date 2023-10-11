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

            // Gamepad.all �Őڑ�����Ă��邷�ׂẴQ�[���p�b�h��񋓂ł���
            // TextObjects �̐��ȏ�̏��͍ڂ����Ȃ��̂ŁA���Ȃ����̐��� for ����
            for (int i = 0; i < Gamepad.all.Count; i++)
            {
                var gamepad = Gamepad.all[i];

                // ���삳�ꂽ�{�^���Ȃǂ̏����擾
                var leftStickValue = gamepad.leftStick.ReadValue();
                var rightStickValue = gamepad.rightStick.ReadValue();
                var dpadValue = gamepad.dpad.ReadValue();

                Vector3 Pos = player[i].transform.position;

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
        }
    }
}