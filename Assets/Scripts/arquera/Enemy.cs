using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Archer
{

    public class Enemy : MonoBehaviour
    {

        // Cuántas vidas tiene el enemigo
        [SerializeField]
        private int hitPoints;

        // Método que se llamará cuando el enemigo reciba un impacto
        public void Hit()
        {
            // Bajar la vida
            hitPoints--;

            // ... y si baja a 0, el enemigo muere
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