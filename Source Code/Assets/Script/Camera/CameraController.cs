using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {
    public bool gameOver;
    public static CameraController main;
    Camera cam;
    bool clicked;
    float size;

	// Use this for initialization
	void Start () {
        gameOver = false;
        if (main != null)
            Destroy(main);
        main = this;
        clicked = false;
        cam = Camera.main;
        size = cam.orthographicSize;
    }

    void OnMouseDown() {
        if (MouseManager.clicked) return;
        MouseManager.clickDis();
        clicked = true;
    }

    void OnMouseDrag()
    {
        if (!clicked) return;
        Vector3 delta = MouseManager.delta;
        transform.Rotate(transform.up, delta.x);
        //transform.Rotate(cam.transform.right, delta.y);
    }
    
    public static void setMiddle(Vector3 midV)
    {
        midV.y = 0;
        main.gameOver = true;
        main.transform.position = midV;
        GamePhase.current = new EndGame();
    }

    void Update()
    {
        if (gameOver)
        {
            size -= 5f * Time.deltaTime;
            if (size < 1) size = 1;
            cam.orthographicSize = size;
            return;
        }
        if (!MouseManager.clicked)
            clicked = false;
        /*var d = Input.GetAxis("Mouse ScrollWheel");
        if (d > 0f)
        {
            // scroll up
            size -= 5f * Time.deltaTime;
            if (size < 1) size = 1;
            cam.orthographicSize = size;
        }
        else if (d < 0f)
        {
            // scroll down
            size += 5f * Time.deltaTime;
            if (size > 10) size = 10;
            cam.orthographicSize = size;
        }*/
    }
}
