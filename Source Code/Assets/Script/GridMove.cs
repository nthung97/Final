using UnityEngine;
using System.Collections;

public class GridMove : MonoBehaviour
{
    public static GridMove self;
    static private float moveSpeed = 3f;
    static private float gridSize = 1f;
    /*private enum Orientation
    {
        Horizontal,
        Vertical
    };
    //private Orientation gridOrientation = Orientation.Horizontal;
    //private bool allowDiagonals = false;
    //private bool correctDiagonalSpeed = true;
    private Vector2 input;*/
    public static Transform transf;
    static Vector3 start, end;
    public static bool isMoving = false;
    static int type;
    static float height, t;
    static private float factor = 0.5f;

    public static void setMove(Transform a, Vector3 startV, Vector3 endV)
    {
        transf = a;
        if (endV.y < -2) return; //Error checking
        if (startV == endV) return;
        start = startV;
        end = endV;
        type = -1;
        if (a.tag == "Cursor") type = 0;
        if (a.tag == "Minion") type = 1;
        if (type == -1) return;
        if (type == 1) ready2();
        isMoving = true;
    }

    public static void stop()
    {
        isMoving = false;
        t = 0;
    }

    public void Update()
    {
        if (isMoving)
            if (type == 0) moving1(); //Cursor
            else moving2();
    }

    public static void moving1() //Cursor
    {
        t += Time.deltaTime * (moveSpeed / gridSize) * 2 * factor;
        if (t > 1) t = 1;
        transf.position = Vector3.Lerp(start, end, t);
        if (t == 1) stop();
    }

    public static void ready2()
    {
        float hA = start.y;
        float hB = end.y;
        height = hA;
        if (hA != hB) height = ((hA > hB) ? hA : hB) + 1f;
    }

    public static void moving2() //Minion
    {
        t += Time.deltaTime * (moveSpeed / gridSize) * factor;
        if (t > 1) t = 1;
        Vector3 a = Vector3.Lerp(start, end, t);
        
        a.y = ((t - 0.5f) * (t - 0.5f)) * -4;
        if (t < 0.5f)
            a.y *= height - start.y;
        else
            a.y *= height - end.y;
        a.y += height;
        /*Apply to transform*/
        transf.position = a;
        if (t == 1) stop();
    }
}