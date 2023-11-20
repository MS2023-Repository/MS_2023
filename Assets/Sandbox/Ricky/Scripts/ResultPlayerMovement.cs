using OutGame.TimeManager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultPlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5.0f;

    private Vector3 targetPos;

    // Start is called before the first frame update
    void Start()
    {
        targetPos = new Vector3(-0.066f, this.transform.localPosition.y, this.transform.localPosition.z);
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.localPosition = Vector3.MoveTowards(this.transform.localPosition, targetPos, moveSpeed * TimeManager.instance.unscaledDeltaTime / 10.0f);
    }
}
