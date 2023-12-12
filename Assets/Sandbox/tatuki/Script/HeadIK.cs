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
        private float _NeckAngleZ;

        private Vector3 _MaxChangeSize;
        private Vector3 _ChangeSize;

        private float _ChangeSpeed;

        PlayerBodyShape _BodyShapeScript;

        HeadManager _HeadManagerScript;

        // Start is called before the first frame update
        void Start()
        {
            _HeadManagerScript = transform.parent.gameObject.GetComponent<HeadManager>();
            _BodyShapeScript = GetComponent<PlayerBodyShape>();
            _NeckAngleY = 0.0f;
            _MaxChangeSize = new Vector3(0, 0, 0);
            _ChangeSize = new Vector3(0, 0, 0);
            _ChangeSpeed = 10.0f;
            _NeckAngleZ = 0.0f;
        }

        private void FixedUpdate()
        {
            ChangeScaleNeck();
        }

        void LateUpdate()
        {
            if (_NeckObj != null)
            {
                //_NeckObj.transform.up = new Vector3(0.0f, -1.0f, 0.0f);
                _NeckObj.transform.Rotate(0.0f, _NeckAngleY, _NeckAngleZ);
                _HeadObj.transform.Rotate(0.0f, 0.0f, _NeckAngleZ);
            }
        }

        public void SetNeckRotationY(float rotateAngle)
        {   
            if(rotateAngle >= 90)
            {
                rotateAngle = 90;
            }
            if(rotateAngle <= -90)
            {
                rotateAngle = -90;
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

            //体のサイズによって頭の大きさを変える
            float SizeChangeForBody;

            //痩せているとき
            if (_BodyShapeScript.GetBodyShapeType() == PlayerBodyShape.BodyShapeType.Thin)
            {
                SizeChangeForBody = _HeadManagerScript.GetThinHeadScale();
            }
            //太っているとき
            else if (_BodyShapeScript.GetBodyShapeType() == PlayerBodyShape.BodyShapeType.Fat)
            {
                SizeChangeForBody = _HeadManagerScript.GetFatHeadScale();
            }
            //標準体型
            else
            {
                SizeChangeForBody = 0.0f;
            }

            _ChangeSize.x += SizeChangeForBody;
        }

        public void SetStartNeckRoatateY()
        {
            _NeckAngleZ = _HeadManagerScript.GetNeckRotateZ();
        }

        public void SetEndNeckRotateY()
        {
            _NeckAngleZ = -_HeadManagerScript.GetNeckRotateZ();
        }

        public void ResetNeckRotateY()
        {
            _NeckAngleZ = 0.0f;
        }

        public void ChangeScaleNeck()
        {
            //目標の頭の大きさ
            Vector3 targetScale = new Vector3(
                1.0f + _ChangeSize.x,
                1.0f + _ChangeSize.y,
                1.0f + _ChangeSize.z
            );

            //目標の大きさに向かうベクトル
            Vector3 linerVec3 = targetScale - _NeckObj.transform.localScale;
            
            //目標までが近いなら目標値を代入
            if (linerVec3.magnitude > 0.5f)
            {
                _NeckObj.transform.localScale += (linerVec3 / 5) * _ChangeSpeed * Time.deltaTime;
            } 
            else
            {
                _NeckObj.transform.localScale = targetScale;
            }
        }

        public void ResetNeckScale()
        {
            _ChangeSize = new Vector3(0, 0, 0.2f);
        }

        public float GetNeckAngleY()
        {
            return _NeckAngleY;
        }
    }
}
