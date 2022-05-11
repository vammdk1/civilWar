using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class MoveBehaviour : MonoBehaviour
{

    [SerializeField]
    private float moveSpeed = 4;

    [SerializeField]
    private float rotateSpeed = 180;

    private Rigidbody playerRigidbody;

    private void Awake()
    {
        playerRigidbody = GetComponent<Rigidbody>();
    }

    public void FixedUpdate()
    {
        float movement = Input.GetAxis("Vertical");
        Move(movement);

        float rotation = Input.GetAxis("Horizontal");
        Rotate(rotation);
    }

    private void Move(float value)
    {
        //transform.Translate(Vector3.forward * Time.deltaTime * moveSpeed * value);
        playerRigidbody.MovePosition(transform.position + transform.forward * Time.deltaTime * moveSpeed * value);
    }

    private void Rotate(float value)
    {
        //transform.Rotate(Vector3.up * Time.deltaTime * rotateSpeed * value);
        playerRigidbody.MoveRotation(Quaternion.Euler(transform.rotation.eulerAngles + Vector3.up * Time.deltaTime * rotateSpeed * value));
    }
}
