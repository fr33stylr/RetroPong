using UnityEngine; // Required for basic Unity functionalities

// No need for System.Collections or System.Collections.Generic unless you use them later.
// We also don't need TMPro or SceneManagement in the Ball script itself,
// as the GameManager handles those concerns.

public class Ball : MonoBehaviour
{
    public Rigidbody2D rb; // Reference to the Ball's Rigidbody2D component
    public float startingSpeed = 5f; // Public variable for initial speed, adjust in Inspector

    // No maxBounceAngle added, as per your request.

    // Start is called before the first frame update
    void Start()
    {
        // Get the Rigidbody2D component if it's not already assigned in the Inspector
        if (rb == null)
        {
            rb = GetComponent<Rigidbody2D>();
        }

        // Immediately launch the ball when the game starts (or when the scene loads)
        LaunchBall();
    }

    // This function handles the initial launch of the ball
    void LaunchBall()
    {
        // 1. Determine Horizontal Direction (Left or Right)
        // UnityEngine.Random.value returns a float between 0.0 and 1.0 (inclusive).
        // If it's >= 0.5, we go right (1); otherwise, we go left (-1).
        float xDirection = (UnityEngine.Random.value >= 0.5f) ? 1f : -1f;

        // 2. Determine Vertical Direction (slight up or down, or flat)
        // UnityEngine.Random.Range with float arguments returns a float within the range.
        // Using a range like -0.5f to 0.5f prevents the ball from always going
        // straight horizontally or too sharply vertically.

        // *** MODIFIED LINE: Changed range to encourage more verticality at start ***
        float yDirection = UnityEngine.Random.Range(-0.8f, 0.8f);

        // *** NEW LINES: Ensures ball never starts perfectly horizontal (or nearly so) ***
        if (Mathf.Abs(yDirection) < 0.1f) // If yDirection is very close to zero
        {
            yDirection = (yDirection >= 0) ? 0.1f : -0.1f; // Nudge it to a small non-zero value
        }
        // *** END NEW LINES ***


        // 3. Set the Ball's Velocity
        // Create a new Vector2 with the chosen directions.
        // .normalized makes the length of the vector exactly 1, so our speed is consistent.
        // Then, multiply by startingSpeed to get the desired movement speed.
        rb.velocity = new Vector2(xDirection, yDirection).normalized * startingSpeed;
    }

    // OnTriggerEnter2D is called when this collider (Ball's) enters another trigger collider (Goal's)
    void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the collider it entered has the "LeftGoal" tag
        if (other.CompareTag("LeftGoal"))
        {
            // If it hit the LeftGoal, Player 2 scores (because Player 1 failed to block)
            // We access the GameManager's singleton instance and call its scoring method.
            if (GameManager.Instance != null) // Always good to check for null before accessing singletons
            {
                GameManager.Instance.Player2Scored();
            }
            ResetBall(); // Reset the ball's position and relaunch for the next round
        }
        // Check if the collider it entered has the "RightGoal" tag
        else if (other.CompareTag("RightGoal"))
        {
            // If it hit the RightGoal, Player 1 scores (because Player 2 failed to block)
            if (GameManager.Instance != null)
            {
                GameManager.Instance.Player1Scored();
            }
            ResetBall(); // Reset the ball's position and relaunch
        }
    }

    // OnCollisionEnter2D is called when this collider (Ball's) physically hits another non-trigger collider (Paddle's, Walls')
    void OnCollisionEnter2D(Collision2D collision)
    {
        // Check if the object it collided with has the "Paddle" tag
        if (collision.gameObject.CompareTag("Paddle"))
        {
            // No dynamic paddle bounce logic added, as per your request.

            // Optional: Increase speed slightly after hitting a paddle for more dynamic gameplay
            rb.velocity *= 1.05f; // Increase current velocity by 5%
        }
        // You could add more collision logic here, e.g., for top/bottom walls if they bounce the ball.
    }


    // This function resets the ball to the center and prepares it for a new launch
    void ResetBall()
    {
        rb.velocity = Vector2.zero; // Stop any current movement of the ball
        transform.position = Vector2.zero; // Move the ball to the exact center of the screen (0,0)

        // Use Invoke to call LaunchBall after a short delay (e.g., 1 second).
        // This gives players a moment to react after a score and prevents immediate re-scoring.
        Invoke("LaunchBall", 1f);
    }

    // Update is called once per frame (not used for movement or physics in this simple setup)
    void Update()
    {
        // Keep Update empty for now as all logic is in Start, OnTriggerEnter2D, OnCollisionEnter2D, and helper functions.
    }
}