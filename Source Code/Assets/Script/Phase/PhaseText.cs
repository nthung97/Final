using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PhaseText : MonoBehaviour {

    static Text a;
    public void Start()
    {
        Text b = gameObject.GetComponent<Text>();
        if (b != null)
        {
            if (a != null)
                Destroy(a.gameObject);
            a = b;
        }
    }
    public static void UpText()
    {
        a.text = GamePhase.current.getString();
    }
}