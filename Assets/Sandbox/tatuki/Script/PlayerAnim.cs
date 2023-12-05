using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InGame.Player
{
    public class PlayerAnim : MonoBehaviour
    {
        public enum WalkAnimID
        {
            Idle,
            Forward,
            Back,
            Left,
            Right,
        }

        public enum EatAnimID
        {
            Idle,
            Eat,
        }

        [SerializeField] private PlayerController _PlayerControllerScript;
        private Animator _Anim;

        // Start is called before the first frame update
        void Start()
        {
            _Anim = GetComponent<Animator>();
        }

        public void SetAnimWalk(Vector3 PlayerForwardVec,Vector2 StickVec)
        {
            // ��ԏ�̃x�N�g��
            var from = PlayerForwardVec; 
            var to = new Vector3(StickVec.x,1,StickVec.y);

            // ���ʂ̖@���x�N�g���i������x�N�g���Ƃ���j
            var planeNormal = Vector3.up;

            // ���ʂɓ��e���ꂽ�x�N�g�������߂�
            var planeFrom = Vector3.ProjectOnPlane(from, planeNormal);
            var planeTo = Vector3.ProjectOnPlane(to, planeNormal);

            // ���ʂɓ��e���ꂽ�x�N�g�����m�̕����t���p�x
            // ���v���Ő��A�����v���ŕ�
            var signedAngle = Vector3.SignedAngle(planeFrom, planeTo, planeNormal);

            if (signedAngle < 0)
            {
                signedAngle += 360;
            }

            if (signedAngle > 45 && signedAngle < 135)
            {
                SetAnimWalkID(WalkAnimID.Left);
            }
            else if (signedAngle > 135 && signedAngle < 225)
            {
                SetAnimWalkID(WalkAnimID.Back);
            }
            else if (signedAngle > 225 && signedAngle < 315)
            {
                SetAnimWalkID(WalkAnimID.Right);
            }
            else if(signedAngle < 45 || signedAngle < 360 && signedAngle > 315)
            {
                SetAnimWalkID(WalkAnimID.Forward);
            }
        }

        public void SetIdleAnimWalk()
        {
            SetAnimWalkID(WalkAnimID.Idle);
        }

        void SetAnimWalkID(WalkAnimID id)
        {
            _Anim.SetInteger("WalkAnim", (int)id);
        }

        public void StartAnimEat()
        {
            _Anim.SetBool("IsEat", true);
        }

        public void EndAnimEat()
        {
            _Anim.SetBool("IsEat", false);
        }

        public void SetAnimSpeedEat()
        {
            if (_PlayerControllerScript.GetISRightStickTrigger())
            {
                //Debug.Log("ccccccccccc");
                _Anim.SetFloat("AnimSpeed", 0.0f);
            }
            else if(!_PlayerControllerScript.GetISRightStickTrigger())
            {
                //Debug.Log("afafafafafaf");
                _Anim.SetFloat("AnimSpeed", 1.0f);
            }
        }
    }
}
