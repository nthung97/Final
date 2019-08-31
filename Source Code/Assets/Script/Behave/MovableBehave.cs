using UnityEngine;
using System.Collections;

public abstract class MovableBehave : MonoBehaviour {
    //protected float t;
    //protected bool isMoving;
    protected Vector3 des, start;
    
    public abstract void getMove();
    public void Update()
    {
        if (!GridMove.isMoving)
        {
            start = transform.position;
            des = transform.position;
            getMove();
        }
    }
}
