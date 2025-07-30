using UnityEngine;

public class P2 : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float minY = -3f; // Adjust these values in Inspector after visual testing
    public float maxY = 3f;  // Adjust these values in Inspector after visual testing

    private Rigidbody2D rb; // Add a reference to the paddle's Rigidbody2D

    void Start()
    {
        // Get the Rigidbody2D component when the script starts
        rb = GetComponent<Rigidbody2D>();

        // IMPORTANT: Ensure your paddle's Rigidbody2D Body Type is set to 'Kinematic' in the Inspector.
        // Also ensure Gravity Scale is 0 and Freeze Rotation Z is checked.
    }

    // Use FixedUpdate for physics-related movement.
    // It runs at a fixed timestep, which is consistent with the physics engine.
    void FixedUpdate()
    {
        Vector2 currentPosition = rb.position; // Get the paddle's current Rigidbody position

        // Calculate desired Y movement based on input
        if (Input.GetKey(KeyCode.UpArrow))
        {
            currentPosition.y += moveSpeed * Time.fixedDeltaTime;
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            currentPosition.y -= moveSpeed * Time.fixedDeltaTime;
        }

        // Clamp the new position
        currentPosition.y = Mathf.Clamp(currentPosition.y, minY, maxY);

        // Move the Rigidbody to the new position
        rb.MovePosition(currentPosition);
    }

    // We no longer need the Update method for movement, as it's now in FixedUpdate
    // You can remove the empty Update method or leave it if you plan to use it for non-physics logic later.
    // void Update()
    // {
    //     // Keep this empty
    // }
}