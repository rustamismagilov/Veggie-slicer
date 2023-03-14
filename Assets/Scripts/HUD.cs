using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HUD : MonoBehaviour
{
    // An array of GameObjects that represent the live icons.
    public GameObject[] liveIcons;

    // A method to update the number of live icons that are shown based on the current number of lives
    public void UpdateLives(int lives)
    {
        for (int i = 0; i < liveIcons.Length; i++)
        {
            // If the index is less than the current number of lives, show the corresponding live icon
            if (i < lives)
            {
                liveIcons[i].SetActive(true);
            }
            // Otherwise, hide the corresponding live icon
            else
            {
                liveIcons[i].SetActive(false);
            }
        }
    }
}
