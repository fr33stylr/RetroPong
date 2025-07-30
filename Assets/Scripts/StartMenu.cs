using UnityEngine;
using UnityEngine.SceneManagement; // <-- IMPORTANT: Add this line to use SceneManager

public class StartMenu : MonoBehaviour
{
    // This function will be called when the "Start Game" button is clicked
    public void StartGame()
    {
        Debug.Log("Start Game button clicked!"); // This will show in the Console for testing

        // Load your main game scene. Make sure "SampleScene" is the correct name!
        SceneManager.LoadScene("SampleScene");
    }

    // Optional: A function for a Quit button (won't work in editor, only in a built game)
    public void QuitGame()
    {
        Debug.Log("Quit Game button clicked!");
        Application.Quit(); // Closes the application
    }
}