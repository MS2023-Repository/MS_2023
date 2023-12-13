using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InGame.Player
{
    public class HeadManager : MonoBehaviour
    {
        [SerializeField] float _FatHeadScale;
        [SerializeField] float _ThinHeadScale;
        [SerializeField] float _NeckRotateZ;

        public float GetFatHeadScale()
        { 
            return _FatHeadScale; 
        }

        public float GetThinHeadScale()
        {
            return _ThinHeadScale;
        }

        public float GetNeckRotateZ()
        {
            return _NeckRotateZ;
        }
    }
}
