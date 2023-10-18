using UnityEngine;

namespace InGame.Player
{
    public class HandIK : MonoBehaviour
    {
        private Animator animator;
        public Transform rightHandObj = null;
        //public Transform rightFootObj = null;
        public Transform leftHandObj = null;
        //public Transform leftFootObj = null;

        [Range(0, 1)]
        public float weight = 1;

        private void Start()
        {
            animator = GetComponent<Animator>();
        }

        void OnAnimatorIK()// Animation Controller events
        {
            animator.SetIKPositionWeight(AvatarIKGoal.LeftHand, weight);
            animator.SetIKRotationWeight(AvatarIKGoal.LeftHand, weight);
            animator.SetIKHintPositionWeight(AvatarIKHint.LeftElbow, weight);
            animator.SetIKPositionWeight(AvatarIKGoal.RightHand, weight);
            animator.SetIKRotationWeight(AvatarIKGoal.RightHand, weight);
            animator.SetIKHintPositionWeight(AvatarIKHint.RightElbow, weight);

            //animator.SetIKPositionWeight(AvatarIKGoal.LeftFoot, weight);
            //animator.SetIKRotationWeight(AvatarIKGoal.LeftFoot, weight);
            //animator.SetIKPositionWeight(AvatarIKGoal.RightFoot, weight);
            //animator.SetIKRotationWeight(AvatarIKGoal.RightFoot, weight);

            animator.SetIKPosition(AvatarIKGoal.RightHand, rightHandObj.position);
            animator.SetIKRotation(AvatarIKGoal.RightHand, rightHandObj.rotation);
            //animator.SetIKPosition(AvatarIKGoal.RightFoot, rightFootObj.position);
            //animator.SetIKRotation(AvatarIKGoal.RightFoot, rightFootObj.rotation);
            animator.SetIKPosition(AvatarIKGoal.LeftHand, leftHandObj.position);
            animator.SetIKRotation(AvatarIKGoal.LeftHand, leftHandObj.rotation);
            //animator.SetIKPosition(AvatarIKGoal.LeftFoot, leftFootObj.position);
            //animator.SetIKRotation(AvatarIKGoal.LeftFoot, leftFootObj.rotation);
        }
    }
}