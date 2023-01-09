using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetSound : MonoBehaviour
{
    public float volumeMultiplier = 1;
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    private void Start() {
        ChangeVolume();
        audioSource.Stop();
        audioSource.Play();
    }

    public void ChangeVolume()
    {
        audioSource.volume = GameManager.instance.soundVolume * volumeMultiplier;
    }

    public void StartSound()
    {
        ChangeVolume();
        audioSource.Stop();
        audioSource.PlayScheduled(AudioSettings.dspTime + 0.1);
    }
}
