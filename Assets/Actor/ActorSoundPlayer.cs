using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActorSoundPlayer : MonoBehaviour
{
    public bool shouldPlayOnStart = false;
    public void PlayOnStart()
    {
        if (shouldPlayOnStart)
        {
            AudioSource audioSource = this.GetComponent<AudioSource>();
            if (audioSource != null)
            {
                audioSource.Play();
            }
        }
    }
}
