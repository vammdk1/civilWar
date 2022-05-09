using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Archer
{

    [RequireComponent(typeof(AudioSource))]
    public class Bow : MonoBehaviour
    {

        // Referencia a la acci�n de Input para disparar
        [SerializeField]
        private InputActionReference fireInputReference;

        // Una referencia al prefab de la flecha
        [SerializeField]
        private GameObject arrowPrefab;

        // Cantidad de fuerza que aplicaremos al disparar la flecha
        [SerializeField]
        private float force;
        
        // Una referencia a un transform que servir� de punto de referencia para disparar la flecha
        [SerializeField]
        private Transform handPosition;

        private AudioSource audioSource;

        private void Awake()
        {
            // Obtener una referencia al AudioSource, el cu�l usaremos m�s tarde para reproducir un disparo de flecha
            audioSource = GetComponent<AudioSource>();

            // Nos subscribimos al evento de input de disparo (el espacio o el bot�n A).
            fireInputReference.action.performed += Action_performed;
        }

        private void Action_performed(InputAction.CallbackContext obj)
        {
            // Cuando se pulsa espacio, producimos un disparo
            Shoot();
        }

        private void Shoot()
        {
            // Reproducir sonido de disparo de flecha
            audioSource.Play();

            // Instanciar una flecha
            var newArrow = Instantiate(arrowPrefab);

            // Colocar la flecha en el punto de referencia de la mano de la arquera
            newArrow.transform.position = handPosition.position;

            // Orientar la flecha hacia delante con respecto a la arquera
            newArrow.transform.rotation = transform.rotation;

            // Aplicar una fuerza a la flecha para que salga disparada
            var arrowRigidbody = newArrow.GetComponent<Rigidbody>();
            arrowRigidbody.AddForce(transform.forward * force);
        }
    }

}