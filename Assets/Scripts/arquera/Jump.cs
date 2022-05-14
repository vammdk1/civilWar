using UnityEngine;
using UnityEngine.InputSystem;

namespace personaje
{

    public class Jump : MonoBehaviour
    {
        [SerializeField]
        private float jumpForce = 100;
        [SerializeField]
        private LayerMask layerMask;

        private Rigidbody playerRigidbody;
        [SerializeField]
        private InputActionReference jumpActionReference;
        private int groundCollisions;

        private void Awake()
        {
            playerRigidbody = GetComponent<Rigidbody>();
            jumpActionReference.action.performed += Action_performed;
        }
        private void Action_performed(InputAction.CallbackContext obj)
        {
            Debug.Log("salto");
            // Si el personaje está en el suelo y el jugador pulsa espacio...
            if (groundCollisions > 0)
            {
                // ... hacemos que el personaje salte aplicando una fuerza hacía arriba
                playerRigidbody.AddForce(Vector3.up * jumpForce);
            }
        }

        private void OnCollisionEnter(Collision collision)
        {
            // Si el objeto contra el que chocamos (collision.gameObject) está en las capas que consideramos suelo (layerMask)...
            if (layerMask == (layerMask | 1 << collision.gameObject.layer))
            {
                // ... establecemos que el personaje está en el suelo
                groundCollisions++;
            }
        }

        private void OnCollisionExit(Collision collision)
        {
            // Al dejar de estar en contacto con un objeto que sea suelo...
            if (layerMask == (layerMask | 1 << collision.gameObject.layer))
            {
                // ... establecemos que el personaje ya no está en el suelo
                groundCollisions--;
            }
        }
    }

}