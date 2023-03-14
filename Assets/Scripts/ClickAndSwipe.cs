using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Require the object to have TrailRenderer and BoxCollider components
[RequireComponent(typeof(TrailRenderer), typeof(BoxCollider))]
public class ClickAndSwipe : MonoBehaviour
{

    private GameManager gameManager;
    private Camera cam;
    private Vector3 mousePos;
    private TrailRenderer trail;
    private BoxCollider col;

    //private AudioSource[] audio;

    // Set the swiping state to false by default
    private bool swiping = false;

    void Awake()
    {
        cam = Camera.main;

        trail = GetComponent<TrailRenderer>();
        col = GetComponent<BoxCollider>();

        // Disable the trail renderer by default
        trail.enabled = false;

        // Disable the box collider by default
        col.enabled = false;

        // Reference to Game Manager object in the scene
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }


    // Update is called once per frame
    void Update()
    {

        // Only allow swiping if the game is active
        if (gameManager.isGameActive)
        {
            // If the player holds the mouse button, start swiping
            if (Input.GetMouseButtonDown(0))
            {
                swiping = true;
                UpdateComponents();
            }
            // If the player releases the mouse button, stop swiping
            else if (Input.GetMouseButtonUp(0))
            {
                swiping = false;
                UpdateComponents();
            }
            // If the player is currently swiping, update the mouse position
            if (swiping)
            {
                UpdateMousePosition();
            }
        }
    }

    // Updates the position of the trail based on the mouse position
    void UpdateMousePosition()
    {
        // Get the mouse position and convert it to world space
        mousePos = cam.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10.0f));

        // Update the position of the trail to the mouse position
        transform.position = mousePos;
    }

    // Enables/disables the trail renderer and box collider based on the swiping state
    void UpdateComponents()
    {
        trail.enabled = swiping;
        col.enabled = swiping;
    }

    private void OnCollisionEnter(Collision collision)
    {
        // If the object that was hit has a Target component, destroy it
        if (collision.gameObject.GetComponent<Target>())
        {
            //Destroy the target
            collision.gameObject.GetComponent<Target>().DestroyTarget();
        }
    }
}
