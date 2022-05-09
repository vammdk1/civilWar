using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

namespace Netcode
{

    public class CarSpawner : NetworkBehaviour
    {

        // Establecer una referencia a un prefab (NPC) para instanciarlo en todos los clientes.
        [SerializeField]
        private GameObject carPrefab;

        // Prefabs que instanciaremos para los jugadores (en lugar de que se instance desde el NetworkManager)
        [SerializeField]
        private GameObject player1Car;
        [SerializeField]
        private GameObject player2Car;

        private void Start()
        {
            // Escuchamos a que servidor se haya arrancado para crear un coche
            NetworkManager.Singleton.OnServerStarted += Singleton_OnServerStarted;
        }

        private void Singleton_OnServerStarted()
        {
            // Crear un prefab (coche NPC) que se instanciará en todos los clientes
            if (IsServer)
            {
                var gameObject = Instantiate(carPrefab);

                // La función Spawn hace que este prefab se instancie en todas las copias
                gameObject.GetComponent<NetworkObject>().Spawn();
            }
        }

        // Esta función se llama desde NetLogic para crear un prefab para cada jugador
        public void SpawnPlayer(ulong clientId, string playerName)
        {
            StartCoroutine(SpawnPlayerAfterFrame(clientId, playerName));
        }

        // Necesitamos esperar un frame para asegurarnos de que el sistema de networking se haya arrancado
        private IEnumerator SpawnPlayerAfterFrame(ulong clientId, string playerName)
        {
            yield return null;

            // Llamamos al servidor para que cree un prefab distinto para cada jugador
            SpawnPlayerServerRpc(clientId, playerName);
        }

        // Esta función se llama en el servidor aunque se llame desde un cliente
        // Con RequireOwnership = false hacemos que cualquier cliente pueda llamar al servidor (sin necesitar ser Owner)
        // https://docs-multiplayer.unity3d.com/netcode/current/advanced-topics/message-system/serverrpc
        [ServerRpc(RequireOwnership = false)]
        private void SpawnPlayerServerRpc(ulong clientId, string playerName)
        {
            GameObject carGameObject;

            // En función del ClientId instanciamos un prefab u otro
            if (clientId == 0)
            {
                carGameObject = Instantiate(player1Car);
            }
            else
            {
                carGameObject = Instantiate(player2Car);
            }

            // Con el NetworkObject llamamos a SpawnAsPlayerObject para que este prefab sea el Player Object de cada cliente
            var networkObject = carGameObject.GetComponent<NetworkObject>();
            networkObject.SpawnAsPlayerObject(clientId);

            // Establecemos el nombre con PlayerDisplay
            var playerDisplay = carGameObject.GetComponent<PlayerDisplay>();
            playerDisplay.SetPlayerName(playerName);
        }

    }

}