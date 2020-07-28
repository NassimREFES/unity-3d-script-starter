using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallEnemy : MonoBehaviour {
	public float speed = 10.0f;
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
		Debug.Log("colision wiith : " + other.gameObject.tag);
		if (other.gameObject.tag == "Player") {
			Debug.Log("Collision wiith player !!!");
			Managers.Player.ChangeHealth(-damage);
			Destroy(this.gameObject);
		}
		else if (other.gameObject.tag == "Ground") {
			Debug.Log("Collision wiith ground !!!");
			Destroy(this.gameObject);
		}
		else {
			Debug.Log("Collision wiith other !!!");
			StartCoroutine(ShootTimer());
		}
	}
}
