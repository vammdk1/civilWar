using personaje;
using Unity.Netcode;
using UnityEngine;

namespace Netcode
{

    public class NetLogic : NetworkBehaviour
    {

        // Desde este script controlaremos la comunicación de red.

        // Variable donde almacenaremos el nombre que el jugador escriba
        private string playerName = "Nombre";
        private Enemy e;

        // Cogeremos una referencia al CarSpawner, para pedirle que instancie los coches de los jugadores
        private CarSpawner Spawner;

        private void Start()
        {
            // Nos subscribimos al evento OnClientConnection, el cual nos notificará que un jugador se ha conectado
            // Se nos llamará por cada jugador que se conecte en todos los jugadores que estén conectados
            
            NetworkManager.Singleton.OnClientConnectedCallback += Singleton_OnClientConnectedCallback;

            // Más tarde CarSpawner nos servirá para instanciar coches de los jugadores
            Spawner = GetComponent<CarSpawner>();        
        }


        private void Singleton_OnClientConnectedCallback(ulong clientId)
        {
            // Nos hemos subscrito a este evento en Start, se llama en todas las instancias del juego cuando un jugador se conecta
            if (clientId == NetworkManager.Singleton.LocalClientId)
            {
                // Si el jugador que se acaba de conectar es la instancia local del juego, 
                // mediante carSpawner pedimos que el servidor nos instancie nuestro coche
                
                Spawner.spanwplayer(clientId, playerName);
                //e.Buscador();
            }

            var localPlayer = NetworkManager.Singleton.SpawnManager.GetLocalPlayerObject();
            //var playerDisplay = localPlayer.GetComponent<PlayerDisplay>(); da error
            //playerDisplay.SetPlayerName(playerName);
        }

        private void OnGUI()
        {
            // En la función OnGUI dibujamos controles para que el usuario pueda poner su nombre
            // y conectarse al servidor
            
            // Dibujar un campo de texto para que los usuarios introduzcan su nombre
            playerName = GUILayout.TextField(playerName);

            // Dibujar botones que servirán para alojar una partida como Host (server y player a la vez)
            // o bien como Client (solo jugador)
            if (GUILayout.Button("Cliente"))
            {
                NetworkManager.Singleton.StartClient();
            }
            if (GUILayout.Button("Host"))
            {
                NetworkManager.Singleton.StartHost();
            }
        }

    }
}