using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Archer
{

    public class Weapon : MonoBehaviour
    {

        // Obtener una referencia a la acción "Fire"
        [SerializeField]
        private InputActionReference inputActionReference;
        

        private void Awake()
        {
            // Subscribir el comienzo de la acción disparo
            inputActionReference.action.performed += Action_performed;


        }

       
        private void Action_performed(InputAction.CallbackContext obj)
       
        {
           
            // Dibujar un rayo de referencia para saber por dónde se dispara el rayo
            Debug.DrawRay(transform.position + Vector3.up, transform.forward * 1000, Color.red, 1);

            // Lanzar un rayo desde la arquera hacia delantes
            RaycastHit raycastHit;
            bool hit = Physics.Raycast(transform.position + Vector3.up, transform.forward, out raycastHit);

            // Si ha colisionado...
            if (hit)
            {
                // ... comprobar si le ha dado al enemigo
                if (raycastHit.collider.gameObject.layer == LayerMask.NameToLayer("Enemy"))
                {
                    // Si choca con el enemigo, obtenemos una referencia al componente Enemy para notificar que ha recibido un hit
                    Debug.Log("Hit enemy");
                    var enemy = raycastHit.collider.GetComponent<Enemy>();

                    if (enemy)
                    {
                        enemy.Hit();
                    }
                }
                else
                {
                    Debug.Log("Miss!");
                }
            }
        }
    }

}