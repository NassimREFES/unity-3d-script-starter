using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
	- Manager le status du joueur
 */

public class PlayerManager : MonoBehaviour, IGameManager {
	public ManagerStatus status {get; private set;}

	private NetworkService _network;

	public int health {get; private set;}
	public int maxHealth {get; private set;}

	public int shoot;
	public int maxShoot;

	public int reloadShoot = 1;
	public float reloadShootRate = 2.0f;

	private float next_reloadShoot;

	public void Startup(NetworkService service)
	{
		_network = service;
		Debug.Log("Player manager starting...");

		UpdateData(100, 100, 100, 100);

		status = ManagerStatus.Started;
	}

	public void UpdateData(int health, int maxHealth, int shoot, int maxShoot)
	{
		this.health = health;
		this.maxHealth = maxHealth;
		this.shoot = shoot;
		this.maxShoot = maxShoot;
	}

	public void ChangeShoot(int value)
	{
		shoot += value;
		if (shoot > maxShoot) {
			shoot = maxShoot;
		}
		else if (shoot < 0) {
			shoot = 0;
		}

		if (shoot == 0) {
			//Messenger.Broadcast(GameEvent.LEVEL_FAILED);
		}

		Debug.Log("shoot : " + shoot + "/" + maxShoot);
		//Messenger.Broadcast(GameEvent.HEALTH_UPDATED);
		//Messenger<int, int>.Broadcast(GameEvent.PLAYER_SHOOT, shoot, maxShoot);
	}

	public void ChangeHealth(int value)
	{
		health += value;
		Debug.Log("health = " + health);
		if (health > maxHealth) {
			Debug.Log("***********");
			health = maxHealth;
		}
		else if (health < 0) {
			Debug.Log("++++++++++");
			health = 0;
		}

		if (health == 0) {
			Debug.Log("----------");
			//Messenger.Broadcast(GameEvent.LEVEL_FAILED);
		}

		Debug.Log("Health : " + health + "/" + maxHealth);
		//Messenger.Broadcast(GameEvent.HEALTH_UPDATED);
		Messenger<int, int>.Broadcast(GameEvent.PLAYER_HIT, health, maxHealth);
	}

	public void Respawn()
	{
		UpdateData(100, 100, 100, 100);
	}
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		// if (Time.time > next_reloadShoot) {
		// 	next_reloadShoot = Time.time + reloadShootRate;
		// 	ChangeShoot(reloadShoot);
		// }
	}
}
