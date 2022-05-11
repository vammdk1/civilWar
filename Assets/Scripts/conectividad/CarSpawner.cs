using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

namespace Netcode
{    public class CarSpawner : NetworkBehaviour
    {
        [SerializeField]
        private GameObject Caballero;
        [SerializeField]
        private GameObject Arquera;


       

        internal void spanwplayer(ulong clientId, string playerName)
        {
            Debug.Log("Spawmplayer");
            StartCoroutine(SpawnPlayerAfterFrame(clientId, playerName));
        }

        private IEnumerator SpawnPlayerAfterFrame(ulong clientId, string playerName)
        {
            yield return null;
            Debug.Log("SpawnPlayerAfterFrame");
            SpawnPlayerServerRpc(clientId, playerName);
        }

        [ServerRpc(RequireOwnership =false)]
        private void SpawnPlayerServerRpc(ulong clientId, string playerName)
        {
            Debug.Log("SpawnPlayerServerRpc");
            GameObject carGameObject;
            if (clientId == 0){
                carGameObject = Instantiate(Caballero);
            }
            else
            {
                carGameObject = Instantiate(Arquera);
            }

            var networObject = carGameObject.GetComponent<NetworkObject>();
            networObject.SpawnAsPlayerObject(clientId);

            var playerDisplay = carGameObject.GetComponent<PlayerDisplay>();
            playerDisplay.SetPlayerName(playerName);
        }
    }

}

