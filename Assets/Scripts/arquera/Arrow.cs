using UnityEngine;

namespace personaje
{

    public class Arrow : MonoBehaviour
    {

        private Rigidbody arrowRigidbody;
        private AudioSource audioSource;
        private bool hit;

        private void Awake()
        {
            // Establecer las referencias de Rigidbody (para detener la flecha) y AudioSource (para el sonido de impacto)
            arrowRigidbody = GetComponent<Rigidbody>();
            audioSource = GetComponent<AudioSource>();
        }

        // El rigidbody de la flecha es tipo Trigger, para que no colisione
        private void OnTriggerEnter(Collider other)
        {
            // La flecha sólo produce daño y ruido en el primer impacto
            if (hit) {
                return;
            }

            // Si impacta con el jugador, lo ignoramos
            if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                Debug.Log("prueba");

                return;
            }
            
            hit = true;

            // Reproducir el impacto de la flecha
            audioSource.Play();

            // Hacemos que la flecha sea hija del objeto contra el que impacta, para que se mueva con el
            transform.SetParent(other.transform);

            // Hacemos que la flecha sea kinematica para que no responda a nuevas aceleraciones (se quede clavada)
            arrowRigidbody.isKinematic = true;

            // Miramos a ver si el objeto contra el que ha impacto la flecha tiene un componente Enemy...
            var enemy = other.gameObject.GetComponentInParent<Enemy>();
            // ... Y si lo tiene, le hacemos daño (la siguiente comprohación es equivalente a hacer if (enemy != null) { enemy.Hit(); }
            enemy?.Hit();
            

        }

    }

}