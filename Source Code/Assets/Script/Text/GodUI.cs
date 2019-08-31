using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GodUI : MonoBehaviour
{
	public Sprite isActive, nonActive;
	public int god;
	public static GodUI GodUIOne, GodUITwo;
	int status = 0;
	List<GameObject> godChildren;
    // Start is called before the first frame update
    void Start()
	{
		this.godChildren = new List<GameObject> ();
		foreach (Transform child in transform)
		{
			this.godChildren.Add (child.gameObject);
		}
		Debug.Log ("God" + god);
		if (god == 1)
			GodUIOne = this;
		if (god == 2)
			GodUITwo = this;
    }

	public void updateStatus (bool active, int status)
	{
		if (status >= godChildren.Count)
			status = 0;
		godChildren [this.status].SetActive (false);
		if (active) {
			GetComponent<Image> ().sprite = isActive;
			this.status = status;
			Debug.Log ("Status" + status);
			godChildren [this.status].SetActive (true);
		}
		else GetComponent<Image> ().sprite = nonActive;
	}
}
