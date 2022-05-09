using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

namespace Netcode
{

    public class PlayerDisplay : NetworkBehaviour
    {

        // Este script sirve para poner el nombre de cada jugador sobre su coche


        // Una referencia a la etiqueta de texto (UI) que está sobre el coche de cada jugador
        [SerializeField]
        private Text playerNameLabel;

        // Las variables de tipo NetworkVariable sirven para sincronizar datos entre todos los clientes
        // Por defecto, todos pueden leer el valor pero solo el servidor puede escribirlo
        // (En este caso hemos cambiado el permiso de escritura para que solo el Owner pueda escribir)
        private NetworkVariable<FixedString64Bytes> networkPlayerName = new NetworkVariable<FixedString64Bytes>(writePerm: NetworkVariableWritePermission.Owner);

        private void Awake()
        {
            // Nos suscribimos al evento que nos notificará que el valor del NetworkVariable ha cambiado
            networkPlayerName.OnValueChanged = OnPlayerNameUpdated;
            // Establecemos el valor del nombre en el texto
            playerNameLabel.text = networkPlayerName.Value.Value;
        }

        public override void OnNetworkSpawn()
        {
            base.OnNetworkSpawn();

            // OnNetworkSpawn se llama cuando prefab (en este caso, el prefab de cada jugador) se ha instanciado

            // Si no es el prefab del jugador local, ponemos el valor que ya tenemos almacenado en la variable
            if (!IsLocalPlayer)
            {
                playerNameLabel.text = networkPlayerName.Value.Value;
            }
        }

        private void OnPlayerNameUpdated(FixedString64Bytes previousValue, FixedString64Bytes newValue)
        {
            // Este evento se llama cuando el valor del NetworkVariable ha cambiado (nos hemos suscrito en el Awake)
            playerNameLabel.text = newValue.Value;
        }

        public void SetPlayerName(string playerName)
        {
            // Esta función se llama por cada jugador cuando ha puesto el nombre

            // Actualizamos la etiequeta de texto
            playerNameLabel.text = playerName;


            // Y aquí actualizamos el valor de la variable
            //UpdateNameServerRpc(playerName);
            networkPlayerName.Value = playerName;
        }

        //[ServerRpc]
        //private void UpdateNameServerRpc(string playerName)
        //{
        //    networkPlayerName.Value = playerName;
        //}

    }

}