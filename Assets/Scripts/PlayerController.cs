using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;


public class Player : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    [SerializeField] private float rotationSpeed = 5f;
    [SerializeField] private float movementSpeed = 5f;
    [SerializeField] private float stepDistance = 2f;

    private CinemachineImpulseSource cinemachineImpulseSource;
    private CharacterController characterController;
    private Vector3 moveInput;
    private float rotation;
    private float gravity = -9.81f;
    private float accumulatedDistance = 0f;


    void Start()
    {
        characterController = GetComponent<CharacterController>();
        cinemachineImpulseSource = GetComponent<CinemachineImpulseSource>();
        Debug.Log("Cargamos characterController");
    }


    public void Move(InputAction.CallbackContext context)
    {
        Vector2 move = context.ReadValue<Vector2>();
        moveInput = Vector3.forward * move.y;

        rotation = move.x;

    }



    private void Step()
    {
        cinemachineImpulseSource.GenerateImpulse();
        // stepsound.Play();
    }


    void Update()
    {

        if (moveInput == Vector3.zero && rotation == 0) return;

        transform.Rotate(0, rotation * rotationSpeed * Time.deltaTime, 0);

        if (!characterController.isGrounded) moveInput.y += gravity * Time.deltaTime;

        Vector3 globalMove = transform.TransformDirection(moveInput);

        accumulatedDistance += globalMove.magnitude * movementSpeed * Time.deltaTime;

        if (accumulatedDistance >= stepDistance)
        {
            Step();
            accumulatedDistance = 0f;
        }

        characterController.Move(globalMove * movementSpeed * Time.deltaTime);

    }
}
