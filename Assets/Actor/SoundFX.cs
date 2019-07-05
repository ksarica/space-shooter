using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundFX : MonoBehaviour
{
    AudioSource audioSource;
    bool playStarted = false;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = this.GetComponent<AudioSource>();
        if (audioSource == null)
        {
            Destroy(this.gameObject);
        }
        PlaySound();
    }

    // Update is called once per frame
    void Update()
    {
        if (playStarted && !audioSource.isPlaying)
        {
            Destroy(this.gameObject);
        }
    }

    void PlaySound()
    {
        playStarted = true;
        audioSource.Play();
    }
}
