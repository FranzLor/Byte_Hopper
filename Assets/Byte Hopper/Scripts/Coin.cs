using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public int coinValue = 1;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player") 
        {
            Debug.Log("Coin Collected");

            Manager.instance.UpdateCoinCount(coinValue);

            Destroy(this.gameObject);
        }
    }
}
