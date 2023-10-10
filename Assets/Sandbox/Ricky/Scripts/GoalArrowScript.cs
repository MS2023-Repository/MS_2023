using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GoalArrowScript : MonoBehaviour
{
    private GameObject arrowObj;

    private GameObject playerObj;
    private GameObject goalObject;

    private TextMeshPro distanceText;

    // Start is called before the first frame update
    void Start()
    {
        arrowObj = transform.GetChild(0).gameObject;

        playerObj = GameObject.FindGameObjectWithTag("Player").gameObject;
        goalObject = GameObject.FindGameObjectWithTag("Goal").gameObject;

        distanceText = transform.GetChild(1).GetComponent<TextMeshPro>();
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = playerObj.transform.position + transform.up * 0.5f;

        arrowObj.transform.LookAt(goalObject.transform.position);
        distanceText.text = Vector3.Distance(playerObj.transform.position, goalObject.transform.position).ToString("F2") + "M";
    }
}