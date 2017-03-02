using UnityEngine;
using System.Collections;

public class EndObjectScript : MonoBehaviour
{
    public bool PlayerHasTouched = false;
    public EndObjectScript OtherEndObject;

    void OnTriggerEnter2D(Collider2D c)
    {
        if (c.tag == "Player")
        {
            PlayerHasTouched = true;
            if (OtherEndObject.PlayerHasTouched)
            {
                //SceneManager.LoadScene(2);
            }
        }
    }

}
