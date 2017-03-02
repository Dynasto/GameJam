using System;
using UnityEngine;
using System.Collections;

public class PickupScript : MonoBehaviour
{
    public float ChangeSize = 2f;
    public float WaitTime = 2f;
    public float SpeedMultiplier = 2f;

    void OnTriggerEnter2D(Collider2D c)
    {
        if (transform.tag == "PickupStopGrowing")
        {
            c.GetComponent<PlayerScript>().StopGrowingTemporarily(WaitTime);
        }
        else if (transform.tag == "PickupAddSpeed")
        {
            c.GetComponent<PlayerScript>().AddSpeedTemporarily(SpeedMultiplier, 5f);
        }
        Destroy(gameObject);
    }
}
