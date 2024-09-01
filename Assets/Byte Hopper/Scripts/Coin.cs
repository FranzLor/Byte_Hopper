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

            //TODO: add coin to players score
            //manager -> coin count

            Destroy(this.gameObject);
        }
    }
}
