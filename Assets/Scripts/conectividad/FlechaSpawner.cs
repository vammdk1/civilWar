using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

namespace Netcode
{
    public class FlechaSpawner : NetworkBehaviour
    {
      
        [SerializeField]
        private GameObject flechas;
       

        internal void spanwFlecha(float force, Vector3 pos, Quaternion rot)
        {
            Debug.Log("flecha");
            StartCoroutine(SpawnArrowfafterFrame(force,pos,rot));
        }

        private IEnumerator SpawnArrowfafterFrame(float force, Vector3 pos, Quaternion rot)
        {
            yield return null;
            Debug.Log("SpawnPlayerAfterFrame");
            SpawnPlayerServerRpc(force,pos,rot);
        }

        [ServerRpc(RequireOwnership = false)]
        private void SpawnPlayerServerRpc(float force, Vector3 pos, Quaternion rot)
        {
            Debug.Log("SpawnFlechaServerRpc");
            GameObject felchaGameObject;
            felchaGameObject = Instantiate(flechas);
            var networObject = felchaGameObject.GetComponent<NetworkObject>();
            networObject.Spawn();
            felchaGameObject.transform.position = pos;
            felchaGameObject.transform.rotation = rot;
            felchaGameObject.GetComponent<Rigidbody>().AddForce(transform.forward * force);

        }



    }

}

