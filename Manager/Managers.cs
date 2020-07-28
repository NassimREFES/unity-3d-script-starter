using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
	- gerer le comportement du jeu
 */

[RequireComponent(typeof(PlayerManager))]
[RequireComponent(typeof(InventoryManager))]

public class Managers : MonoBehaviour {

	public static PlayerManager Player {get; private set;}

	public static InventoryManager Inventory {get; private set;}

	private List<IGameManager> _startSequence;

	void Awake()
	{
		// -------------------- UI LISTENER -----------------------
		// --------- health
		Messenger.AddListener(GameEvent.HEALTH_USE, OnHealthUse);
		Messenger.AddListener(GameEvent.HEALTH_ENTER, OnHealthEnter);
		Messenger.AddListener(GameEvent.HEALTH_EXIT, OnHealthExit);
		// --------- energy
		Messenger.AddListener(GameEvent.ENERGY_USE, OnEnergyUse);
		Messenger.AddListener(GameEvent.ENERGY_ENTER, OnEnergyEnter);
		Messenger.AddListener(GameEvent.ENERGY_EXIT, OnEnergyExit);
		// --------- orel
		Messenger.AddListener(GameEvent.OREL_USE, OnOrelUse);
		Messenger.AddListener(GameEvent.OREL_ENTER, OnOrelEnter);
		Messenger.AddListener(GameEvent.OREL_EXIT, OnOrelExit);
		// --------- key
		Messenger.AddListener(GameEvent.KEY_USE, OnKeyUse);
		Messenger.AddListener(GameEvent.KEY_ENTER, OnKeyEnter);
		Messenger.AddListener(GameEvent.KEY_EXIT, OnKeyExit);

		// --------------------------------------------------------

		DontDestroyOnLoad(gameObject);

		Player = GetComponent<PlayerManager>();
		Inventory = GetComponent<InventoryManager>();

		_startSequence = new List<IGameManager>();
		_startSequence.Add(Player);
		_startSequence.Add(Inventory);

		StartCoroutine(StartupManagers());
	}

	void OnDestroy()
	{
		// -------------------- UI LISTENER -----------------------
		// --------- health
		Messenger.RemoveListener(GameEvent.HEALTH_USE, OnHealthUse);
		Messenger.RemoveListener(GameEvent.HEALTH_ENTER, OnHealthEnter);
		Messenger.RemoveListener(GameEvent.HEALTH_EXIT, OnHealthExit);
		// --------- energy
		Messenger.RemoveListener(GameEvent.ENERGY_USE, OnEnergyUse);
		Messenger.RemoveListener(GameEvent.ENERGY_ENTER, OnEnergyEnter);
		Messenger.RemoveListener(GameEvent.ENERGY_EXIT, OnEnergyExit);
		// --------- orel
		Messenger.RemoveListener(GameEvent.OREL_USE, OnOrelUse);
		Messenger.RemoveListener(GameEvent.OREL_ENTER, OnOrelEnter);
		Messenger.RemoveListener(GameEvent.OREL_EXIT, OnOrelExit);
		// --------- key
		Messenger.RemoveListener(GameEvent.KEY_USE, OnKeyUse);
		Messenger.RemoveListener(GameEvent.KEY_ENTER, OnKeyEnter);
		Messenger.RemoveListener(GameEvent.KEY_EXIT, OnKeyExit);
	}

	private IEnumerator StartupManagers()
	{
		NetworkService network = new NetworkService();

		foreach(IGameManager manager in _startSequence) {
			manager.Startup(network);
		}

		yield return null;

		int numModules = _startSequence.Count;
		Debug.Log("Modules = " + _startSequence.Count);
		int numReady = 0;

		while (numReady < numModules) {
			int lastReady = numReady;
			numReady = 0;

			foreach(IGameManager manager in _startSequence) {
				if (manager.status == ManagerStatus.Started) {
					numReady++;
				}
			}

			if (numReady > lastReady) {
				Debug.Log("Progress : " + numReady + "/" + numModules);
				//Messenger<int, int>.Broadcast(StartupEvent.MANAGERS_PROGRESS, numReady, numModules);
			}

			yield return null;
		}

		Debug.Log("All managers started up");
		//Messenger.Broadcast(StartupEvent.MANAGERS_STARTED);
	}

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	void OnHealthUse()
	{
		if (Managers.Player.health != Managers.Player.maxHealth 
			&& Managers.Inventory.GetItemCount("health") != 0) {
			Managers.Player.ChangeHealth(+8);
			Managers.Inventory.ConsumeItem("health");
		}
	}
	void OnHealthEnter()
	{

	}
	void OnHealthExit()
	{

	}
		// --------- energy
	void OnEnergyUse()
	{

	}
	void OnEnergyEnter()
	{

	}
	void OnEnergyExit()
	{

	}
		// --------- orel
	void OnOrelUse()
	{

	}
	void OnOrelEnter()
	{

	}
	void OnOrelExit()
	{

	}
		// --------- key
	void OnKeyUse() 
	{

	}
	void OnKeyEnter()
	{

	}
	void OnKeyExit()
	{

	}
}
