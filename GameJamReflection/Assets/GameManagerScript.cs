using System;
using UnityEngine;
using System.Collections;
using System.Net;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class GameManagerScript : MonoBehaviour {
    public static GameManagerScript instance;
    public GameObject[] Players;
    public Text GameOverText;
    public Text WinText;
    public Button GameOverButton;
    public Button WinButton;
    [HideInInspector]
    public bool GameOver = false;
    public AudioSource GameSound;
    public AudioSource GameOverSound;
    public AudioSource WinSound;

    void Start() {
        instance = this;
        GameOverText.enabled = false;
        GameOverButton.gameObject.SetActive(false);
        WinButton.gameObject.SetActive(false);
        //Cursor.visible = false;
    }

    void Update() {
        if (Input.GetKey(KeyCode.Space)) {
            GameOverFunction();
        }
        if (Input.GetKey(KeyCode.N)) {
            Win();
        }
    }

    public void GameOverFunction() {
        GameOver = true;
        foreach (var player in Players) {
            player.GetComponent<Animator>().SetBool("IsGameOver", true);
        }
        GameOverText.enabled = true;
        GameOverButton.gameObject.SetActive(true);
        Cursor.visible = true;
        GameOverSound.Play();
        GameSound.Stop();
    }

    public void Win() {
        GameOver = true;
        WinText.enabled = true;
        WinButton.gameObject.SetActive(true);
        Cursor.visible = true;
        WinSound.Play();
        GameSound.Stop();
    }

    public void NextLevel() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void RestartTheGame() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
