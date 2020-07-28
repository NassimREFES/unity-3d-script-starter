using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GunsState 
{
	//[SerializeField] private string[] Conditions = new string[] {"Neuf", "Utiliser", "Casser"};
	public Dictionary<string, float[]> weapons = new Dictionary<string, float[]>();

	public GunsState()
	{
		// exemple d'arme
		string[] weapons_name = {"USP", "GLOCK"};
		// indice
		// 0 : nombre de balle minimum
		// 1 : nombre de balle maximum
		// 2 : degat
		// 3 : rate des balles
		// 4 : vitesse de la balle
		float[] usp_details = {0, 12, 7, 1.5f, 1.0f};
		float[] glock_details = {0, 20, 6, 1.5f, 1.0f};

		weapons.Add(weapons_name[0], usp_details);
		weapons.Add(weapons_name[1], glock_details);
	}

	public GunsState(Dictionary<string, float[]> all_weapons) 
	{
		weapons = all_weapons;
	}
}
