using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBarController : MonoBehaviour {

	[SerializeField]
	private Image health;

	[SerializeField]
	private Image mana;

	[SerializeField]
	private Text healthText;

	void Awake()
	{
		Messenger<int, int>.AddListener(GameEvent.PLAYER_HIT, OnPlayerHit);
		Messenger<int, int>.AddListener(GameEvent.PLAYER_SHOOT, OnPlayerShoot);
	}

	void OnDestroy()
	{
		Messenger<int, int>.RemoveListener(GameEvent.PLAYER_HIT, OnPlayerHit);
		Messenger<int, int>.RemoveListener(GameEvent.PLAYER_SHOOT, OnPlayerShoot);
	}

	private void OnPlayerHit(int value, int maxvalue)
	{
		health.fillAmount = (float)value/maxvalue;
		healthText.text = value + " / " + maxvalue;
	}

	private void OnPlayerShoot(int value, int maxvalue)
	{
		mana.fillAmount = (float)value/maxvalue;
	}



	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
