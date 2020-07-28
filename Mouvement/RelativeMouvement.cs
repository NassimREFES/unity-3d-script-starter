using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]

public class RelativeMouvement : MonoBehaviour {

	[SerializeField] private Transform target;
	[SerializeField] private Camera targetCameraTPS;
	[SerializeField] private Camera targetCameraFPS;

	private CharacterController _charController;

	public float rotationSpeed = 15.0f;

	public float moveSpeed = 10.0f;

	// for jump
	public float jumpSpeed = 15.0f;
	public float gravity = -9.8f;
	public float terminalVelocity = -5.0f;
	public float minFall = -10.0f;

	private float _vertSpeed;

	private ControllerColliderHit _contact;

	[SerializeField] private GameObject weaponPrefab;
	private GameObject _weapon;
	[SerializeField] private GameObject fireballPrefab;
	private GameObject _fireball;

	public float fireRate;
	private float nextFire;

	public int shootValue = 2;

	private bool uiInventory = false;

	private bool reactiveRafal = true;

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
		if (res)
			reactiveRafal = false;
	}

	// Use this for initialization
	void Start () {
		_charController = GetComponent<CharacterController>();
		_vertSpeed = minFall;

		// instantié l'arme a utilisé
		_weapon = Instantiate(weaponPrefab) as GameObject;
		// recuperer par nom
		// Debug.Log("name child = " + this.transform.GetChild(2).name);
		for (int i = 0; i < this.transform.childCount; ++i) {
			if (this.transform.GetChild(i).name == "weaponPosition") {
				_weapon.transform.parent = this.transform.GetChild(i);
				_weapon.transform.position = this.transform.GetChild(i).position;
				break;
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (Managers.Player.health > 0 && !uiInventory) {
			bool hitGround = false;
			RaycastHit hit;

			Vector3 movement = Vector3.zero;

			float horInput = Input.GetAxis("Horizontal");
			float vertInput = Input.GetAxis("Vertical");


			if (horInput != 0 || vertInput != 0) {
				movement.x = horInput * moveSpeed;
				movement.z = vertInput * moveSpeed;
				movement = Vector3.ClampMagnitude(movement, moveSpeed);

				Quaternion tmp = target.rotation;
				target.eulerAngles = new Vector3(0, target.eulerAngles.y, 0);
				movement = target.TransformDirection(movement);
				target.rotation = tmp;
				// if (Camera_Controller.camera_status == CameraStatus.camera_tps){
				// 	//transform.rotation = Quaternion.LookRotation(movement);
				// 	Quaternion direction = Quaternion.LookRotation(movement);
				// 	//transform.rotation = Quaternion.Lerp(transform.rotation, direction, rotationSpeed*Time.deltaTime);
				// 	transform.rotation = Quaternion.Slerp(transform.rotation, direction, rotationSpeed*Time.deltaTime);
				// }
			}

			if (_vertSpeed < 0 && Physics.Raycast(transform.position, Vector3.down, out hit)) {
				float check = (_charController.height + _charController.radius) / 1.9f;
				hitGround = hit.distance <= check;
			}

			if (hitGround) {
				if (Input.GetButtonDown("Jump")) {
					_vertSpeed = jumpSpeed;
				}
				else {
					_vertSpeed = minFall;
				}
			}
			else {
				_vertSpeed += gravity * 5 * Time.deltaTime;
				if (_vertSpeed < terminalVelocity) {
					_vertSpeed = terminalVelocity;
				}

				/*if(_contact != null) {
					//_animator.SetBool("Jumping", true);
				}*/

				if (_charController.isGrounded) {
					// verifier si _contact.normal position est derrier movement position
					// direction opposé entre les 2
					if (Vector3.Dot(movement, _contact.normal) < 0) {
						movement = _contact.normal * moveSpeed;
					}
					else {
						// verifier si _contact.normal position est devant movement position
						// alor on ajoute un step pour le faire tombé au lieu de resté debout dans le vide
						// meme direction
						movement += _contact.normal * moveSpeed;
					}
				}
			}
			movement.y = _vertSpeed;

			movement *= Time.deltaTime;

			_charController.Move(movement);

			if (Input.GetMouseButtonDown(0)) reactiveRafal = true;

			if (reactiveRafal && (Input.GetMouseButtonDown(0) || Input.GetButton("Fire1")) && Time.time > nextFire) {
					nextFire = Time.time + fireRate;

					Camera targetCamera = new Camera();
					if (Camera_Controller.camera_status == CameraStatus.camera_fps) 
						targetCamera = targetCameraFPS;
					else if (Camera_Controller.camera_status == CameraStatus.camera_tps)
						targetCamera = targetCameraTPS;

					Vector3 mousePosition = new Vector3(targetCamera.pixelWidth/2, 
														targetCamera.pixelHeight/2, 0);

					Ray ray1 = targetCamera.ScreenPointToRay(mousePosition);
					RaycastHit hit1;

					var childweapon = _weapon.transform.GetChild(0).transform;

					if (Physics.Raycast(ray1, out hit1)) {
						float dist_to_object = hit1.distance;
						float dist_between_camera_weapon = Vector3.Distance(new Vector3(childweapon.position.x, 
																target.position.y, 
																childweapon.position.z), 
																childweapon.position);
						
						mousePosition = targetCamera.ScreenToWorldPoint(mousePosition);
						// pythagore algo ray shoot
						mousePosition += Mathf.Sqrt(Mathf.Pow(dist_to_object, 2) + 
													Mathf.Pow(dist_between_camera_weapon, 2)) * targetCamera.transform.forward;

						// Debug.Log("dist = " + dist_to_object);
						// Debug.Log("dist2 = " + dist_between_camera_weapon);

						_fireball = Instantiate(fireballPrefab);
						_fireball.transform.position = childweapon.position;
						_fireball.transform.LookAt(mousePosition);
					}
			}
		}
		else {
			// si le player est mort ...
		}
	}

	/// <summary>
	/// OnControllerColliderHit is called when the controller hits a
	/// collider while performing a Move.
	/// </summary>
	/// <param name="hit">The ControllerColliderHit data associated with this collision.</param>
	void OnControllerColliderHit(ControllerColliderHit hit)
	{
		_contact = hit;
		//Debug.Log("collision normal = " + hit.normal);
	}
}
