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
    public Button GameOverButton;
    [HideInInspector]
    public bool GameOver = false;
    
    void Start() {
        instance = this;
        GameOverText.enabled = false;
        GameOverButton.gameObject.SetActive(false);
        //Cursor.visible = false;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            GameOverFunction();
        }
    }

    public void GameOverFunction()
    {
        GameOver = true;
        foreach (var player in Players)
        {
            player.GetComponent<Animator>().SetBool("IsGameOver", true);
        }
        GameOverText.enabled = true;
        GameOverButton.gameObject.SetActive(true);
        Cursor.visible = true;
    }

    public void RestartTheGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
