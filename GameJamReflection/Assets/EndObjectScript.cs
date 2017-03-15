using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EndObjectScript : MonoBehaviour
{
    public bool PlayerHasTouched = false;
    public EndObjectScript OtherEndObject; 
    public Sprite WinSprite;

    void OnTriggerEnter2D(Collider2D c)
    {
        if (c.tag == "Player")
        {
            PlayerHasTouched = true;
            GetComponent<SpriteRenderer>().sprite = WinSprite;
            if (OtherEndObject.PlayerHasTouched)
            {
                GameManagerScript.instance.Win();
                //SceneManager.LoadScene(2);
            }
        }
    }

}
