using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class EnemyState : MonoBehaviour {

	public int health;

	public Image healthBar;

	public void ReactToHit(int damage)
	{
		if (this.health > 0) {
			this.health -= damage;
			Debug.Log("enemy health = " + this.health);
			if (this.health <= 0) {
				healthBar.fillAmount = 0;
				StartCoroutine(Die());
			}
			else {
				healthBar.fillAmount = this.health / 100.0F;
			}
		}
	}

	private IEnumerator Die() 
	{
		//this.transform.Rotate(-80, 0, 0);
		//this.GetComponent<NavMeshAgent>().speed = 0;
		this.GetComponent<NavMeshAgent>().destination = this.transform.position;
		this.GetComponent<EnemyAI>().SetAlive(false);
		yield return new WaitForSeconds(5f);
		Destroy(this.gameObject);
	}

	// Use this for initialization
	void Start () {
		this.health = 100;
	}
	
	// Update is called once per frame
	void Update () {

	}
}

