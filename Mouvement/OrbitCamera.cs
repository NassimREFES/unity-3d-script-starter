using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitCamera : MonoBehaviour {

	[SerializeField] private Transform target; // player
	[SerializeField] private Transform targetTPS; // objet a regardé pour TPS view ( head )
	[SerializeField] private Transform targetFPS; // objet a regardé pour FPS view ( objet front of head )

	public float rotationSpeed = 4.5f;
	private float _rotY;
	private float _rotX;
	private Vector3 _offsetTPS;
	private Vector3 _offsetFPS;

	private int count_scrollupdown = 0;
	public int countZoomMin = -15;
    public int countZoomMax = 25;

	public float minMouseXAngleFPS = -25.0f;
	public float minMouseXAngleTPS = -60.0f;
	public float maxMouseXAngle = 65.0f;

	private bool uiInventory = false;

	// false : keys left right ne fait pas rotate pour la caméra
	// autour du player
	public bool ViewStyle;

	void Awake()
	{
		Messenger<bool>.AddListener(GameEvent.UI_INVENTORY, OnUiInventory);
	}

	void OnDestroy()
	{
		Messenger<bool>.RemoveListener(GameEvent.UI_INVENTORY, OnUiInventory);
	}

	private void OnUiInventory(bool res)
	{
		uiInventory = res;
	}

	// Use this for initialization
	void Start () {
		_rotY = transform.eulerAngles.y+10;
		_offsetTPS = targetTPS.position - transform.position;

		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
	}

	// private IEnumerator SphereIndicator(Vector3 pos) 
	// {
	// 	GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
	// 	sphere.transform.Translate(pos * moveSpeed * Time.deltaTime);

	// 	yield return new WaitForSeconds(1);

	// 	Destroy(sphere);

	// 	//ViewStyle = !ViewStyle;
	// }

	/// <summary>
	/// LateUpdate is called every frame, if the Behaviour is enabled.
	/// It is called after all Update functions have been called.
	/// </summary>
	void LateUpdate()
	{
		// rotate with key direction
		// float horInput = Input.GetAxis("Horizontal");
		// if (horInput != 0) {
		// 	_rotY += horInput * rotationSpeed;
		// }
		//else {
		if (!uiInventory) {
			_rotY += Input.GetAxis("Mouse X") * rotationSpeed;
			_rotX -= Input.GetAxis("Mouse Y") * rotationSpeed;
			_rotX = Mathf.Clamp(_rotX, minMouseXAngleTPS, maxMouseXAngle);

			Quaternion rotation = Quaternion.Euler(_rotX, _rotY, 0);

			transform.position = targetTPS.position - (rotation * _offsetTPS);
			transform.LookAt(targetTPS);
			rotation = Quaternion.Euler(0, _rotY, 0);
			if(ViewStyle) {
				if (Camera_Controller.camera_status == CameraStatus.camera_fps) 
					target.rotation = rotation;//Quaternion.Slerp(target.rotation, rotation, 15.0f*Time.deltaTime);
				else if (Camera_Controller.camera_status == CameraStatus.camera_tps)
					target.rotation = rotation; //target.rotation = Quaternion.Slerp(target.rotation, rotation, 10.0f*Time.deltaTime);
					
			}
		}

		// scroll mouse pour le zoom (utiliisation dans camera collision)
		// float val = Input.GetAxis ("Mouse ScrollWheel");
		// if ( val != 0.0f ) {
		// 	if (val < 0) {
		// 		if (countZoomMin <= count_scrollupdown) {
		// 			count_scrollupdown--;
		// 			_offsetTPS += Vector3.forward * Time.deltaTime * 10.0f;
		// 		}
		// 	}
		// 	else {
		// 		if (count_scrollupdown <= countZoomMax) {
		// 			count_scrollupdown++;
		// 				_offsetTPS += Vector3.back * Time.deltaTime * 10.0f;
		// 		}
		// 	}
		// }
	}


	// Update is called once per frame
	void Update () {
		
	}
}
