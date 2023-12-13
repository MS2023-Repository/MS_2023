using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace InGame.Player
{
    public class EatArea : MonoBehaviour
    {
        EatManager _EatManagerScript;
        PlayerBodyShape _PlayerBodyShapeScript;

        private void Start()
        {
            _EatManagerScript = transform.parent.parent.GetComponent<EatManager>();
            _PlayerBodyShapeScript = transform.parent.parent.GetComponent<PlayerBodyShape>();
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.gameObject.CompareTag("CollectibleObject"))
            {
                if (_EatManagerScript.GetIsEat())
                {
                    Destroy(other.gameObject);
                    _PlayerBodyShapeScript.HungerUp();
                }
            }
        }
    }
}
