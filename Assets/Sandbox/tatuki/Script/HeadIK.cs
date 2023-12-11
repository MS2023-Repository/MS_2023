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

            //�̂̃T�C�Y�ɂ���ē��̑傫����ς���
            float SizeChangeForBody;

            //�����Ă���Ƃ�
            if (_BodyShapeScript.GetBodyShapeType() == PlayerBodyShape.BodyShapeType.Thin)
            {
                SizeChangeForBody = _HeadManagerScript.GetThinHeadScale();
            }
            //�����Ă���Ƃ�
            else if (_BodyShapeScript.GetBodyShapeType() == PlayerBodyShape.BodyShapeType.Fat)
            {
                SizeChangeForBody = _HeadManagerScript.GetFatHeadScale();
            }
            //�W���̌^
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
            //�ڕW�̓��̑傫��
            Vector3 targetScale = new Vector3(
                1.0f + _ChangeSize.x,
                1.0f + _ChangeSize.y,
                1.0f + _ChangeSize.z
            );

            //�ڕW�̑傫���Ɍ������x�N�g��
            Vector3 linerVec3 = targetScale - _NeckObj.transform.localScale;
            
            //�ڕW�܂ł��߂��Ȃ�ڕW�l����
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
