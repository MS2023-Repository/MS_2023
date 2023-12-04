using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MelonAI : MonoBehaviour
{
    public Transform player;
    public float fleeDistance = 10f;

    private NavMeshAgent agent;


    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        // Calculate the distance between the melon and the player
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // Check if the player is within the flee distance
        if (distanceToPlayer < fleeDistance)
        {
            // Calculate the direction away from the player
            Vector3 fleeDirection = transform.position - player.position;

            // Calculate the destination point by adding the flee direction to the current position
            Vector3 fleeDestination = transform.position + fleeDirection.normalized * fleeDistance;

            // Set the destination for the NavMeshAgent
            agent.SetDestination(fleeDestination);
        }
    }
}
