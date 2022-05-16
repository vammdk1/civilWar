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
        private int Npuntos;

        [SerializeField]
        private  List<Transform> waypoints = new List<Transform>();

        [SerializeField]
        private int Nenemy;
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
        private void Awake()
        {
            Debug.Log("fantasma-----------------------------------------");
            GameObject fantamas;
            fantamas = Instantiate(ghost);
        
            for (int i = 0; i < Nenemy; i++)
            {
                fantamas = Instantiate(ghost);
                fantamas.transform.position = new Vector3(0, 1, 100);
                fantamas.GetComponent<Enemy>().Asignador(waypoints);
                var networObject = fantamas.GetComponent<NetworkObject>();
                networObject.Spawn();
              
                

            }
           
        }

        
    }
}