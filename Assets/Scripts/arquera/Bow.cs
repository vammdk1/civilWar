using Netcode;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
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
        private float force;
        
        [SerializeField]
        private Transform handPosition;
        private Animator shooter;
        private AudioSource audioSource;
        private bool isCooldown;
        private FlechaSpawner Spawner;

        private void Awake()
        {            
            audioSource = GetComponent<AudioSource>();
            shooter = GetComponent<Animator>();
            fireInputReference.action.performed += Action_performed;
            
        }
        private void Start()
        {
            Spawner = GetComponent<FlechaSpawner>();
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
            Spawner.spanwFlecha(force, handPosition.position, transform.rotation);
            

        }
    }

}