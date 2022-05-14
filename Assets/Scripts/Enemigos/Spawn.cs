using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.AI;


namespace personaje
{
    public class Spawn : NetworkBehaviour
    {

        // Una referencia al prefab de la flecha
        [SerializeField]
        private GameObject ghost;
        [SerializeField]
        private Transform[] waypointsm;
        /**
        internal void spanwEnemigo(int Ne)
        {
            Debug.Log("Fantasmas");
            StartCoroutine(SpawnFantasmaafterFrame(Ne));
        }

        private IEnumerator SpawnFantasmaafterFrame(int Ne)
        {
            yield return null;
            Debug.Log("SpawnPlayerAfterFrame");
            CreadorEnemigos(Ne);
        }**/

        //[ServerRpc(RequireOwnership = false)]
        private void awake()
        {
            GameObject fantamas;
            Debug.Log("SpawnEnemigosServerRpc");
            for (int i = 0; i < 3; i++)
            {
                fantamas = Instantiate(ghost);
                var networObject = fantamas.GetComponent<NetworkObject>();
                networObject.Spawn();

                fantamas.transform.position = new Vector3(0, 1, 100);
                fantamas.GetComponent<Enemy>().waypoints = waypointsm;
                fantamas.AddComponent<NavMeshAgent>();
                fantamas.GetComponent<Enemy>().Asignador();

            }
           
        }

        
    }
}