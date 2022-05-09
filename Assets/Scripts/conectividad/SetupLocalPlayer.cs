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
                Destroy(GetComponent<MoveBehaviour>());
            }
        }

    }
}
