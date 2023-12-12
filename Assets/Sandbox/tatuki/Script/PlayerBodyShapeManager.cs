using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InGame.Player
{
    public class PlayerBodyShapeManager : MonoBehaviour
    {
        public struct BodyShapeVariable
        {
            public float fatBodyScale;
            public float thinBodyScale;
            public float initHungerLevel;
            public float maxHungerLevel;
            public float fatHungerLevel;
            public float thinHungerLevel;
            public float hungerLevelUpNum;
            public float hungerLevelDownNum;
        }

        [SerializeField] private float _FatBodyScale;
        [SerializeField] private float _ThinBodyScale;
        [SerializeField] private float _InitHungerLevel;
        [SerializeField] private float _MaxHungerLevel;
        [SerializeField] private float _FatHungerLevel;
        [SerializeField] private float _ThinHungerLevel;
        [SerializeField] private float _HungerLevelUpNum;
        [SerializeField] private float _HungerLevelDownNum;

        [SerializeField] private BodyShapeVariable _BodyShapeVariable;

        // Start is called before the first frame update
        void Start()
        {
            _BodyShapeVariable.fatBodyScale = _FatBodyScale;
            _BodyShapeVariable.thinBodyScale = _ThinBodyScale;
            _BodyShapeVariable.initHungerLevel = _InitHungerLevel;
            _BodyShapeVariable.maxHungerLevel = _MaxHungerLevel;
            _BodyShapeVariable.fatHungerLevel = _FatHungerLevel;
            _BodyShapeVariable.thinHungerLevel = _ThinHungerLevel;
            _BodyShapeVariable.hungerLevelUpNum = _HungerLevelUpNum;
            _BodyShapeVariable.hungerLevelDownNum = _HungerLevelDownNum;
        }

        // Update is called once per frame
        void Update()
        {

        }

        public BodyShapeVariable GetBodyShapeVariable()
        {
            return _BodyShapeVariable;
        }

    }
}
