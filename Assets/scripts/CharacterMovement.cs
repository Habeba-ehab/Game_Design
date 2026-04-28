using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
[RequireComponent(typeof(Animator))]
public class CharacterMovement : MonoBehaviour
{
    public float moveSpeed = 4f;
    public float rotationSpeed = 720f;

    public float jumpForce = 6f; //  NEW

    public float minX = 0f;
    public float maxX = 60f;
    public float minZ = 0f;
    public float maxZ = 60f;

    private CharacterController controller;
    private Animator animator;
    private float verticalVelocity = 0f;
    private float gravity = -20f;
    private AudioSource footstepAudio;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        footstepAudio = GetComponent<AudioSource>();
    }

    void Update()
    {
        var keyboard = Keyboard.current;
        if (keyboard == null) return;

        float h = 0f;
        float v = 0f;

        if (keyboard.upArrowKey.isPressed)    v =  1f;
        if (keyboard.downArrowKey.isPressed)  v = -1f;
        if (keyboard.leftArrowKey.isPressed)  h = -1f;
        if (keyboard.rightArrowKey.isPressed) h =  1f;

        bool isMoving = (h != 0f || v != 0f);
        animator.SetBool("isRunning", isMoving);

        // footsteps
        if (footstepAudio != null)
        {
            if (isMoving && !footstepAudio.isPlaying)
                footstepAudio.Play();
            else if (!isMoving && footstepAudio.isPlaying)
                footstepAudio.Stop();
        }

        // =========================
        //  JUMP
        // =========================
        if (controller.isGrounded)
        {
            verticalVelocity = -2f;

            if (keyboard.spaceKey.wasPressedThisFrame)
            {
                verticalVelocity = jumpForce;
            }
        }
        else
        {
            verticalVelocity += gravity * Time.deltaTime;
        }

        // rotation
        if (h != 0f)
            transform.Rotate(0f, h * rotationSpeed * Time.deltaTime, 0f);

        // movement
        Vector3 move = transform.forward * v;
        move.y = verticalVelocity;

        controller.Move(move * moveSpeed * Time.deltaTime);

        // clamp
        Vector3 pos = transform.position;
        pos.x = Mathf.Clamp(pos.x, minX, maxX);
        pos.z = Mathf.Clamp(pos.z, minZ, maxZ);

        if (transform.position.x != pos.x || transform.position.z != pos.z)
        {
            controller.enabled = false;
            transform.position = pos;
            controller.enabled = true;
        }
    }
}