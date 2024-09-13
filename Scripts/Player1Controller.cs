using UnityEngine;
using UnityEngine.UI;

public class Player1Controller : MonoBehaviour
{
    Animator animator;
    int isWalkingHash;
    int isRunningHash;
    int isWalkingLeftHash;
    int isWalkingRightHash;
    int isWalkingBackwardsHash;
    public float walkSpeed = 3f;
    public float runSpeed = 6f;
    public Slider HealthBar; // Reference to the health bar component

    void Start()
    {
        animator = GetComponent<Animator>();
        isWalkingHash = Animator.StringToHash("isWalking");
        isRunningHash = Animator.StringToHash("isRunning");
        isWalkingLeftHash = Animator.StringToHash("isWalkingLeft");
        isWalkingRightHash = Animator.StringToHash("isWalkingRight");
        isWalkingBackwardsHash = Animator.StringToHash("isWalkingBackwards");
    }

    void FixedUpdate()
    {
        bool forwardPressed = Input.GetKey(KeyCode.UpArrow);
        bool backwardPressed = Input.GetKey(KeyCode.DownArrow);
        bool leftPressed = Input.GetKey(KeyCode.LeftArrow);
        bool rightPressed = Input.GetKey(KeyCode.RightArrow);
        bool runPressed = Input.GetKey(KeyCode.RightShift);
        bool attackPressed = Input.GetKeyDown(KeyCode.RightControl); // Assuming Right Control for attack
        bool spinPressed = Input.GetKeyDown(KeyCode.RightAlt); // Assuming Right Alt for spin

        // Move the character
        MoveCharacter(forwardPressed, backwardPressed, leftPressed, rightPressed, runPressed);

        // Animation control
        SetAnimationBools(forwardPressed, backwardPressed, leftPressed, rightPressed, runPressed);

        // Handle combat
        if (attackPressed || spinPressed)
        {
            Combat(attackPressed, spinPressed);
        }

        // Check player's health and die if necessary
        if (HealthBar.value <= 0)
        {
            Die();
        }
    }

    // Function to move the character
    void MoveCharacter(bool forwardPressed, bool backwardPressed, bool leftPressed, bool rightPressed, bool runPressed)
    {
        Vector3 movementDirection = Vector3.zero;
        if (forwardPressed)
            movementDirection += transform.forward;
        if (backwardPressed)
            movementDirection -= transform.forward;
        if (leftPressed)
            movementDirection -= transform.right;
        if (rightPressed)
            movementDirection += transform.right;

        movementDirection = movementDirection.normalized;
        float speed = runPressed ? runSpeed : walkSpeed;
        transform.Translate(movementDirection * speed * Time.deltaTime, Space.World);
    }

    // Function to set animation bools
    void SetAnimationBools(bool forwardPressed, bool backwardPressed, bool leftPressed, bool rightPressed, bool runPressed)
    {
        animator.SetBool(isWalkingHash, forwardPressed || backwardPressed || leftPressed || rightPressed);
        animator.SetBool(isRunningHash, runPressed);
        animator.SetBool(isWalkingLeftHash, leftPressed && !forwardPressed && !backwardPressed && !rightPressed);
        animator.SetBool(isWalkingRightHash, rightPressed && !forwardPressed && !backwardPressed && !leftPressed);
        animator.SetBool(isWalkingBackwardsHash, backwardPressed && !forwardPressed && !leftPressed && !rightPressed);
    }

    // Function to handle combat
    void Combat(bool attackPressed, bool spinPressed)
    {
        if (attackPressed)
        {
            animator.SetTrigger("Attack");
            // Add attack-related functionality here
        }

        if (spinPressed)
        {
            animator.SetTrigger("Spin");
            // Add spin-related functionality here
        }
    }

    // Function to trigger the player's death animation
    void Die()
    {
        // Trigger the death animation
        animator.SetTrigger("DieTrigger");

        // Optionally, disable player control or other actions during death animation
        // Example: Disable movement script
        GetComponent<Player1Controller>().enabled = false;

        // Add any additional functionality here, such as game over logic
    }
}






