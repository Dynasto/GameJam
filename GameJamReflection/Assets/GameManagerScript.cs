using UnityEngine;
using System.Collections;

public class GameManagerScript : MonoBehaviour {
    public static GameManagerScript instance;
    public bool GameOver = false;
    void Start() {
        instance = this;
    }
}
