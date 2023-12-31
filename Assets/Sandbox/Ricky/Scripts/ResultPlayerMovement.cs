using OutGame.TimeManager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultPlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5.0f;

    private Vector3 targetPos;

    public bool reachedPos {get; private set;}

    private bool moveState;

    public void StartMove()
    {
        moveState = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        targetPos = new Vector3(173, -188, 390);
        reachedPos = false;

        moveState = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (moveState)
        {
            if (this.transform.localPosition != targetPos)
            {
                this.transform.localPosition = Vector3.MoveTowards(this.transform.localPosition, targetPos, moveSpeed * TimeManager.instance.unscaledDeltaTime / 10.0f);
            }
            else
            {
                reachedPos = true;
                this.transform.GetChild(0).GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            }
        }
    }
}
