using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

namespace personaje
{

    public class Vida : NetworkBehaviour
    {
        [SerializeField]
        private int hitPoints;
        private bool protegido;

        public void SetProtegido(bool x)
        {
            Debug.Log("protegido: " + x);
            protegido=x;
        }
        public void Hit()
        {
            if (protegido)
            {
                Debug.Log("protegido");
                return;
            }
            else
            {
                Debug.Log(protegido);
                hitPoints--;
            }


            if (hitPoints <= 0)
            {
                Die();
            }
        }

        private void Die()
        {
            // Cuándo el enemigo muere
            Destroy(gameObject);
        }
    }
}