using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GhostPatrolling : MonoBehaviour
{
    public NavMeshAgent ghost;

    int waypointIndex = 0;

    public Transform[] waypoints;
    void Start()
    {
        ghost.SetDestination(waypoints[waypointIndex].position);
    }

    void Update()
    {
        if(ghost.remainingDistance < ghost.stoppingDistance)
        {
            waypointIndex = (waypointIndex + 1);
            if(waypointIndex == waypoints.Length)
            {
                waypointIndex = 0;
            }
            ghost.SetDestination(waypoints[waypointIndex].position);
        }
    }
}
