using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlCamera : MonoBehaviour {

	private Camera mycamera;

	void OnGUI()
	{
			GUI.contentColor = Color.red;
			
			int size = 12;
			float posX = mycamera.pixelWidth/2 - 2;
			float posY = mycamera.pixelHeight/2 - 8;
			GUI.Label(new Rect(posX, posY, size, size), "*");
	}

	// Use this for initialization
	void Start () {
		mycamera = GetComponent<Camera>();
	}

	public void active_camera()
	{
		mycamera.enabled = true;
	}

	public void deactive_camera()
	{
		mycamera.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
