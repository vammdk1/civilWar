using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraController : MonoBehaviour
{

    // Este Script hace que la cámara siga y mire al jugador, ya lo hemos visto en el proyecto Archer

    [SerializeField]
    private Transform target;

    [SerializeField]
    private float angle;

    [SerializeField]
    private float distance;

    void Update()
    {
        transform.position = target.position;
        transform.rotation = Quaternion.Euler(angle, target.rotation.eulerAngles.y, 0);
        transform.position = transform.position - transform.forward * distance;
    }
}
