using System;
using System.Threading;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Navmesh : MonoBehaviour
{
    [SerializeField, Range(5, 30)] private float _runAwayDistance = 10f;
    private NavMeshAgent _myAgent;

    void Start()
    {
        _myAgent = GetComponent<NavMeshAgent>();
        SetRandomDestinationAsync();
    }

    void Update()
    {
        var playerTransform = Camera.main.transform;
        var distance = Vector3.Distance(playerTransform.position, transform.position);
        var direction = (transform.position - playerTransform.position).normalized;
        direction.y = 0;
        var destination = transform.position + direction * _runAwayDistance;

        if (distance >= _runAwayDistance)
        {
            SetRandomDestinationAsync();
        }
        else
        {
            _myAgent.SetDestination(destination);
        }
    }

    private void SetRandomDestinationAsync()
    {
        StartCoroutine(RandomDestinationCoroutine());
    }

    private System.Collections.IEnumerator RandomDestinationCoroutine()
    {
        while (true)
        {
            var randomValue = UnityEngine.Random.Range(-100, 100);
            if (_myAgent == null) yield break;

            _myAgent.SetDestination(new Vector3(randomValue, 0, randomValue));
            yield return new WaitForSeconds(10f);
        }
    }
}