using UnityEngine;
using System.Collections;
using System.Globalization;
using UnityEngine.Assertions.Must;
using UnityEngine.UI;
using System;

public class PlayerScript : MonoBehaviour {
    public bool flipped = false;
    public Sprite SplatSprite;
    public Sprite scaredSprite;
    Vector2 StartSize;
    private bool splat = false;
    public float growthSpeed = 1f;
    public float deathSize = 2f;
    public float speed = 0.1f, orgSpeed;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    public float timer = 0;
    public float timeToAnimate = 5;
    private bool scared = false;
    public Sprite[] sprites;
    private Animator anim;
    [HideInInspector]
    public bool continueGrowing = true;
    public AudioSource pickupSoundSpeed;
    public AudioSource pickupSoundSGrowth;
    public AudioSource wallTouchSound;
    public Slider HealthSlider;
    private float percentBase;

    void Start() {
        orgSpeed = speed;
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        StartSize = transform.localScale;
        rb = GetComponent<Rigidbody2D>();
        //HealthSlider.maxValue = deathSize;
    }

    void Update() {
        if (GameManagerScript.instance.GameOver == false) {
            //Timer();
            if (splat == false && continueGrowing == true) {
                Grow();
            }
        }
    }

    void Timer() {
        timer += Time.deltaTime;
        if (timer > timeToAnimate) {
            timer = 0;
        }
    }

    void FixedUpdate() {
        if (GameManagerScript.instance.GameOver == false) {
            Movement();
        }
    }

    void OnCollisionEnter2D(Collision2D col) {
        if (col.gameObject.tag == "Wall") {
            wallTouchSound.Play();
            scared = true;
            growthSpeed = 2f;
            anim.SetBool("IsScared", true);
        }
    }

    void OnCollisionExit2D(Collision2D col) {
        if (col.gameObject.tag == "Wall") {
            wallTouchSound.Stop();
            scared = false;
            growthSpeed = 1f;
            anim.SetBool("IsScared", false);
        }
    }

    void Grow() {
        var newScaleX = transform.localScale.x + 0.0001f * (flipped ? -growthSpeed : growthSpeed);
        var newScaleY = transform.localScale.y + 0.0001f * growthSpeed;
        transform.localScale = new Vector3(newScaleX, newScaleY, 0);

        var maxSize = StartSize.x * 2;
        var sizeValue = transform.localScale.x / maxSize;
        HealthSlider.value = sizeValue;

        if (Mathf.Abs(transform.localScale.x) > (Mathf.Abs(StartSize.x * deathSize)) && transform.localScale.y > (StartSize.y * deathSize)) {
            spriteRenderer.sprite = SplatSprite;
            splat = true;
            anim.SetBool("IsDead", true);
            GameManagerScript.instance.GameOverFunction();
        }
    }

    void Movement() {
        if (Input.GetKey(KeyCode.A)) {
            transform.eulerAngles = new Vector2(0, 180);
            rb.MovePosition(rb.position + Vector2.left * speed * (flipped ? -1 : 1));
        }
        if (Input.GetKey(KeyCode.D)) {
            transform.eulerAngles = new Vector2(0, 0);
            rb.MovePosition(rb.position + Vector2.right * speed * (flipped ? -1 : 1));
        }
        if (Input.GetKey(KeyCode.W)) {
            transform.eulerAngles = new Vector2(0, 0);
            rb.MovePosition(rb.position + Vector2.up * speed);
        }
        if (Input.GetKey(KeyCode.S)) {
            transform.eulerAngles = new Vector2(0, 0);
            rb.MovePosition(rb.position + Vector2.down * speed);
        }
    }

    public void TakeDmg(float multiplier) {
        growthSpeed *= multiplier;
    }

    public void StopGrowingTemporarily(float waitTime) {
        continueGrowing = false;
        HealthSlider.GetComponentInChildren<Image>().color = Color.green;
        StartCoroutine(StopGrowingCoroutine(waitTime));
    }

    IEnumerator StopGrowingCoroutine(float waitTime) {
        while (true) {
            yield return new WaitForSeconds(waitTime);
            HealthSlider.GetComponentInChildren<Image>().color = Color.grey;
            continueGrowing = true;
            break;
        }
    }

    public void AddSpeedTemporarily(float speedMultiplier, float waitTime) {
        speed *= speedMultiplier;
        StartCoroutine(AddSpeedCoroutine(waitTime));
    }

    IEnumerator AddSpeedCoroutine(float waitTime) {
        while (true) {
            yield return new WaitForSeconds(waitTime);
            speed = orgSpeed;
            break;
        }
    }
}
