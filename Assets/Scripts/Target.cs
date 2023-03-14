using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Target : MonoBehaviour
{
    // defines rigidbody of the targets
    private Rigidbody targetRb;

    // declare the minimum and the maximum speed of the targets
    private float minSpeed = 12;
    private float maxSpeed = 16;

    // Maximum torque of the targets
    private float maxTorque = 12;

    // Range of x positions the targets can spawn in
    private float xRange = 4;

    // Y-axis range where the targets can spawn in
    private float ySpawnPos = -3;

    private GameManager gameManager;

    // defines the value of each point for every target
    public int pointValue;

    // Particle system for explosion effect
    public ParticleSystem explosionParticle;



    // Start is called before the first frame update
    void Start()
    {
        // Get the Rigidbody of the target and add force and torque to it
        targetRb = GetComponent<Rigidbody>();
        targetRb.AddForce(RandomForce(), ForceMode.Impulse);
        targetRb.AddTorque(RandomTorque(), RandomTorque(), RandomTorque(), ForceMode.Impulse);

        // Set the position of the target
        transform.position = RandomSpawnPos();

        // Get the GameManager object
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    // Generate a random force
    Vector3 RandomForce()
    {
        return Vector3.up * Random.Range(minSpeed, maxSpeed);
    }

    // Generate a random torque
    float RandomTorque()
    {
        return Random.Range(-maxTorque, maxTorque);
    }

    // Generate a random spawn position
    Vector3 RandomSpawnPos()
    {
        return new Vector3(Random.Range(-xRange, xRange), ySpawnPos);
    }

    private void OnMouseDown()
    {
        // Check if the game is active and the target is not tagged as "Bad" then destroy the target and update the score
        if (gameManager.isGameActive)
        {
            Destroy(gameObject);
            Instantiate(explosionParticle, transform.position, explosionParticle.transform.rotation);

            gameManager.UpdateScore(pointValue);
        }

        // If the target is tagged as "Bad" and the game is active then subtract a life
        if (gameObject.CompareTag("Bad") && gameManager.isGameActive)
        {
            gameManager.SubstractLives();
        }
    }

    // Destroy the target and update the score if the game is active
    public void DestroyTarget()
    {
        if (gameManager.isGameActive)
        {
            Destroy(gameObject);
            Instantiate(explosionParticle, transform.position, explosionParticle.transform.rotation);
            gameManager.UpdateScore(pointValue);
        }

        // If the target is tagged as "Bad" and the game is active then subtract a life
        if (gameObject.CompareTag("Bad") && gameManager.isGameActive)
        {
            gameManager.SubstractLives();
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Check if the player has run out of lives
        if (gameManager.lives == 0)
        {
            gameManager.GameOver();
        }
    }
}
