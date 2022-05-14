using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace personaje
{

    [RequireComponent(typeof(AudioSource))]
    public class Bow : MonoBehaviour
    {
        [SerializeField]
        private InputActionReference fireInputReference;

        [SerializeField]
        private GameObject arrowPrefab;

        [SerializeField]
        private float force;
        
        [SerializeField]
        private Transform handPosition;
        private Animator shooter;
        private AudioSource audioSource;
        private bool isCooldown;

        private void Awake()
        {            
            audioSource = GetComponent<AudioSource>();
            shooter = GetComponent<Animator>();
            fireInputReference.action.performed += Action_performed;
        }

        private void Action_performed(InputAction.CallbackContext obj)
        {
            if (isCooldown)
            {
                return;
            }
            StartCoroutine(espetaDisparo());
        }
        IEnumerator espetaDisparo()
        {
            isCooldown = true;
            shooter.SetTrigger("disp");
            float shootDelay = .2f;
            yield return new WaitForSeconds(shootDelay);
            Shoot();
            yield return new WaitForSeconds(shooter.GetCurrentAnimatorClipInfo(0).Length - shootDelay);
            isCooldown = false;
        }

        private void Shoot()
        {
            audioSource.Play();
            var newArrow = Instantiate(arrowPrefab);
            newArrow.transform.position = handPosition.position;
            newArrow.transform.rotation = transform.rotation;
            var arrowRigidbody = newArrow.GetComponent<Rigidbody>();
            arrowRigidbody.AddForce(transform.forward * force);

        }
    }

}