using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InGame.Player
{
    public class HeadIK : MonoBehaviour
    {
        [SerializeField] GameObject _NeckObj;
        [SerializeField] GameObject _HeadObj;

        private float _NeckAngleY;

        private Vector3 _MaxChangeSize;
        private Vector3 _ChangeSize;

        private float _ChangeSpeed;

        // Start is called before the first frame update
        void Start()
        {
            _NeckAngleY = 0.0f;
            _MaxChangeSize = new Vector3(0, 0, 0);
            _ChangeSize = new Vector3(0, 0, 0);
            _ChangeSpeed = 10.0f;
        }

        private void FixedUpdate()
        {
            ChangeScaleNeck();
            Debug.Log(_ChangeSize);
        }

        void LateUpdate()
        {
            if (_NeckObj != null)
            {
                //_NeckObj.transform.up = new Vector3(0.0f, -1.0f, 0.0f);
                _NeckObj.transform.Rotate(0.0f, _NeckAngleY, -10.0f);
                _HeadObj.transform.Rotate(0.0f, 0.0f, -10.0f);
            }
        }

        public void SetNeckRotation(float rotateAngle)
        {   
            if(rotateAngle >= 90)
            {
                rotateAngle = 90;
            }
            if(rotateAngle <= -90)
            {
                rotateAngle = 90;
            }
            
            _NeckAngleY = rotateAngle;
        }

        public void ResetHeadRotation(float ResetSpeed)
        {
            if( _NeckObj != null)
            {
                if (_NeckAngleY > 0.0f)
                {
                    _NeckAngleY -= Time.deltaTime * ResetSpeed;
                }
                else if (_NeckAngleY < 0.0f)
                {
                    _NeckAngleY += Time.deltaTime * ResetSpeed;
                }

                _NeckObj.transform.up = new Vector3(0.0f, 1.0f, 0.0f);
            }
        }

        public void InitChangeScaleNeckNum(Vector3 maxChangeSize, float changeSpeed)
        {
            _MaxChangeSize = maxChangeSize;
            _ChangeSpeed = changeSpeed;
        }

        public void SetMaxChangeSize()
        {
            _ChangeSize = _MaxChangeSize;
        }

        public void ChangeScaleNeck()
        {
            Vector3 targetScale = new Vector3(1.0f + _ChangeSize.x, 1.0f + _ChangeSize.y, 1.0f + _ChangeSize.z);
            Vector3 linerVec3 = targetScale - _NeckObj.transform.localScale;
            if (linerVec3.magnitude > 0.5f)
            {
                _NeckObj.transform.localScale += linerVec3.normalized * _ChangeSpeed * Time.deltaTime;
            }
            else
            {
                _NeckObj.transform.localScale = targetScale;
            }
        }

        public void ResetScaleNeck()
        {
            _ChangeSize = new Vector3(0, 0, 0.2f);
        }
    }
}
