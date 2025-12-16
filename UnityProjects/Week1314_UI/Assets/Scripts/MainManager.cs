using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// Manages the main menu scene, handling UI interactions and scene transitions.
/// This script controls the start button, exit button, and title display.
/// </summary>
public class MainManager : MonoBehaviour
{
    #region Constants
    
    // Scene name constant to avoid typos and make scene references easier to manage
    private const string Level1 = "Level_1";
    
    #endregion Constants
    
    #region UI References
    
    // Reference to the start button in the main menu
    public Button StartButton;
    
    // Reference to the title text component
    public TextMeshProUGUI Title;
    
    #endregion UI References

    #region Unity Lifecycle Methods
    
    private void Awake()
    {
        // Register the start button click event
        StartButton.onClick.AddListener(OnStartButton);
        
        // Set up the title text properties
        Title.text = "RPG Game";
        Title.color = Color.black;
    }

    private void OnDestroy()
    {
        // Remove all listeners from the start button to prevent memory leaks
        StartButton.onClick.RemoveAllListeners();
    }
    
    #endregion Unity Lifecycle Methods

    #region Button Event Handlers
    
    /// <summary>
    /// Called when the Start button is clicked.
    /// Loads the first level of the game.
    /// </summary>
    private void OnStartButton()
    {
        // Load scene
        SceneManager.LoadScene(Level1);
    }

    /// <summary>
    /// Called when the Exit button is clicked.
    /// Quits the application (works in builds, not in the Unity Editor).
    /// </summary>
    public void OnExitButton()
    {
        Debug.Log("Exit");
        
        // Close the application
        // Note: This will only work in a built game, not in the Unity Editor
        Application.Quit();
    }
    
    #endregion Button Event Handlers
}