using System;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class EnemyNavigation : MonoBehaviour
{
    [SerializeField, Range(2, 10)] private float runAwayDistance = 10f;

    private NavMeshAgent myAgent;
    private bool isNearPlayer;

    private void Start()
    {
        myAgent = GetComponent<NavMeshAgent>();
        isNearPlayer = false;

        // Start the random destination coroutine
        StartCoroutine(SetRandomDestinationAsync());
    }

    private void Update()
    {
        var playerTransform = Camera.main.transform;
        var distance = Vector3.Distance(playerTransform.position, transform.position);
        var direction = (transform.position - playerTransform.position).normalized;
        direction.y = 0;
        var destination = transform.position + direction * runAwayDistance;

        if (distance >= runAwayDistance)
        {
            isNearPlayer = false;
        }
        else
        {
            myAgent.SetDestination(destination);
            isNearPlayer = true;
        }
    }

    private System.Collections.IEnumerator SetRandomDestinationAsync()
    {
        while (true)
        {
            if (!isNearPlayer)
            {
                var randomValue = Random.Range(-100, 100);
                if (myAgent == null) yield break;

                myAgent.SetDestination(new Vector3(randomValue, 0, randomValue));

                yield return new WaitForSeconds(10f);
            }
            yield return null;
        }
    }
}