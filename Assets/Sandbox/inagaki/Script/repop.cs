using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using Unity.Collections;
using UnityEngine;

namespace gimmick
{

    public class repop : MonoBehaviour
    {
        // Start is called before the first frame update
        private GameObject _child;

        private bool _touchjuge = false;
        void Start()
        {
            _child = transform.Find("CollectibleObject").gameObject;
            _child.SetActive(false);
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKey(KeyCode.F))
            {
                transform.position = new Vector3(0, 10, 0);
                _touchjuge = true;
            }
        }
        //ƒ^ƒO‚É‚æ‚é”»’è
        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.tag == "Ground" && _touchjuge == true)
            {
                _child.SetActive(true);
                
                this.gameObject.transform.DetachChildren();

                Destroy(gameObject, 0.01f);
            }
        }



    }
}
