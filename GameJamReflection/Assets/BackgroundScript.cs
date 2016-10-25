using UnityEngine;
using System.Collections;

public class BackgroundScript : MonoBehaviour {
    private SpriteRenderer sr;
    void Start() {
        sr = GetComponent<SpriteRenderer>();
    }
	void Update () {
	//    ResizeSpriteToScreen();
	}
    void ResizeSpriteToScreen() {
        if (sr == null) return;

        transform.localScale = new Vector3(1, 1, 1);

        var width = sr.sprite.bounds.size.x;
        var height = sr.sprite.bounds.size.y;

        var worldScreenHeight = Camera.main.orthographicSize * 2.0;
        var worldScreenWidth = worldScreenHeight / Screen.height * Screen.width;

        //transform.localScale.x = worldScreenWidth / width;
        //transform.localScale.y = worldScreenHeight / height;
        transform.localScale = new Vector3((float)(worldScreenWidth / width), (float)(worldScreenHeight / height), 1);
    }
}
