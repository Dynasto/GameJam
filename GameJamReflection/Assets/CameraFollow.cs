using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour
{
    public Transform player1, player2;
    public float MarginX, MarginY;
    public float minSizeY = 5f;
    private Camera camera;

    public void Awake()
    {
        camera = GetComponent<Camera>();
    }

    void SetCameraPos()
    {
        Vector3 middle = (player1.position + player2.position) * 0.5f;

        camera.transform.position = new Vector3(
            middle.x,
            middle.y,
            camera.transform.position.z
        );
    }

    void SetCameraSize()
    {
        //horizontal size is based on actual screen ratio
        float minSizeX = minSizeY * Screen.width / Screen.height;

        //multiplying by 0.5, because the ortographicSize is actually half the height
        float width = Mathf.Abs(player1.position.x - player2.position.x - 4) * 0.5f;
        float height = Mathf.Abs(player1.position.y - player2.position.y + 4) * 0.5f;

        //computing the size
        float camSizeX = Mathf.Max(width, minSizeX);
        camera.orthographicSize = Mathf.Max(height, camSizeX * Screen.height / Screen.width, minSizeY);
    }

    void Update()
    {
        SetCameraPos();
        SetCameraSize();
    }
}