using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainAudio : MonoBehaviour
{
    public AudioClip audioTrain = null;
    private AudioSource audioSource = null;
    private bool playerInTrigger = false;
    private bool audioPlayedForCurrentTrain = false;

    public TrafficLight trafficLight = null;

    void Start()
    {
        audioSource = this.GetComponent<AudioSource>();

    }

    void Update()
    {
        CheckTrainStatus();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            playerInTrigger = true;

            // player in area, play train audio, only if the light is active
            if (playerInTrigger && trafficLight.light.activeSelf && audioTrain != null)
            {
                PlayTrainAudio();
            }
        }

    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            playerInTrigger = false;

            // player leaves, stop audio, unhear the train
            if (audioSource.isPlaying)
            {
                audioSource.Stop();
            }
        }
    }

    void PlayTrainAudio()
    {
        if (audioSource != null && audioTrain != null)
        {
            audioSource.clip = audioTrain;
            audioSource.Play();

            // mark audio has been plaeyd for current train
            audioPlayedForCurrentTrain = true;
        }
    }

    public void CheckTrainStatus()
    {
        // if the player is in the trigger, the light is active (train is there), and the audio hasn't been played for the current train
        if (playerInTrigger && trafficLight.light.activeSelf && !audioSource.isPlaying)
        {
            PlayTrainAudio();
        }
        else if (!trafficLight.light.activeSelf)
        {
            // reset audio trigger when the train leaves
            audioPlayedForCurrentTrain = false;
        }
    }
}
