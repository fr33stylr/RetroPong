using UnityEngine;
using TMPro; // <-- IMPORTANT: Add this line for TextMeshPro UI
using UnityEngine.SceneManagement; // Add for potential scene loading for win screen later

public class GameManager : MonoBehaviour
{
    // This is our Singleton instance. It makes the GameManager easily accessible from other scripts.
    public static GameManager Instance;

    // Variables to hold the scores
    public int player1Score = 0;
    public int player2Score = 0;

    // References to our UI TextMeshPro objects for displaying scores
    public TextMeshProUGUI player1ScoreText; // Text for Player 1's score
    public TextMeshProUGUI player2ScoreText; // Text for Player 2's score

    public int winningScore = 10; // The score needed to win the game

    // Awake is called when the script instance is being loaded, even before Start
    void Awake()
    {
        // Implement the Singleton pattern:
        // If an instance of GameManager doesn't exist yet, this is it.
        if (Instance == null)
        {
            Instance = this; // Set this instance as the singleton
            // Optional: DontDestroyOnLoad(gameObject); // Uncomment if you want GameManager to persist across multiple game levels
        }
        else
        {
            // If another GameManager already exists, destroy this one to ensure only one.
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        UpdateScoreUI(); // Initialize the score display when the game starts
    }

    // Function called when Player 1 scores
    public void Player1Scored()
    {
        player1Score++; // Increment Player 1's score
        UpdateScoreUI(); // Update the UI display
        CheckForWin(); // Check if Player 1 has won
    }

    // Function called when Player 2 scores
    public void Player2Scored()
    {
        player2Score++; // Increment Player 2's score
        UpdateScoreUI(); // Update the UI display
        CheckForWin(); // Check if Player 2 has won
    }

    // Updates the text on the UI to show the current scores
    void UpdateScoreUI()
    {
        // Only update if the text objects are assigned in the Inspector
        if (player1ScoreText != null)
        {
            player1ScoreText.text = player1Score.ToString(); // Convert int to string for display
        }
        if (player2ScoreText != null)
        {
            player2ScoreText.text = player2Score.ToString(); // Convert int to string for display
        }
    }

    // Checks if either player has reached the winning score
    void CheckForWin()
    {
        if (player1Score >= winningScore)
        {
            Debug.Log("Player 1 Wins!");
            WinLoseManager.lastWinnerMessage = "Player 1 Wins!"; // Set the message
            SceneManager.LoadScene("WinLoseScene");
        }
        else if (player2Score >= winningScore)
        {
            Debug.Log("Player 2 Wins!");
            WinLoseManager.lastWinnerMessage = "Player 2 Wins!"; // Set the message
            SceneManager.LoadScene("WinLoseScene");
        }
    }

    // Call this function to reset scores if you restart the game within the scene
    public void ResetScores()
    {
        player1Score = 0;
        player2Score = 0;
        UpdateScoreUI();
        Time.timeScale = 1; // Unfreeze game if it was paused
    }
}