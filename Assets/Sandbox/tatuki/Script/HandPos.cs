using InGame.Box;
using UnityEngine;

namespace InGame.Player
{
    public class HandPos : MonoBehaviour
    {
        [SerializeField] private GameObject _Player;
        [SerializeField] private GameObject _LeftHand;
        [SerializeField] private GameObject _RightHand;

        //[SerializeField] private float _InitLeftHeight;
        //[SerializeField] private float _InitRightHeight;

        private float _LeftHeight;
        private float _RightHeight;

        [SerializeField] private GameObject _Box;

        private Vector3 _LeftHandPos;
        private Vector3 _RightHandPos;

        private BoxCornerPos _BoxCornerPosScript;

        // Start is called before the first frame update
        void Start()
        {
            _BoxCornerPosScript = _Box.GetComponent<BoxCornerPos>();

            _LeftHeight = 0.0f;
            _RightHeight = 0.0f;
        }

        // Update is called once per frame
        void Update()
        {
            ResetRotateHand();       
        }

        public void SetLeftHandHeight(float InitHeight,float InitWidth,float height,float width)
        {
            //_BoxCornerPosScript.SetBoxCornerPos();

            _LeftHeight = -height * width;

            _LeftHandPos = _LeftHand.transform.position;
            _LeftHandPos = _Player.transform.position - (_Player.transform.right.normalized * InitWidth);
            _LeftHandPos.y = _Player.transform.position.y + _LeftHeight + InitHeight;
            _LeftHand.transform.position = _LeftHandPos;
        }

        public void SetRightHandHeight(float InitHeight,float InitWidth,float height, float range)
        {
            //_BoxCornerPosScript.SetBoxCornerPos();

            _RightHeight = -height * range;

            _RightHandPos = _RightHand.transform.position;
            _RightHandPos = _Player.transform.position + (_Player.transform.right.normalized * InitWidth);
            _RightHandPos.y = _Player.transform.position.y + _RightHeight + InitHeight;
            _RightHand.transform.position = _RightHandPos;
        }

        private void ResetRotateHand()
        {
            _RightHand.transform.rotation = new Quaternion(0,-90,0,0);
            _LeftHand.transform.rotation = new Quaternion(0, -90, 0, 0);
        }
    }
}
