using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour {
    public GameObject[] Waypoints;
    public Sprite[] Sprites;
    GameObject currentWaypoint;
    public float multiplier;
    public AudioSource HurtSound;

    void Start() {
        currentWaypoint = Waypoints[Random.Range(0, Waypoints.Length)];
    }

    void Update() {
        Move();
    }

    void Move() {
        if (GameManagerScript.instance.GameOver == false) {
            transform.position = Vector2.MoveTowards(transform.position, currentWaypoint.transform.position, Time.deltaTime * 5);

            if (Vector2.Distance(transform.position, currentWaypoint.transform.position) < 0.1f) {
                currentWaypoint = Waypoints[Random.Range(0, Waypoints.Length)];
                if (Sprites.Length > 0) {
                    GetComponent<SpriteRenderer>().sprite = Sprites[Random.Range(0, Sprites.Length)];
                }
            }
        }
    }

    void OnTriggerEnter2D(Collider2D o) {
        if (o.tag == "Player") {
            HurtSound.Play();
            o.GetComponent<PlayerScript>().TakeDmg(multiplier);
        }
    }
}
