using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DescriptionText : MonoBehaviour {

    static Text a;
    public void Start()
    {
        Text b = gameObject.GetComponent<Text>();
        if (b != null)
        {

			Debug.Log ("Destroy");
            if (a != null)
                Destroy(a.gameObject);
            a = b;
        }
    }
    public static void UpText()
    {
        a.text = "GamePhase: " + GamePhase.current.getDString();
    }
}
