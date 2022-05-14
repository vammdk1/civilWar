using personaje;
using Unity.Netcode;
using UnityEngine;

namespace Netcode
{

    public class SetupLocalPlayer : NetworkBehaviour
    {
        void Start()
        {
            // Si el Prefab del jugador no es del jugador local...
            if (!IsLocalPlayer)
            { 
                // ... quitamos la cámara y el control del movimiento
                Destroy(GetComponentInChildren<Camera>().gameObject);
                Destroy(GetComponent<Move>());
                if (GetComponent<Jump>())
                {
                    Destroy(GetComponent<Jump>());
                    Destroy(GetComponent<Bow>());
                }
                if (GetComponent<Caballero>())
                {
                    Debug.Log("Caballero");
                    Destroy(GetComponent<Caballero>());
                    Destroy(GetComponentInChildren<Caballero>());
                }
                
            }
        }

    }
}
