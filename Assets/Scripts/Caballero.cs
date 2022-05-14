using Archer;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace personaje
{
    public class Caballero : MonoBehaviour
    {
        [SerializeField]
        private InputActionReference ataque;

        [SerializeField]
        private InputActionReference defensa;

        private Animator control;
        private Move move;
        private bool isCooldown;
        private bool defensasActivas=false;


        private void Awake()
        {
            control = GetComponent<Animator>();
            move = GetComponent<Move>();
            ataque.action.performed += Action_performed;
        }
        private void Update()
        {
            if (Input.GetMouseButtonDown(1))
            {
                if (defensasActivas)
                {
                    activarDefensa(false);
                    move.setfreno(false);
                    defensasActivas = !defensasActivas;
                }
                else
                {
                    activarDefensa(true);
                    move.setfreno(true);
                    defensasActivas = !defensasActivas;
                }
                
            }
        }

        private void activarDefensa(bool x)
        {
            control.SetBool("Def",x);
        }

        private void Action_performed(InputAction.CallbackContext obj)
        {
            if (isCooldown)
            {
                return;
            }

            // Cuando se pulsa espacio, producimos un disparo
            StartCoroutine(esperaGolpe());
        }
        IEnumerator esperaGolpe()
        {
            isCooldown = true;
            control.SetTrigger("At01");

            float punchDelay = .2f;

            yield return new WaitForSeconds(punchDelay);
            Impacto();
            yield return new WaitForSeconds(control.GetCurrentAnimatorClipInfo(0).Length - punchDelay);
            isCooldown = false;
       
        }

        private void Impacto()
        {
            // Dibujar un rayo de referencia para saber por dónde se dispara el rayo
            Debug.DrawRay(transform.position + Vector3.up, transform.forward * 1, Color.red, 1);

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