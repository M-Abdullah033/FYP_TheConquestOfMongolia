using UnityEngine;

public class FaceEachOther : MonoBehaviour
{
    Transform otherPlayer;
    Rigidbody rb;
    float groundOffset = 0.5f; // Adjust this value based on your ground height

    void Start()
    {
        // Find the other player
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject player in players)
        {
            if (player != gameObject)
            {
                otherPlayer = player.transform;
                break;
            }
        }

        // Get the Rigidbody component
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (otherPlayer != null)
        {
            // Calculate the direction to the other player
            Vector3 direction = otherPlayer.position - transform.position;
            direction.y = 0f; // Ignore height difference

            // Rotate towards the other player
            if (direction != Vector3.zero)
            {
                Quaternion lookRotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
            }
        }
    }

    void FixedUpdate()
    {
        // Cast a ray downwards to detect ground
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit))
        {
            // Adjust player's position to stay above the ground
            float targetY = hit.point.y + groundOffset;
            Vector3 targetPosition = new Vector3(transform.position.x, targetY, transform.position.z);
            rb.MovePosition(targetPosition);
        }
    }
}
