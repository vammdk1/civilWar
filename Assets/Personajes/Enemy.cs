using UnityEngine;
using UnityEngine.AI;

namespace Swordsman
{

    public class Enemy : MonoBehaviour
    {

        private enum EnemyStates
        {
            Patrolling,
            Chasing
        }

        private EnemyStates currentState;

        [SerializeField]
        private Transform objectToChase;

        [SerializeField]
        private float chaseRadius = 5;

        private float defaultSpeed;

        [SerializeField]
        private float chasingSpeedFactor = 1.5f;

        [SerializeField]
        private Transform[] waypoints;

        private int currentWaypoint;

        private NavMeshAgent navMeshAgent;

        private void Awake()
        {
            navMeshAgent = GetComponent<NavMeshAgent>();
            defaultSpeed = navMeshAgent.speed;

            transform.position = waypoints[currentWaypoint].position;
            transform.rotation = waypoints[currentWaypoint].rotation;

            navMeshAgent.SetDestination(waypoints[currentWaypoint].position);
        }

        private void Update()
        {
            if (Vector3.Distance(transform.position, objectToChase.position) < chaseRadius)
            {
                currentState = EnemyStates.Chasing;
                navMeshAgent.speed = defaultSpeed * chasingSpeedFactor;
            }
            else
            {
                currentState = EnemyStates.Patrolling;
                navMeshAgent.speed = defaultSpeed;
            }

            if (currentState == EnemyStates.Patrolling)
            {
                if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
                {
                    currentWaypoint++;

                    if (currentWaypoint >= waypoints.Length)
                    {
                        currentWaypoint = 0;
                    }

                    navMeshAgent.SetDestination(waypoints[currentWaypoint].position);
                }
            }
            else
            {
                navMeshAgent.SetDestination(objectToChase.position);
            }
        }

    }

}