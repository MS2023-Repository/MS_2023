using InGame.Player;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerPickupScript : MonoBehaviour
{
    [SerializeField] private GameObject pickupRangeObj;

    private PlayerController playerControlSc;

    private int playerNo;

    // Start is called before the first frame update
    void Start()
    {
        playerControlSc = GetComponent<PlayerController>();

        string name = this.gameObject.name;
        if (name.Contains("1"))
        {
            playerNo = 1;
        }
        else if (name.Contains("2"))
        {
            playerNo = 2;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 stickValue = Vector2.zero;
        switch (playerNo)
        {
            case 1:
                stickValue = playerControlSc.rightStickP1;
                break;
            case 2:
                stickValue = playerControlSc.rightStickP2;
                break;
        }

        pickupRangeObj.transform.Translate(this.transform.forward * stickValue, Space.Self);
    }
}