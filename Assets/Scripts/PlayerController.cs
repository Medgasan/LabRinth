using System;
using System.Threading.Tasks;
using TMPro;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerController : MonoBehaviour
{

    [Header("Movement")]
    [SerializeField] private float rotationSpeed = 5f;
    [SerializeField] private float movementSpeed = 5f;

    [Header("UI")]
    [SerializeField] private TMP_Text text;

    [Header("Audio")]
    [SerializeField] private AudioSource stepSound;
    [SerializeField] private AudioSource takeDamageSound;

    [Header("Health")]
    [SerializeField] private PlayerHealth playerHealth;

    [Header("Animations")]
    [SerializeField] private Animator animator;


    private GameObject textGO;
    private CharacterController characterController;
    private Vector3 moveInput;
    private float rotation;
    private float gravity = -9.81f;
    private Collider colliderObject;


    void Start()
    {
        //stepClips = Resources.LoadAll<AudioClip>("Sounds/Steps/");
        //Debug.Log("Loaded " + stepClips.Length + " step sound clips.");

        characterController = GetComponent<CharacterController>();
        textGO = text.gameObject;
    }



    public void Move(InputAction.CallbackContext context)
    {
        Vector2 move = context.ReadValue<Vector2>();
        moveInput = Vector3.forward * move.y;
        rotation = move.x;
    }


    public void OnFootstep()
    {
        stepSound.volume = UnityEngine.Random.Range(0.9f, 1.1f);
        stepSound.Play();
    }


    public void Interact(InputAction.CallbackContext context)
    {
        if (context.performed && colliderObject != null)
        {
            Debug.Log("Interacting with " + colliderObject.gameObject.name);
            AbstractSwitcherBehaviour buttonSwitch = colliderObject.gameObject.GetComponentInChildren<AbstractSwitcherBehaviour>();
            if (buttonSwitch != null)
            {
                buttonSwitch.Action();
            }
            else
            {
                Debug.Log("No ButtonSwitchBehaviour found on the object.");
            }
            Debug.Log("Interact pressed");
        }
    }


    void Update()
    {
        Movement();
    }



    void Movement()
    {

        transform.Rotate(0, rotation * rotationSpeed * Time.deltaTime, 0);

        if (!characterController.isGrounded) moveInput.y += gravity * Time.deltaTime;

        Vector3 globalMove = transform.TransformDirection(moveInput);

        characterController.Move(globalMove.normalized * movementSpeed * Time.deltaTime);
        animator.SetFloat("Speed", characterController.velocity.magnitude);
    }


    internal void InteractableObject(Collider collider)
    {
        AbstractSwitcherBehaviour abstractSwitcher = colliderObject?.GetComponent<AbstractSwitcherBehaviour>();

        if (collider == null) 
        {
            textGO.SetActive(false);
            colliderObject = null;
            abstractSwitcher?.CannotActivate();
            return;
        }
        colliderObject = collider;
        abstractSwitcher?.CanActivate();
        text.text = "Press (X) or E to activate!";
        textGO.SetActive(true);

    }


    public void TakeDamage(int damage)
    {
        if (!takeDamageSound.isPlaying && playerHealth.TakeHit(damage))
            takeDamageSound.Play();

    }

}
