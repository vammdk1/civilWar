using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace personaje
{

    public class Enemy : MonoBehaviour
    {

        private enum EnemyStates
        {
            Patrolling,
            Chasing
        }

        private EnemyStates currentState;

        private GameObject[] listaJugadores;
        public Transform[] objectToChase;

        [SerializeField]
        private float chaseRadius = 5;

        private float defaultSpeed;

        [SerializeField]
        private float chasingSpeedFactor = 1.5f;

        //[SerializeField]
        public List<Transform> waypoints;

        private int currentWaypoint;

        private NavMeshAgent navMeshAgent;

        [SerializeField]
        private int hitPoints;
        private AudioSource audioSource;
        private void Awake()
        {
            navMeshAgent = gameObject.AddComponent(typeof(NavMeshAgent)) as NavMeshAgent;
        }


        public void Asignador(List<Transform> x )
        {
            audioSource = GetComponent<AudioSource>();
            navMeshAgent = GetComponent<NavMeshAgent>();
            defaultSpeed = 3.5f;
            waypoints = x;
            transform.position = waypoints[currentWaypoint].position;
            transform.rotation = waypoints[currentWaypoint].rotation;
            navMeshAgent.SetDestination(waypoints[currentWaypoint].position);
        }
        public void Buscador()
        {
            listaJugadores = GameObject.FindGameObjectsWithTag("Player");
        }

        private void Update()
        {
          
                if (Vector3.Distance(transform.position, objectToChase[0].position) < chaseRadius)
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

                    if (navMeshAgent.remainingDistance-2 <= navMeshAgent.stoppingDistance)
                    {
                        /**  currentWaypoint++;

                          if (currentWaypoint >= waypoints.Length)
                          {
                              currentWaypoint = 0;
                          }**/

                        navMeshAgent.SetDestination(waypoints[Random.Range(0, waypoints.Count)].position);
                    }
                }
                else
                {
                    if (objectToChase.Length < 2)
                    {
                        navMeshAgent.SetDestination(objectToChase[0].position);
                    }
                    else
                    {
                        if (Vector3.Distance(transform.position, objectToChase[0].position) < Vector3.Distance(transform.position, objectToChase[1].position))
                        {
                            navMeshAgent.SetDestination(objectToChase[0].position);
                        }
                        else
                        {
                            navMeshAgent.SetDestination(objectToChase[1].position);
                        }
                    }
                }
            }
        


        // Método que se llamará cuando el enemigo reciba un impacto
        public void Hit()
        {
            // Bajar la vida
            hitPoints--;
            //audioSource.Play();
           

            // ... y si baja a 0, el enemigo muere
            if (hitPoints <= 0)
            {
                Die();
            }
        }

        private void Die()
        {
            // Cuándo el enemigo muere
            Destroy(gameObject);
        }
    }

}