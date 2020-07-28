using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
	- etat du jeu
 */

public static class GameEvent {
	public const string HEALTH_UPDATED = "HEALTH_UPDATED";

	public const string LEVEL_COMPLETE = "LEVEL_COMPLETE";
	public const string LEVEL_FAILED = "LEVEL_FAILED";

	public const string GAME_COMPLETE = "GAME_COMPLETE";

	public const string PLAYER_HIT = "PLAYER_HIT";
	public const string PLAYER_SHOOT = "PLAYER_SHOOT";

	// ------------------- Inventaire ---------------------
	public const string UI_INVENTORY = "UI_INVENTORY";

	// ----------- health
	public const string HEALTH_USE = "HEALTH_USE";
	public const string HEALTH_ENTER = "HEALTH_ENTER";
	public const string HEALTH_EXIT = "HEALTH_EXIT";

	// ----------- energy
	public const string ENERGY_USE = "ENERGY_USE";
	public const string ENERGY_ENTER = "ENERGY_ENTER";
	public const string ENERGY_EXIT = "ENERGY_EXIT";
	// ----------- orel
	public const string OREL_USE = "OREL_USE";
	public const string OREL_ENTER = "OREL_ENTER";
	public const string OREL_EXIT = "OREL_EXIT";

	// ----------- key
	public const string KEY_USE = "KEY_USE";
	
	public const string KEY_ENTER = "KEY_ENTER";
	public const string KEY_EXIT = "KEY_EXIT";
}
