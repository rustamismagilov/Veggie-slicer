using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DifficultyButton : MonoBehaviour
{
    // reference to the difficulty selection buttons
    private Button button;

    private GameManager gameManager;

    // The difficulty level associated with the buttons
    public int difficulty;

    // Start is called before the first frame update
    void Start()
    {
        // Get the Button component of the object
        button = GetComponent<Button>();

        // Find the game manager object and get the GameManager component
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();

        // Add a listener to the button so that it can call the SetDifficulty function when clicked
        button.onClick.AddListener(SetDifficulty);
    }

    // Sets the game difficulty level when the button is clicked
    void SetDifficulty()
    {
        // Print a debug message to the console indicating which button was clicked
        Debug.Log(button.gameObject.name + " was Clicked");

        // Call the StartGame function of the GameManager object, passing in the difficulty level of the button as a parameter
        gameManager.StartGame(difficulty);
    }
}
