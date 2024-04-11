using UnityEngine;

public class CarWaypointController : MonoBehaviour
{
    public Transform[] waypoints;   // Array of waypoints to follow
    public float moveSpeed = 5f;    // Movement speed of the car
    public float turnSpeed = 5f;    // Turning speed of the car
    public float minDistance = 0.5f; // Minimum distance to consider a waypoint reached

    private int currentWaypointIndex = 0;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (waypoints.Length == 0)
            return;

        // Move towards the current waypoint
        Vector3 targetPosition = waypoints[currentWaypointIndex].position;
        Vector3 moveDirection = (targetPosition - transform.position).normalized;

        // Calculate steering angle towards the current waypoint
        float targetAngle = Vector3.Angle(transform.forward, moveDirection);
        float steeringAngle = Mathf.Clamp(targetAngle, -1f, 1f) * turnSpeed;

        // Apply steering to the wheels (assuming 4 wheels setup)
        ApplyWheelSteering(steeringAngle);

        // Apply forward movement
        rb.MovePosition(rb.position + transform.forward * moveSpeed * Time.fixedDeltaTime);

        // Check if reached the current waypoint
        if (Vector3.Distance(transform.position, targetPosition) < minDistance)
        {
            // Move to the next waypoint
            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
        }
    }

    void ApplyWheelSteering(float steeringAngle)
    {
       
         //frontLeftWheelCollider.steerAngle = steeringAngle;
         //frontRightWheelCollider.steerAngle = steeringAngle;
    }
}
