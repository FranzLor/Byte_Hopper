using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainAudio : MonoBehaviour
{
    public string trainAlarmSoundName = "Train";
    private bool playerInTrigger = false;
    private bool isAlarmPlaying = false;

    // store the duration of the train alarm sound
    private float audioClipLength = 0.0f; 
    private Coroutine trainAudioCoroutine;

    public TrafficLight trafficLight = null;

    void Start()
    {
        // get the duration of the train alarm sound from AudioManager
        Sound trainAlarm = System.Array.Find(AudioManager.instance.sfxSounds, sound => sound.name == trainAlarmSoundName);
        if (trainAlarm != null)
        {
            audioClipLength = trainAlarm.clip.length;
        }
        else
        {
            Debug.LogWarning("Train alarm sound not found in AudioManager!");
        }
    }

    void Update()
    {
        CheckTrainStatus();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInTrigger = true;
            CheckTrainStatus();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInTrigger = false;
            StopTrainAudio();
        }
    }

    void PlayTrainAudio()
    {
        if (!isAlarmPlaying && playerInTrigger && trafficLight.light.activeSelf && AudioManager.instance != null)
        {
            AudioManager.instance.PlaySFX(trainAlarmSoundName);
            isAlarmPlaying = true;

            // start coroutine to reset after the clip finishes playing
            if (trainAudioCoroutine != null)
            {
                StopCoroutine(trainAudioCoroutine);
            }
            trainAudioCoroutine = StartCoroutine(ResetTrainAudioAfterDelay(audioClipLength));
        }
    }

    void StopTrainAudio()
    {
        // stop the train audio if the player leaves the trigger or the train passes
        if (playerInTrigger == false && isAlarmPlaying)
        {
            AudioManager.instance.StopSFX(trainAlarmSoundName);
            isAlarmPlaying = false;
        }
    }

    IEnumerator ResetTrainAudioAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        isAlarmPlaying = false;
    }

    void CheckTrainStatus()
    {
        if (playerInTrigger && trafficLight.light.activeSelf)
        {
            PlayTrainAudio();
        }
        else if (!trafficLight.light.activeSelf)
        {
            StopTrainAudio();
        }
    }
}
