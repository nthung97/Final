using UnityEngine;
using System.Collections;

public class CursorBehave : MovableBehave
{
    public int side;
    static Vector3 error = Vector3.up * -4;

    public override void getMove()
    {
        /*if (Input.GetAxis("Enter" + side) > 0)
        {
            GamePhase.current.click(this);
            return;
        }*/
        if (Input.GetAxis("Horizontal" + side) != 0)
            des += Vector3.right * Input.GetAxisRaw("Horizontal" + side);
        else
            if (Input.GetAxis("Vertical" + side) != 0)
            des += Vector3.forward * Input.GetAxisRaw("Vertical" + side);
        else return;
        des = VecMathf.destination(des);
        if (des.y < -2) return;
        GridMove.setMove(transform, start, des);
    }
}
