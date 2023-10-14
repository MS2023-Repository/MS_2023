using UnityEngine;

namespace InGame.Player
{
    public class HandIK : MonoBehaviour
    {
        private Animator animator;
        public Transform rightHandObj = null;

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

            animator.SetIKPosition(AvatarIKGoal.RightHand, rightHandObj.position);
            animator.SetIKRotation(AvatarIKGoal.RightHand, rightHandObj.rotation);
        }
    }
}