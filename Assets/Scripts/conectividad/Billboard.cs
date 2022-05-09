using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Netcode
{

    public class Billboard : MonoBehaviour
    {

        void Update()
        {
            // Las c�maras est�n marcadas con el tag "MainCamera", con Camera.main obtenemos una referencia a la misma
            if (Camera.main != null)
            {
                // Hacer que el texto est� orientado a la c�mara del jugador
                transform.LookAt(Camera.main.transform);
                transform.Rotate(0, 180, 0);
            }
        }
    }


}