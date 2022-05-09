using UnityEngine;
using UnityEngine.InputSystem;

namespace Archer
{

    public class Move : MonoBehaviour
    {
        [SerializeField]
        private float walkSpeed;
        [SerializeField]
        private float rotateSpeed;
        [SerializeField]
        private float boost;

        [SerializeField]
        private InputActionAsset inputActionAsset;

        private Rigidbody playerRigidbody;
        
        private InputAction moveAction;

        private Animator control;

        private void Awake()
        {
            playerRigidbody = GetComponent<Rigidbody>();
            inputActionAsset.Enable();
            moveAction = inputActionAsset.FindAction("Move");
            control = GetComponent<Animator>();
        }

       
        private void FixedUpdate()
        {
            var movement = moveAction.ReadValue<Vector2>();
            if (Input.GetKey(KeyCode.LeftShift))
            {
                Walk(movement.y*boost);
                Rotate(movement.x*boost);
                control.SetFloat("Hs", movement.y*boost);

            }
            else
            {
                Walk(movement.y);
                Rotate(movement.x);
                control.SetFloat("Hs", movement.y);
            }
            control.SetFloat("Vs", playerRigidbody.velocity.y);
        }

       
        private void Walk(float amount)
        {
            playerRigidbody.MovePosition(transform.position + transform.forward * Time.deltaTime * walkSpeed * amount);
        }

        private void Rotate(float amount)
        {
            playerRigidbody.MoveRotation(Quaternion.Euler(transform.eulerAngles + Vector3.up * amount * Time.deltaTime * rotateSpeed));
        }

    }

}