using UnityEngine;

public class TrailSound : MonoBehaviour
{
    // Reference to TrailRenderer
    private TrailRenderer trail;

    // Array of AudioSources
    private AudioSource[] audioArray;

    // Array of AudioClips
    public AudioClip[] soundArray;

    void Start()
    {
        // Get TrailRenderer component and create new AudioSource for each AudioClip in soundArray
        trail = GetComponent<TrailRenderer>();
        audioArray = new AudioSource[soundArray.Length];
        for (int i = 0; i < soundArray.Length; i++)
        {
            audioArray[i] = gameObject.AddComponent<AudioSource>();
            audioArray[i].clip = soundArray[i];
        }
    }

    void Update()
    {
        // If TrailRenderer is enabled, play all AudioSources that aren't currently playing
        if (trail.enabled)
        {
            foreach (AudioSource audio in audioArray)
            {
                if (!audio.isPlaying)
                {
                    audio.Play();
                }
            }
        }
        // If TrailRenderer is disabled, stop all AudioSources that are currently playing
        else
        {
            foreach (AudioSource audio in audioArray)
            {
                if (audio.isPlaying)
                {
                    audio.Stop();
                }
            }
        }
    }
}
