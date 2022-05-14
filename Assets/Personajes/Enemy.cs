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
        public Transform[] waypoints;

        private int currentWaypoint;

        private NavMeshAgent navMeshAgent;

        [SerializeField]
        private int hitPoints;
        private AudioSource audioSource;
        //Buscar como agregar jugadores a la lista automáticamente
        private void Awake()
        {
            int i = 0;
           
            listaJugadores = GameObject.FindGameObjectsWithTag("Player");
            foreach(GameObject jugador in listaJugadores){
                objectToChase[i] = jugador.transform;
                i++;
            }

            
            audioSource = GetComponent<AudioSource>();

            
        }

        public void Asignador()
        {
            navMeshAgent = GetComponent<NavMeshAgent>();
            defaultSpeed = navMeshAgent.speed;
            transform.position = waypoints[currentWaypoint].position;
            transform.rotation = waypoints[currentWaypoint].rotation;
            navMeshAgent.SetDestination(waypoints[currentWaypoint].position);
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
                
                if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
                {
                  /**  currentWaypoint++;

                    if (currentWaypoint >= waypoints.Length)
                    {
                        currentWaypoint = 0;
                    }**/
                    
                    navMeshAgent.SetDestination(waypoints[Random.Range(0, waypoints.Length)].position);
                }
            }
            else
            {
                if (objectToChase.Length <2 )
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
            audioSource.Play();
           

            // ... y si baja a 0, el enemigo muere
            if (hitPoints <= 0)z
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