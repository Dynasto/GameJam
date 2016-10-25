using UnityEngine;
using System.Collections;

public class PlayerScript : MonoBehaviour {
    public bool flipped = false;
    public Sprite SplatSprite;
    Vector2 StartSize;
    private bool splat = false;
    private float growthSpeed = 1f;

    void Start() {
        StartSize = transform.localScale;
    }

    void Update() {
        if (GameManagerScript.instance.GameOver == false) {
            Movement();
            if (splat == false) {
                Grow();
            }
        }
    }

    void OnTriggerStay2D(Collider2D col) {
        if (col.gameObject.tag == "Wall") {
            growthSpeed = 2f;
        }
    }

    void OnTriggerExit2D(Collider2D col) {
        if (col.gameObject.tag == "Wall") {
            growthSpeed = 1f;
        }
    }

    void Grow() {//magic numbers ftw
        transform.localScale = new Vector3(transform.localScale.x + ((flipped ? -0.0001f : 0.0001f) * growthSpeed), transform.localScale.y + 0.0001f * growthSpeed, transform.localScale.z + 0.0001f);
        if (transform.localScale.x > (StartSize.x * 2) && transform.localScale.y > (StartSize.y * 2)) {
            GetComponent<SpriteRenderer>().sprite = SplatSprite;
            splat = true;
            GameManagerScript.instance.GameOver = true;
        }
    }

    void Movement() {
        if (Input.GetKey(KeyCode.A)) {
            transform.eulerAngles = new Vector2(0, 180);
            transform.position = ChangePositionVector2(-0.1f, 0, transform.position);
        }
        if (Input.GetKey(KeyCode.D)) {
            transform.eulerAngles = new Vector2(0, 0);
            transform.position = ChangePositionVector2(0.1f, 0, transform.position);
        }
        if (Input.GetKey(KeyCode.W)) {
            transform.eulerAngles = new Vector2(0, 0);
            transform.position = ChangePositionVector2(0, 0.1f, transform.position);
        }
        if (Input.GetKey(KeyCode.S)) {
            transform.eulerAngles = new Vector2(0, 0);
            transform.position = ChangePositionVector2(0, -0.1f, transform.position);
        }
    }

    Vector2 ChangePositionVector2(float x, float y, Vector2 oldVector2) {
        return new Vector2(oldVector2.x + x, oldVector2.y + y);
    }
}
