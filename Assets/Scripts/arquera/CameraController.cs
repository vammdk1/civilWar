using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Archer
{

    public class CameraController : MonoBehaviour
    {

        // El objeto al que va a seguir la c�mara
        [SerializeField]
        private Transform target;

        // �ngulo de la c�mara
        [SerializeField]
        private float angle;

        // Distancia a la que se coloca la c�mara con respecto a la arquera
        [SerializeField]
        private float distance;

        // Desplazamiento con respecto al pivote de la arquera 
        // (para que la c�mara est� m�s orienta hacia la cabeza que a los pies)
        [SerializeField]
        private Vector3 offset;
        
        // Velocidad a la que se mueve la c�mara con Vector3.MoveTowards()
        //[SerializeField]
        //private float travelSpeed;

        // Tiempo que tarda la c�mara en moverse a la nueva ubicaci�n con Vector3.Lerp()
        [SerializeField]
        private float travelTime;

        private void Update()
        {
            // Mover la c�mara inmediatamente
            //transform.rotation = Quaternion.Euler(angle, target.eulerAngles.y, 0);
            //transform.position = target.position + offset - transform.forward * distance;

            // Calcular la posici�n a la que se tiene que mover la c�mara para moverla de manera suavidada
            var referencePosition = target.position + offset;
            float offsetY = Mathf.Cos(angle * Mathf.Deg2Rad) * distance;
            float offsetX = Mathf.Sin(angle * Mathf.Deg2Rad) * distance;

            // Posici�n a la que se tiene que mover la c�mara
            var newPosition = referencePosition + target.rotation * new Vector3(0, offsetY, offsetX);

            // Mover la c�mara a una velocidad determinada
            //transform.position = Vector3.MoveTowards(transform.position, newPosition, Time.deltaTime * travelSpeed);

            // Mover la c�mara en un tiempo dado
            transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime / travelTime);

            // Orientar la c�mara hacia la arquera
            transform.LookAt(target.position + offset);
        }

    }

}
