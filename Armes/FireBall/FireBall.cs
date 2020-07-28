using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour {
	public float speed = 10.0f;
	public Vector3 directionVector;

	public int damage = 10;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		transform.Translate(Vector3.forward * speed * Time.deltaTime);
	}

	private IEnumerator ShootTimer() 
	{
		yield return new WaitForSeconds(20);
		Destroy(this.gameObject);
	}

	void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.tag == "Enemy") {
			EnemyState target = other.GetComponent<EnemyState>();
			if (target != null) {
				Debug.Log("Target hit");
				target.ReactToHit(damage);
				//StartCoroutine(ShootTimer());
				Destroy(this.gameObject);
			}
		}
		else if (other.gameObject.tag == "Ground") {
			Destroy(this.gameObject);
		}
		else {
			StartCoroutine(ShootTimer());
		}
	}

}
