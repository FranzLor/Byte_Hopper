using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Electric : MonoBehaviour
{
    bool hitElectricity = false;

    void OnTriggerStay(Collider other)
    {
        if (hitElectricity) return;

        if (other.tag == "Player")
        {
            PlayerController playerController = other.GetComponent<PlayerController>();

            hitElectricity = true;

            playerController.Electrified();
        }
    }
}
