using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class water : MonoBehaviour
{
    [SerializeField] float _power=1;
    [SerializeField] float _upPower=1;
    private float _timer;
    [SerializeField] float _interval = 1;
    [SerializeField] float _down = 1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _timer += Time.deltaTime*_interval;
    }

    private void OnCollisionStay(Collision collision)
    {
        collision.gameObject.GetComponent<Rigidbody>().AddForce(gameObject.transform.forward*_power);

    }
    private void OnTriggerStay(Collider other)
    {
        //other.gameObject.GetComponent<Rigidbody>().AddForce(Vector3.up * (transform.position.y - other.gameObject.transform.position.y + 1)*UpPower);
        Vector3 pos = other.gameObject.transform.position;
        pos.y = transform.position.y+Mathf.Sin(_timer)*_down;
        other.gameObject.transform.position = pos;
        other.gameObject.GetComponent<Rigidbody>().AddForce(gameObject.transform.forward * _power);
    }
    private void OnTriggerEnter(Collider other)
    {
        other.gameObject.GetComponent<Rigidbody>().useGravity= false;
      

    }
    private void OnTriggerExit(Collider other)
    {
        other.gameObject.GetComponent<Rigidbody>().useGravity = true;
    }
}
