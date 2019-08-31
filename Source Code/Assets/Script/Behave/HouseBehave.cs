using UnityEngine;
using System.Collections;

public class HouseBehave : MonoBehaviour {
    public int state = 0;

    public void updateFl(int St)
    {
        //0: None. 1 - 3. House Level. 4. Dorm
        if (St < 0 || St > 4) return;
        state = St;

        int i = 0;
        transform.GetChild(0).gameObject.SetActive(true);
        for (; i < St; i++)
        {
            transform.GetChild(i).gameObject.SetActive(true);
        }
        for (; i < 4; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
        Vector3 scale = transform.localScale;
        switch (St)
        {
            case 0:
                scale.y = 1;
                break;
            case 1:
                scale.y = 2.4f;
                break;
            case 2:
                scale.y = 4.4f;
                break;
            case 3:
                scale.y = 6;
                break;
            case 4:
                scale.y = 6.6f;
                break;
        }
        CreateField.Receive(gameObject, St);
    }

    public void addFl()
    {
        updateFl(state + 1);
    }

    public void decFl()
    {
        updateFl(state - 1);
    }
	// Use this for initialization
	void Start () {
    }
	
	// Update is called once per frame
	void Update () {
	}
}
