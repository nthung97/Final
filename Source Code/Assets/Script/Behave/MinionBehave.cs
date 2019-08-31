using UnityEngine;
using System.Collections;
using System;

public class MinionBehave : MovableBehave
{
    public int side; 
    static Vector3 error = Vector3.up * -4;


    public static Vector3 destination(Vector3 end)
    {
        if (VecMathf.outSide(end)) return error;
        if (Mathf.Abs(end.x) > 2) return error;
        if (Mathf.Abs(end.z) > 2) return error;
        Vector3 res = end;
        res.y = CreateField.Field.fl[(int)end.x + 2, (int)end.z + 2] * 0.5f;
        if (res.y == 2) return error;
        return res;
    }

    public void created()
    {
        Vector3 a = transform.position;
        a.y = 5;
        transform.position = a;
        GridMove.setMove(transform, a, destination(a));
    }

    public void move(Vector3 des)
    {
        //des = destination(des);
        GridMove.setMove(transform, transform.position, des);
    }

    public override void getMove()
    {
        /*if (Input.GetAxis("Horizontal") != 0)
            des += Vector3.right * Input.GetAxisRaw("Horizontal");
        else
            if (Input.GetAxis("Vertical") != 0)
            des += Vector3.forward * Input.GetAxisRaw("Vertical");
        else return;
        des = destination(des, start);
        if (des.y < -2) return;
        GridMove.setMove(transform, start, des);*/
    }

    public void removed()
    {
        Debug.Log(side);
        CreateField.Field.minion[side - 1].Remove(this);
        Destroy(gameObject);
    }
}
