using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour {
	public float speed = 4.0f;
	public float obstacleRange = 3.0f;
	public float firstDetectionRange = 25.0f; // 1er cercle de detection

	public float secondDetectionRange = 10.0f; // 2eme cercle de detection

	public float thirdDetectionRangeOffset = 2.0f; // 2eme cercle de detection

	[SerializeField] private GameObject fireballPrefab;
	private GameObject _fireball;

	private bool _alive;

	public float fireRate;
	private float nextFire;

	public float rotationSpeed = 15.0f;

	private Vector3 default_position;
	private Quaternion default_rotation;

	private bool xdetect_player;
	private bool default_dist;

	private bool only_one_rotate;
	private bool only_one_rotate_objet;

	public List<Transform> goToPoints;
	private int current_index;
	private int last_index;
	private bool direction_points;

	public ModelAI aiEnemy;
 	private RaycastHit hit;

	public int angleDeVue = 180;
	private int TempAngleDeVue;

	private Transform PlayerPosition;

	private NavMeshAgent InternalNavMeshAgent;

	// Use this for initialization
	void Start () {
		_alive = true;
		default_rotation = transform.rotation;
		default_position = transform.position;
		if (goToPoints.Count > 0) {
			current_index = 0;
			last_index = goToPoints.Count;
			direction_points = true;
		}

		InternalNavMeshAgent = this.GetComponent<NavMeshAgent>();
		InternalNavMeshAgent.speed = speed;

		xdetect_player = false;
		TempAngleDeVue = angleDeVue;
	}

	private IEnumerator EnemyInspectTerain()
	{
		float default_speed = this.speed;
		this.speed = 0.0f;
		yield return new WaitForSeconds(3);
		this.speed = default_speed;
	}

	// Update is called once per frame
	void Update () {
		if (_alive) {
			// l'enemy prend un angle de vue de 360 degree puisque le player
			// a été detecté
			if (xdetect_player) {
				angleDeVue = 360;
			}
			else {
				angleDeVue = TempAngleDeVue;
			}
			// angle de vue de l'enemi
			float angle_norm = 0.0f;
			if (angleDeVue <= 180) {
				angle_norm = (float)angleDeVue / 180.0f - 1.0f;
			}
			else if (180 < angleDeVue) {
				int tmp = angleDeVue - 180;
				angle_norm = -((float)tmp / 180.0f);
			}

			Collider[] hitColliders = Physics.OverlapSphere(transform.position, firstDetectionRange);
			foreach(Collider hitCollider in hitColliders) {
				Vector3 dir = (hitCollider.transform.position - transform.position).normalized;
				float res = Vector3.Dot(dir, transform.forward);

				if (hitCollider.gameObject.tag == "Player" && res > angle_norm) {
					float dist = Vector3.Distance(hitCollider.transform.position, transform.position);
					xdetect_player = true;
					PlayerPosition = hitCollider.transform;

					// si le player a été detecté, l'enemy suit le player
					// jusqu'a le player sort de la 1er zone de detection
					// de l'enemy

					if (dist <= firstDetectionRange) {
						// suivre le player
						InternalNavMeshAgent.destination = hitCollider.transform.position + 
														thirdDetectionRangeOffset*hitCollider.transform.forward;
						if (dist < secondDetectionRange) {
							if (Time.time > nextFire) {
								attack_shoot();
							}
						}
					}
					else if (dist > firstDetectionRange)
						xdetect_player = false;

					break;
				}
			}

			if (!xdetect_player) {
				// AI pour suivre des positions donnée
				if (aiEnemy == ModelAI.FollowPoints) {
					if (goToPoints.Count > 0) {
						InternalNavMeshAgent.destination = goToPoints[current_index].position;
						if (Vector3.Distance(goToPoints[current_index].position, transform.position) < 1.0f) {
							if (direction_points) current_index++;
							else current_index--;

							if (current_index >= goToPoints.Count) {
								direction_points = false;
								current_index--;
							}
							if (current_index < 0) {
								direction_points = true;
								current_index++;
							}
						}
					}
				}
				else if (aiEnemy == ModelAI.Stationary)
					InternalNavMeshAgent.destination = default_position;
			}
		}

	}

	private void attack_shoot()
	{
		nextFire = Time.time + fireRate;
		_fireball = Instantiate(fireballPrefab) as GameObject;
		_fireball.transform.position = transform.TransformPoint(Vector3.up / transform.localScale.y);
		_fireball.transform.position += _fireball.transform.forward * 1.5f;
		// tiré dans la direction du joueur
		Vector3 direction = PlayerPosition.position - transform.position;
		Quaternion qdirection = Quaternion.LookRotation(direction);
		_fireball.transform.rotation = qdirection;
	}

	public void SetAlive(bool alive)
	{
		_alive = alive;
	}

}
