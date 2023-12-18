using UnityEngine;

public class NPCMovement : MonoBehaviour
{
    // public members
    public float moveSpeed = 3f;
    public Transform[] waypoints; // Array to store the waypoints or positions the NPC should move to
    public Animator animator; // Animator for NPC animations

    // private members
    private int currentWaypointIndex = 0;
    private Vector3 initialPosition;

    void Start()
    {
        initialPosition = transform.position;
    }

    void Update()
    {
        MoveTowardsWaypoint();
        UpdateAnimations();
    }

    void MoveTowardsWaypoint()
    {
        Transform currentWaypoint = waypoints[currentWaypointIndex];

        // Move towards the waypoint
        transform.position = Vector3.MoveTowards(transform.position, currentWaypoint.position, moveSpeed * Time.deltaTime);

        // Check if the NPC has reached the current waypoint
        if (Vector3.Distance(transform.position, currentWaypoint.position) < 0.1f)
        {
            // Move to the next waypoint in the array
            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;

            // If the NPC reached the last waypoint, reset to the initial position
            if (currentWaypointIndex == 0)
            {
                transform.position = initialPosition;
            }
        }
    }

    void UpdateAnimations()
    {
        // Calculate movement direction for animation
        Vector2 movement = (waypoints[currentWaypointIndex].position - transform.position).normalized;

        // Update animator parameters
        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.magnitude);
    }
}

