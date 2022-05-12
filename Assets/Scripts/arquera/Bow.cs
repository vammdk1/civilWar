using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Archer
{

    [RequireComponent(typeof(AudioSource))]
    public class Bow : MonoBehaviour
    {

        // Referencia a la acción de Input para disparar
        [SerializeField]
        private InputActionReference fireInputReference;

        // Una referencia al prefab de la flecha
        [SerializeField]
        private GameObject arrowPrefab;

        // Cantidad de fuerza que aplicaremos al disparar la flecha
        [SerializeField]
        private float force;
        
        // Una referencia a un transform que servirá de punto de referencia para disparar la flecha
        [SerializeField]
        private Transform handPosition;
        private Animator shooter;
        private AudioSource audioSource;
        private bool isCooldown;

        private void Awake()
        {
            // Obtener una referencia al AudioSource, el cuál usaremos más tarde para reproducir un disparo de flecha
            audioSource = GetComponent<AudioSource>();
            shooter = GetComponent<Animator>();
            // Nos subscribimos al evento de input de disparo (el espacio o el botón A).
            fireInputReference.action.performed += Action_performed;
        }

        private void Action_performed(InputAction.CallbackContext obj)
        {
            if (isCooldown)
            {
                return;
            }

            // Cuando se pulsa espacio, producimos un disparo
            StartCoroutine(espetaDisparo());
        }
        IEnumerator espetaDisparo()
        {
            isCooldown = true;
            shooter.SetTrigger("disp");

            float shootDelay = .2f;

            yield return new WaitForSeconds(shootDelay);
            //yield return new WaitForSeconds(.3f);
            Shoot();
            yield return new WaitForSeconds(shooter.GetCurrentAnimatorClipInfo(0).Length - shootDelay);
            isCooldown = false;
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
            //if (shooter.GetCurrentAnimatorStateInfo(0).IsName("disparar"))
            //{
            //    //TODO
            //    espetaDisparo();
            //}
        }
    }

}