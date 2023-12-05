using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InGame.Player
{
    public class PlayerBodyShape : MonoBehaviour
    {
        [SerializeField] private GameObject _BodyObj;
        [SerializeField] private GameObject _RightArmObj;
        [SerializeField] private GameObject _LeftArmObj;

        private float _HungerLevel;

        [SerializeField] private PlayerBodyShapeManager _PlayerBodyShapeManagerScript;

        PlayerBodyShapeManager.BodyShapeVariable _BodyShapeVariable;

        bool _InitFlg;

        // Start is called before the first frame update
        void Start()
        {
            _InitFlg = false;
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            if (!_InitFlg)
            {
                _BodyShapeVariable = _PlayerBodyShapeManagerScript.GetBodyShapeVariable();
                _HungerLevel = _BodyShapeVariable.initHungerLevel;
                _InitFlg = true;
            }

            _HungerLevel -= _BodyShapeVariable.hungerLevelDownNum * Time.deltaTime;

            if( _HungerLevel < 0 )
            {
                _HungerLevel = 0;
            }

            if(Input.GetKeyDown(KeyCode.UpArrow))
            {
                _HungerLevel = 300;
            }

            if(Input.GetKeyDown(KeyCode.DownArrow))
            {
                _HungerLevel = 0.0f;
            }

            Debug.Log(_HungerLevel);

            if(_HungerLevel <= _BodyShapeVariable.thinHungerLevel)
            {
                _BodyObj.transform.localScale = new Vector3(
                    1,
                    1 * (_BodyShapeVariable.thinBodyScale),
                    1 * _BodyShapeVariable.thinBodyScale
                    );
                _RightArmObj.transform.localScale = new Vector3(
                    1 / _BodyShapeVariable.fatBodyScale,
                    1,
                    1
                    );
                _LeftArmObj.transform.localScale = new Vector3(
                    1 / _BodyShapeVariable.fatBodyScale,
                    1,
                    1
                    );
            }
            else if(_HungerLevel >= _BodyShapeVariable.fatHungerLevel)
            {
                _BodyObj.transform.localScale = new Vector3(
                    1,
                    1 * _BodyShapeVariable.fatBodyScale,
                    1 * _BodyShapeVariable.fatBodyScale
                    );
                _RightArmObj.transform.localScale = new Vector3(
                    1 / _BodyShapeVariable.fatBodyScale,
                    1,
                    1
                    );
                _LeftArmObj.transform.localScale = new Vector3(
                    1 / _BodyShapeVariable.fatBodyScale,
                    1,
                    1
                    );
            }
            else
            {
                _BodyObj.transform.localScale = Vector3.one;
                _RightArmObj.transform.localScale = Vector3.one;
                _LeftArmObj.transform.localScale = Vector3.one;
            }
        }

        public void HungerUp()
        {
            _HungerLevel += _BodyShapeVariable.hungerLevelUpNum;
        }
    }
}
