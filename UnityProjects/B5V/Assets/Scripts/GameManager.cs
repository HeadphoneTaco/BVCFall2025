using UnityEngine;

/// <summary>
/// Manages the overall game state and transitions between different game phases.
/// Handles user input for starting, playing, pausing and ending the game.
/// </summary>
public class GameManager : MonoBehaviour
{
    // Define Enum to represent game sates
    private enum GameState
    {
        Start,      // Initial state when game first loads
        Playing,    // Active gameplay state
        Paused,     // Game is paused (frozen in time)
        GameOver    // End state after losing/completing
    }
    // Variable to store current game state
    private GameState _currentGameState = GameState.Start;

    // Count space bar presses to trigger game over
    private int _spaceBarPressCount;
    // Maximum number of space presses before game over
    private const int MaxPresses = 3;

    #region Unity Functions
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    // Initializes the game to the Start state
    private void Start()
    {
        // Set to Start state when game begins
        StartGame();
    }

    // Update is called once per frame
    // Monitors input based on current game state and handles state transitions.
    private void Update()
    {
        // Check input only when in Start state
        if (_currentGameState == GameState.Start)
        {
            BeginPlaying();
        }

        // Check input only when in Playing state
        // This prevents input from being processed during other states
        if (_currentGameState == GameState.Playing)
        {
            CheckGameInput();
        }

        // ESC key to pause/resume - works in both playing and pause states
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            // If currently playing, pause the game
            if (_currentGameState == GameState.Playing)
            {
                // Pause Menu
                PauseGame();
            }
            // If currently paused, resume the game
            else if (_currentGameState == GameState.Paused)
            {
                ResumeGame();
            }
        }

        // Check input only when in GameOver state
        if (_currentGameState == GameState.GameOver || _currentGameState == GameState.Playing)
        {
            Restart();
        }
    }
    
    #endregion Unity Functions

    #region Methods

    /// <summary>
    /// Initializes the game start state and displays instructions.
    /// </summary>
    private void StartGame()
    {
        // Ensure state is set to Start
        _currentGameState = GameState.Start;
        
        Debug.Log("=== Game Start ===");
        Debug.Log("Press P to begin playing!");
    }
    
    /// <summary>
    /// Transitions from Start state to playing state.
    /// </summary>
    private void BeginPlaying()
    {
        // Detect P key Press
        if (Input.GetKeyDown(KeyCode.P))
        {
            // Change state to playing
            _currentGameState = GameState.Playing;
            
            // Reset space bar press counter for new game session
            _spaceBarPressCount = 0;
            
            Debug.Log("=== Now Playing ===");
            Debug.Log("Press SPACE 3 times to trigger Game Over!");
            Debug.Log("Press ESC to pause");
        }
    }

    /// <summary>
    /// Monitors and processes game input during the playing state.
    /// Tracks space bar pressess and handles P key for starting gameplay.
    /// </summary>
    private void CheckGameInput()
    {
        // Detect space bar press
        if (!Input.GetKeyDown(KeyCode.Space))
        {
            return;
        }
        
        // Increment the press counter
        _spaceBarPressCount++;
        Debug.Log($"Space bar pressed! Count: {_spaceBarPressCount}/{MaxPresses}");
        
        // Trigger game over when reaching maximun presses
        if (_spaceBarPressCount >= MaxPresses)
        {
            TriggerGameOver();
        }
    }

    /// <summary>
    /// Handles game over state when player presses space 3 times.
    /// 
    /// </summary>
    private void TriggerGameOver()
    {
        // Change state to GameOver
        _currentGameState = GameState.GameOver;
        
        Debug.Log("=== Game Over ===");
        Debug.Log($"You pressed space {_spaceBarPressCount} times!");
        Debug.Log("Press R to restart");
        
        // Freeze time during game over screen
        // Time.timeScale = 0;
    }

    /// <summary>
    /// Pause the game by freezing time and changing state to Paused
    /// Displays pause menu instructions
    /// </summary>
    private void PauseGame()
    {
        // Change state to paused
        _currentGameState = GameState.Paused;

        // Freeze time (Stops all time-based updates)
        Time.timeScale = 0f;
        
        Debug.Log("=== Pause Menu ===");
        Debug.Log("Press ESC to resume");
    }

    /// <summary>
    /// Resumes the game by restoring normal time flow and returning to playing state
    /// </summary>
    private void ResumeGame()
    {
        // Change to playing state
        _currentGameState = GameState.Playing;
        
        // Restore normal time flow
        Time.timeScale = 1f;
        
        Debug.Log("=== Resume Menu ===");
    }

    /// <summary>
    /// Resets the game to its initial state, allowing the player to restart.
    /// Restores normal time flow and resets all counters.
    /// </summary>
    private void Restart()
    {
        if (Input.GetKeyDown(KeyCode.R) == false)
        {
            return;
        }
        
        Debug.Log("=== RESTART ===");
        
        // Reset the space bar press counter to zero
        _spaceBarPressCount = 0;
        
        // Display start game Instructions
        StartGame();
    }

    #endregion Methods
}