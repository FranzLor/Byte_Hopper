using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public int coinValue = 1;

    // used to make sure audio plays first before audio is destroyed
    public GameObject coin = null;

    public AudioClip audioClip = null;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player") 
        {
            Debug.Log("Coin Collected");

            Manager.instance.UpdateCoinCount(coinValue);

            // disable coin obj
            coin.SetActive(false);

            this.GetComponent<AudioSource>().PlayOneShot(audioClip);

            // destroy coin after audio is done playing
            Destroy(this.gameObject, 0.45f);
        }
    }
}
