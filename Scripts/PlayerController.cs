using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Linq;

public class PlayerController : MonoBehaviour
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

    public AudioClip[] footstepClips; // Array to hold footstep sounds
    private AudioSource audioSource;
    public float stepInterval = 0.5f; // Time between steps
    private float stepTimer = 0f;

    public AudioClip attackClip; // Audio clip for attack sound
    public AudioClip spinClip; // Audio clip for spin sound

    void Start()
    {
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        isWalkingHash = Animator.StringToHash("isWalking");
        isRunningHash = Animator.StringToHash("isRunning");
        isWalkingLeftHash = Animator.StringToHash("isWalkingLeft");
        isWalkingRightHash = Animator.StringToHash("isWalkingRight");
        isWalkingBackwardsHash = Animator.StringToHash("isWalkingBackwards");

        // Debug logs to ensure audio clips are assigned
        if (attackClip == null)
            Debug.LogWarning("Attack clip is not assigned!");
        else
            Debug.Log($"Attack clip assigned: {attackClip.name}");

        if (spinClip == null)
            Debug.LogWarning("Spin clip is not assigned!");
        else
            Debug.Log($"Spin clip assigned: {spinClip.name}");
    }

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

        // Play footstep sounds
        PlayFootstepSounds(forwardPressed, backwardPressed, leftPressed, rightPressed, runPressed);

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

    // Function to play footstep sounds
    void PlayFootstepSounds(bool forwardPressed, bool backwardPressed, bool leftPressed, bool rightPressed, bool runPressed)
    {
        if (forwardPressed || backwardPressed || leftPressed || rightPressed)
        {
            stepTimer -= Time.deltaTime;

            if (stepTimer <= 0f)
            {
                int index = Random.Range(0, footstepClips.Length);
                audioSource.clip = footstepClips[index];
                audioSource.Play();
                stepTimer = runPressed ? stepInterval / 2 : stepInterval; // Play faster when running
            }
        }
        else
        {
            if (audioSource.isPlaying && footstepClips.Contains(audioSource.clip))
            {
                audioSource.Stop();
            }
            stepTimer = 0f;
        }
    }

    // Function to handle combat
    void Combat(bool attackPressed, bool spinPressed)
    {
        if (attackPressed)
        {
            animator.SetTrigger("Attack");
            Debug.Log("Attack triggered");
            //StartCoroutine(PlayAttackSoundMultipleTimes(attackClip, 3, 0.1f)); // Play the attack sound 3 times quickly
            PlaySound(attackClip);
        }

        if (spinPressed)
        {
            animator.SetTrigger("Spin");
            Debug.Log("Spin triggered");
            PlaySound(spinClip);
        }
    }

    // Coroutine to play a sound multiple times
    IEnumerator PlayAttackSoundMultipleTimes(AudioClip clip, int repetitions, float interval)
    {
        for (int i = 0; i < repetitions; i++)
        {
            PlaySound(clip);
            yield return new WaitForSeconds(interval);
        }
    }

    // Function to play sound clips
    void PlaySound(AudioClip clip)
    {
        if (clip != null)
        {
            Debug.Log($"Playing sound: {clip.name}");
            audioSource.PlayOneShot(clip);
        }
        else
        {
            Debug.LogWarning("Attempted to play a null audio clip!");
        }
    }

    // Function to trigger the player's death animation
    void Die()
    {
        // Trigger the death animation
        animator.SetTrigger("DieTrigger");

        // Optionally, disable player control or other actions during death animation
        GetComponent<PlayerController>().enabled = false;

        // Add any additional functionality here, such as game over logic
    }
}






