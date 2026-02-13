using System.Collections;
using TMPro;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float rotationSpeed = 5f;
    [SerializeField] private float movementSpeed = 5f;
    [SerializeField] private float stepDistance = 2f;

    [Header("UI")]
    [SerializeField] private TMP_Text text;

    [Header("Audio")]
    [SerializeField] private AudioSource stepSound;
    [SerializeField] private AudioSource takeDamageSound;

    [Header("Health")]
    [SerializeField] private PlayerHealth playerHealth;

    private GameObject textGO;
    private CinemachineImpulseSource cinemachineImpulseSource;
    private CharacterController characterController;
    private Vector3 moveInput;
    private float rotation;
    private float gravity = -9.81f;
    private float accumulatedDistance = 0f;
    private Collider colliderObject;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        cinemachineImpulseSource = GetComponent<CinemachineImpulseSource>();
        textGO = text.gameObject;
    }

    public void Move(InputAction.CallbackContext context)
    {
        Vector2 move = context.ReadValue<Vector2>();
        moveInput = Vector3.forward * move.y;
        rotation = move.x;
    }

    public void Interact(InputAction.CallbackContext context)
    {
        if (context.performed && colliderObject != null)
        {
            AbstractSwitcherBehaviour buttonSwitch =
                colliderObject.GetComponentInChildren<AbstractSwitcherBehaviour>();

            if (buttonSwitch != null)
            {
                buttonSwitch.Action();
            }
        }
    }

    void Update()
    {
        Movement();
    }

    void Movement()
    {
        if (moveInput == Vector3.zero && rotation == 0) return;

        transform.Rotate(0, rotation * rotationSpeed * Time.deltaTime, 0);

        if (!characterController.isGrounded)
            moveInput.y += gravity * Time.deltaTime;

        Vector3 globalMove = transform.TransformDirection(moveInput);

        accumulatedDistance += globalMove.magnitude * movementSpeed * Time.deltaTime;

        if (accumulatedDistance >= stepDistance)
        {
            cinemachineImpulseSource.GenerateImpulse();
            stepSound.volume = Random.Range(0.9f, 1.1f);
            stepSound.Play();
            accumulatedDistance = 0f;
        }

        characterController.Move(globalMove * movementSpeed * Time.deltaTime);
    }

    internal void InteractableObject(Collider collider)
    {
        if (collider == null)
        {
            textGO.SetActive(false);
            colliderObject = null;
            return;
        }

        colliderObject = collider;
        text.text = "Press (X) or E to activate!";
        textGO.SetActive(true);
    }

    public void TakeDamage(int damage)
    {
        if (!takeDamageSound.isPlaying)
            takeDamageSound.Play();

        playerHealth.TakeHit(damage);
    }
}
