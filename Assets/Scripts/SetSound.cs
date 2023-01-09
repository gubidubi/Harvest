using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetSound : MonoBehaviour
{
    public float volumeMultiplier = 1;
    private AudioSource audioSource;

    private void Start() {
        audioSource = gameObject.GetComponent<AudioSource>();
        ChangeVolume();
    }

    public void ChangeVolume()
    {
        audioSource.volume = GameManager.instance.soundVolume * volumeMultiplier;
    }

    public void StartSound()
    {
        ChangeVolume();
        audioSource.Play();
    }
}
