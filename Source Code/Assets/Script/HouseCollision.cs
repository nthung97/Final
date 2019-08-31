using UnityEngine;
using System.Collections;

public class HouseCollision : MonoBehaviour {
    
    void OnMouseOver()
    {
        IndicatorBehave.move(VecMathf.destination(transform.position));
        if (Input.GetMouseButtonDown(0)) GamePhase.current.click(transform.position, 1);
        if (Input.GetMouseButtonDown(1)) GamePhase.current.click(transform.position, 2);
    }
}
