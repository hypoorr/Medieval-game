using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMotor : MonoBehaviour
{


    private bool isGrounded;

    PlayerInput playerInput;
    PlayerInput.OnFootActions input;

    CharacterController controller;
    Animator animator;
    AudioSource audioSource;

    [Header("Controller")] // variables for player movement
    public float moveSpeed = 5;
    public float gravity = -9.8f;
    public float jumpHeight = 1.2f;
    [SerializeField] private int maxHealth = 100;
    public int currentHealth;
    Vector3 _PlayerVelocity;

    public bool isAttacked = false;

    [Header("Camera")] // camera movement variables
    public Camera cam;
    public float sensitivity;

    float xRotation = 0f;

    // check if player has been hit by enemy
    void OnTriggerEnter(Collider collision)
    {
        if (collision.CompareTag("Weapon"))
        {
            if (!isAttacked) // prevent multiple hits in 1 swing
            {
                isAttacked = true;
                // find the attack damage of the enemy hitting the player
                FieldOfView AIVision = collision.transform.root.GetComponent<FieldOfView>();
                currentHealth -= AIVision.attackDamage;
                Debug.Log(currentHealth);
            }

        }
    }

    void Awake() // assign variables to components and locks cursor
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponentInChildren<Animator>();
        audioSource = GetComponent<AudioSource>();

        playerInput = new PlayerInput();
        input = playerInput.OnFoot;
        AssignInputs();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        currentHealth = maxHealth;
    }

    void Update() // constantly checking if the player is on the ground and checking if the player is attacking
    {
        isGrounded = controller.isGrounded;

        // Repeat Inputs
        if (input.Attack.IsPressed())
        { Attack(); }

        SetAnimations();
    }

    void FixedUpdate() // checks for movement buttons being pressed
    { MoveInput(input.Movement.ReadValue<Vector2>()); }

    void LateUpdate() // checks if the user is moving mouse
    { LookInput(input.Look.ReadValue<Vector2>()); }

    void MoveInput(Vector2 input) // moves player
    {
        Vector3 moveDirection = Vector3.zero;
        moveDirection.x = input.x;
        moveDirection.z = input.y;
    

        controller.Move(transform.TransformDirection(moveDirection) * moveSpeed * Time.deltaTime);
        _PlayerVelocity.y += gravity * Time.deltaTime;
        if (isGrounded && _PlayerVelocity.y < 0)
            _PlayerVelocity.y = -2f;
        controller.Move(_PlayerVelocity * Time.deltaTime);
    }

    void LookInput(Vector3 input) // turns camera 
    {
        float mouseX = input.x;
        float mouseY = input.y;

        xRotation -= (mouseY * Time.deltaTime * sensitivity);
        xRotation = Mathf.Clamp(xRotation, -80, 80);

        cam.transform.localRotation = Quaternion.Euler(xRotation, 0, 0);

        transform.Rotate(Vector3.up * (mouseX * Time.deltaTime * sensitivity));
    }

    void OnEnable() // enables input
    { input.Enable(); }

    void OnDisable() // disables input
    { input.Disable(); }

    void Jump() // logic for when the player jumps
    {
        // Adds force to the player rigidbody to jump
        if (isGrounded)
            _PlayerVelocity.y = Mathf.Sqrt(jumpHeight * -3.0f * gravity);
    }

    void AssignInputs() // calls jump or attack when the input happens
    {
        input.Jump.performed += ctx => Jump();
        input.Attack.started += ctx => Attack();
    }

    // ---------- //
    // ANIMATIONS //
    // ---------- //

    public const string IDLE = "Idle";
    public const string WALK = "Walk";
    public const string ATTACK1 = "Attack 1";
    public const string ATTACK2 = "Attack 2";

    string currentAnimationState;

    public void ChangeAnimationState(string newState) // starts the animation corresponding to the current action
    {
        // STOP THE SAME ANIMATION FROM INTERRUPTING WITH ITSELF //
        if (currentAnimationState == newState) return;

        // PLAY THE ANIMATION //
        currentAnimationState = newState;
        animator.CrossFadeInFixedTime(currentAnimationState, 0.2f);
    }

    void SetAnimations() // checks if the player is idle or walking
    {
        // If player is not attacking
        if (!attacking)
        {
            if (_PlayerVelocity.x == 0 && _PlayerVelocity.z == 0)
            { ChangeAnimationState(IDLE); }
            else
            { ChangeAnimationState(WALK); }
        }
    }

    // ------------------- //
    // ATTACKING BEHAVIOUR //
    // ------------------- //

    [Header("Attacking")]
    public float attackDistance = 3f;
    public float attackDelay = 0.4f;
    public float attackSpeed = 1f;
    public int attackDamage = 25;
    public LayerMask attackLayer;

    public GameObject hitEffect;
    public AudioClip swordSwing;
    public AudioClip hitSound;

    bool attacking = false;
    bool readyToAttack = true;
    int attackCount;

    public void Attack() // when player clicks, sword swings and animations play
    {
        if (!readyToAttack || attacking) return;

        readyToAttack = false;
        attacking = true;

        Invoke(nameof(ResetAttack), attackSpeed);
        Invoke(nameof(AttackRaycast), attackDelay);

        audioSource.pitch = Random.Range(0.9f, 1.1f);
        audioSource.PlayOneShot(swordSwing);

        if (attackCount == 0)
        {
            ChangeAnimationState(ATTACK1);
            attackCount++;
        }
        else
        {
            ChangeAnimationState(ATTACK2);
            attackCount = 0;
        }
    }

    void ResetAttack() // resets attack
    {
        attacking = false;
        readyToAttack = true;
    }

    void AttackRaycast() // checks if what the player is swinging at contains "hittable"
    {
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out RaycastHit hit, attackDistance, attackLayer))
        {
            HitTarget(hit.point);
            if(hit.transform.root.TryGetComponent<Actor>(out Actor T)) // if the attacked object has actor script, deal damage
            { T.TakeDamage(attackDamage);}
        }
    }

    void HitTarget(Vector3 pos) // creates hit mark on object
    {
        audioSource.pitch = 1;
        audioSource.PlayOneShot(hitSound);

        GameObject GO = Instantiate(hitEffect, pos, Quaternion.identity);
        Destroy(GO, 20);
    }
}