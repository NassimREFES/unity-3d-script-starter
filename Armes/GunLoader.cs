using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunLoader
{
	public int nombre_balle { get; set; }

	public GunLoader()
	{
		nombre_balle = 0;
	}

	public GunLoader(int nombre_de_balle)
	{
		nombre_balle = nombre_de_balle;
	}
	//public int type_chargeur { get; set; }
}
