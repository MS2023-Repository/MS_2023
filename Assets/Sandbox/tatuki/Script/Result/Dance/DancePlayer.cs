using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace InGame.Result
{
    public class DancePlayer : MonoBehaviour
    {
        [SerializeField] GameObject[] _TargetPos = new GameObject[5];
        [SerializeField] GameObject[] _DancePlayerObj = new GameObject[5];
        [SerializeField] float _MoveTime;

        bool _IsStartMove = false;
        Vector3[] _MoveVec = new Vector3[5];
        

        // Start is called before the first frame update
        void Start()
        {
            for (int i = 0; i < _DancePlayerObj.Length; i++)
            {
                _MoveVec[i] = (_TargetPos[i].transform.position - _DancePlayerObj[i].transform.position) / _MoveTime;
            }

            _DancePlayerObj[4].SetActive(false);
        }

        // Update is called once per frame
        void Update()
        {
            if(_IsStartMove)
            {
                for(int i = 0; i < _DancePlayerObj.Length; i++)
                {
                    if ((_TargetPos[i].transform.position - _DancePlayerObj[i].transform.position).magnitude > 0.1f)
                    {
                        _DancePlayerObj[i].transform.position += _MoveVec[i] * Time.deltaTime;
                    }
                    else
                    {
                        _DancePlayerObj[i].transform.position = _TargetPos[i].transform.position;
                    }
                }
            }
        }

        //これを呼べばプレイヤーが定位置に移動をはじめる
        public void SetIsStartMove()
        {
            _IsStartMove = true;
            _DancePlayerObj[4].SetActive(true);
        }
    }
}
