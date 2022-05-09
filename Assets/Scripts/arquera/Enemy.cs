using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Archer
{

    public class Enemy : MonoBehaviour
    {

        // Cu�ntas vidas tiene el enemigo
        [SerializeField]
        private int hitPoints;

        // M�todo que se llamar� cuando el enemigo reciba un impacto
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
            // Cu�ndo el enemigo muere
            Destroy(gameObject);
        }
    }

}