using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


namespace personaje
{
    public class Spawn : MonoBehaviour
    {


        // Una referencia al prefab de la flecha
        [SerializeField]
        private GameObject ghost;
        [SerializeField]
        private Transform[] waypointsm;
        [SerializeField]
        private int Nenemigos;
        private void Awake()
        {
            for (int i = 0; i < Nenemigos; i++)
            {
                var en1 = Instantiate(ghost);
                en1.transform.position = new Vector3(0, 1, 100);
                en1.GetComponent<Enemy>().waypoints = waypointsm;
                en1.AddComponent<NavMeshAgent>();
                en1.GetComponent<Enemy>().Asignador();
            }
        }

        //
    }
}