using UnityEngine;
using UnityEngine.SceneManagement; // For loading scenes
using TMPro; // For TextMeshProUGUI

public class WinLoseManager : MonoBehaviour
{
    public TextMeshProUGUI winnerText; // Link this to the WinnerText UI in Inspector

    // This static string will store the message passed from the GameManager
    public static string lastWinnerMessage = "";

    void Start()
    {
        // Display the message received from the GameManager
        if (winnerText != null)
        {
            winnerText.text = lastWinnerMessage;
        }
        // Reset time scale in case the game was frozen
        Time.timeScale = 1;
    }

    public void PlayAgain()
    {
        Debug.Log("Play Again button clicked!");
        // Load the StartMenu scene to begin a new game
        SceneManager.LoadScene("StartMenu");
    }

    public void QuitGame()
    {
        Debug.Log("Quit Game button clicked!");
        Application.Quit(); // Quits the application (only works in a built game)

        // If running in the Unity Editor, stop playing
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}