using Archer;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Caballero : MonoBehaviour
{
    [SerializeField]
    private InputActionReference ataque;

    [SerializeField]
    private InputActionReference defensa;

    private Animator control;
    private Move move;

    private void Awake()
    {
        control = GetComponent<Animator>();
        move = GetComponent<Move>();
        ataque.action.performed += Action_performed;
        defensa.action.performed += Action_performed1;
        defensa.action.canceled += Action_canceled;
    }

    private void Action_canceled(InputAction.CallbackContext obj)
    {
        control.SetBool("Def", false);
        move.setfreno(false);

    }

    private void Action_performed1(InputAction.CallbackContext obj)
    {
        control.SetBool("Def",true);
        move.setfreno(true);
    }

    private void Action_performed(InputAction.CallbackContext obj)
    {
        control.SetTrigger("At01");
    }
}
