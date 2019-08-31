using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class MyButton : MonoBehaviour {
    public int type;
	// Update is called once per frame
	void OnMouseDown () {
        if (type == 0)
        {
            CreateField.setToNull();
            SceneManager.LoadScene("Game");
        }
        if (type == 1)
            SceneManager.LoadScene("Main");
        if (type == 2)
        {
            Debug.Log("Clicked");
            CreateField.Field.restart();
        }
            
    }
}
