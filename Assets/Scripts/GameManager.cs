using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // List of all possible targets that can spawn
    public List<GameObject> targets;

    // The default rate at which targets will spawn
    private float spawnRate = 1.5f;

    // The player's score
    private int score;

    // Text object that displays the score
    public TextMeshProUGUI scoreText;

    // Text object that displays "Game Over" when the game ends
    public TextMeshProUGUI gameOverText;

    // Flag that indicates whether the game is currently active
    public bool isGameActive;

    // Reference to the button object that restarts the game
    public Button restartButton;

    // Title screen object
    public GameObject titleScreen;

    // Reference to the score text object in the scene
    public GameObject scoreUIText;

    // Heart icons object
    public GameObject heartIcons;

    private HUD hud;

    // The default amout of player's remaining lives
    public int lives = 3;

    // Reference to the pause screen object in the scene
    public GameObject pauseScreen;

    // Flag that indicates whether the game is currently paused
    private bool paused;

    // Audio source for background music
    public AudioSource japan;

    private void Awake()
    {
        hud = FindObjectOfType<HUD>();
    }

    // Start is called before the first frame update
    void Start()
    {
        hud.UpdateLives(lives);
    }

    // Coroutine that spawns targets
    IEnumerator SpawnTarget()
    {
        while (isGameActive)
        {
            yield return new WaitForSeconds(spawnRate);
            int index = Random.Range(0, targets.Count);
            Instantiate(targets[index]);
        }
    }

    // Updates the score variable and the score text in the scene
    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "Score: " + score;
    }

    // Handle the end of the game
    public void GameOver()
    {
        restartButton.gameObject.SetActive(true);
        gameOverText.gameObject.SetActive(true);
        isGameActive = false;
        SubstractLives();

        // Stop the background music if the game ends
        if (japan.isPlaying)
        {
            japan.Stop();
        }
    }

    // reload the scene if restart was pressed
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Start the game with a given difficulty level
    public void StartGame(int difficulty)
    {

        isGameActive = true;
        score = 0;
        spawnRate /= difficulty;

        // Start spawning targets and update the score
        StartCoroutine(SpawnTarget());
        UpdateScore(0);

        // Hide the title screen and show the score text and heart icons
        titleScreen.SetActive(false);
        scoreUIText.SetActive(true);
        heartIcons.SetActive(true);
    }

    // this method for subtracts a life from the player
    public bool SubstractLives()
    {
        if (lives > 0)
        {
            lives--;
            Debug.Log("Lives left: " + lives);
            hud.UpdateLives(lives);
        }
        return true;
    }

    // Check for pause input
    void CheckForPause()
    {
        if (!paused)
        {
            // if game paused then show the "paused" screen and freeze the game with Time.timeScale = 0;
            paused = true;
            pauseScreen.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            // if game unpaused then hide the "paused" screen and unfreeze the game with Time.timeScale = 1;
            paused = false;
            pauseScreen.SetActive(false);
            Time.timeScale = 1;
        }
    }

    void Update()
    {
        //Check if the user has pressed the Escape key
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //then call CheckForPause method
            CheckForPause();
        }

        // this if-statement prevents the ability to pause the game while staying in the main menu (actually, while titleScreen is active)
        if (titleScreen.activeSelf == true)
        {
            pauseScreen.SetActive(false);
        }
    }

}
