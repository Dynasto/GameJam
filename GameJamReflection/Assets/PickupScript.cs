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
        var script = c.GetComponent<PlayerScript>();
        if (transform.tag == "PickupStopGrowing")
        {
            script.StopGrowingTemporarily(WaitTime);
            script.pickupSoundSGrowth.Play();
        }
        else if (transform.tag == "PickupAddSpeed")
        {
            script.AddSpeedTemporarily(SpeedMultiplier, 5f);
            script.pickupSoundSpeed.Play();
        } 
        Destroy(gameObject);
    }
}
