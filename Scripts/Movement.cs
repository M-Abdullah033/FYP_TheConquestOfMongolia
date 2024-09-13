using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Animator animator;
    int isWalkingHash;
    int isRunningHash;
    int isWalkingLeftHash;
    int isWalkingRightHash;
    int isWalkingBackwardsHash;
    public float walkSpeed = 3f;
    public float runSpeed = 6f;


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        isWalkingHash = Animator.StringToHash("isWalking");
        isRunningHash = Animator.StringToHash("isRunning");
        isWalkingLeftHash = Animator.StringToHash("isWalkingLeft");
        isWalkingRightHash = Animator.StringToHash("isWalkingRight");
        isWalkingBackwardsHash = Animator.StringToHash("isWalkingBackwards");
    }

    // FixedUpdate is called a fixed number of times per second
    void FixedUpdate()
    {
        bool forwardPressed = Input.GetKey("w");
        bool backwardPressed = Input.GetKey("s");
        bool leftPressed = Input.GetKey("a");
        bool rightPressed = Input.GetKey("d");
        bool runPressed = Input.GetKey(KeyCode.LeftShift);
        bool attackPressed = Input.GetKeyDown(KeyCode.Mouse0); // Assuming left mouse button for attack
        bool spinPressed = Input.GetKeyDown(KeyCode.Space); // Assuming Space key for spin

        // Move the character
        MoveCharacter(forwardPressed, backwardPressed, leftPressed, rightPressed, runPressed);

        // Animation control
        SetAnimationBools(forwardPressed, backwardPressed, leftPressed, rightPressed, runPressed);

        // Handle combat
        if (attackPressed || spinPressed)
        {
            Combat(attackPressed, spinPressed);
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


    public void Die()
    {
        animator.SetTrigger("DieTrigger");
        // Optionally, disable player control or other actions during death animation
        // Example: Disable movement script
        GetComponent<Movement>().enabled = false;
        // Add any additional functionality here, such as game over logic
    }
}
