using UnityEngine;
using System.Collections;

public class IndicatorBehave : MonoBehaviour {
    public static GameObject indi;
    public static Transform minion;
    public Material mat;
    static Material a;
    static Color red, yellow, green;
	void Start () {
        if (indi != null)
            Destroy(indi);
        red =  new Color(161.0f / 255, 68.0f / 255, 41.0f / 255);
        yellow = new Color(145.0f / 255, 103.0f / 255, 25.0f / 255);
        green = new Color(23.0f / 255, 68.0f / 255, 20.0f / 255);
        minion = null;
        indi = gameObject;
        a = mat;
        updateColor(0);
	}

    public static void updateColor(int side)
    {
        switch (side)
        {
            case 2:
                a.color = red;
                break;
            case 1:
                a.color = yellow;
                break;
            case 0:
                a.color = green;
                break;
        }
    }
    public static void move(Vector3 location)
    {
        IndicatorBehave.indi.transform.position = location;
        location.y = 4;
        if (minion != null && !GridMove.isMoving)
            minion.position = location + Vector3.right * 0.01f; //So that there is no miss GetMinion
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}

