using personaje;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class EnemyController : MonoBehaviour
{
    [SerializeField]
    private int Nenemigos;
    private Spawn spawner;

    private void start()
    {
        Debug.Log("incio enemigos");
        spawner = GetComponent<Spawn>();
        SpawnEnemigos();
    }

    private void SpawnEnemigos()
    {
        //spawner.spanwEnemigo(Nenemigos);
    }
}
