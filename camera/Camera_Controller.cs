using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Controller : MonoBehaviour {

	[SerializeField] private ControlCamera cameraFps;

	[SerializeField] private ControlCamera cameraTps;

	[SerializeField] private ControlCamera cameraMap;

	[SerializeField] private GameObject identifyPlayer; // pour la vue MAP

	public static CameraStatus camera_status {get; private set;}

	private bool for_start = true;
	// Use this for initialization

	void Awake()
	{
		camera_status = CameraStatus.camera_tps;
	}
	void Start () 
	{
		for_start = true;
	}

	void OnGUI()
	{
		if (for_start) {
			etat_camera(false, true, false);
			for_start = false;
		}
	}
	
	// Update is called once per frame
	void Update () {
		// if (Input.GetKeyDown(KeyCode.Tab)) {
		// 	camera_status = CameraStatus.camera_map;
		// 	etat_camera(false, false, true);
		// 	identifyPlayer.SetActive(true);
		// }
		// else if (Input.GetKeyUp(KeyCode.Tab)) {
		// 	camera_status = CameraStatus.camera_tps;
		// 	etat_camera(false, true, false);
		// 	identifyPlayer.SetActive(false);
		// }
		// else 
		if (Input.GetMouseButtonDown(1)) {
			camera_status = CameraStatus.camera_fps;
			etat_camera(true, false, false);
		}
		else if (Input.GetMouseButtonUp(1)) {
			camera_status = CameraStatus.camera_tps;
			etat_camera(false, true, false);
		}
	}

	private void etat_camera(bool for_fps, bool for_tps, bool for_map)
	{
		if(for_fps) cameraFps.active_camera();
		else cameraFps.deactive_camera();

		if(for_tps) cameraTps.active_camera();
		else cameraTps.deactive_camera();

		// if(for_map) cameraMap.active_camera();
		// else cameraMap.deactive_camera();
	}
}
