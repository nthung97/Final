using UnityEngine;
using System.Collections;

public class MouseManager : MonoBehaviour {
    public static bool clicked;
    public static Vector3 direc, start, delta, C;
    static float time0;
	
    void Start()
    {
        C = Input.mousePosition;
    }

	// Update is called once per frame
	void Update () {
        delta = (Input.mousePosition - C);
        C = Input.mousePosition;
        if (Input.GetMouseButtonUp(0))
        {
            clicked = false;
            direc = Vector3.zero;
            
        }
        else
        {
            if (direc != Vector3.zero || !clicked) return;
            Vector3 a = Input.mousePosition - start;
            if (Time.time - time0 > 0.5f)
            {
                direc = a.normalized;
                Vector3 z = new Vector3();
                z.x = -direc.y;
                z.y = direc.x;
            }
        }
    }

    public static void clickDis()
    {
        clicked = true;
        start = Input.mousePosition;
        time0 = Time.time;
    }
}
