using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour {

	private GunsState GunsAvailable;
	private KeyValuePair<string, float[]> CurrentGun;

	private List<GunLoader> CurrentGunLoaders;

	private GunLoader CurrentGunLoader;

	public List<string> GunsDisponible;

	// Use this for initialization
	void Start () {
		GunsAvailable = new GunsState();
		// pour le test
		string gun_name = "USP";
		float[] gun_state = GunsAvailable.weapons[gun_name];
		CurrentGun = new KeyValuePair<string, float[]>(gun_name, gun_state);

		// nombre de chargeur = 3
		CurrentGunLoaders.Add(new GunLoader((int)CurrentGun.Value[1])); // 1er chargeur
		CurrentGunLoaders.Add(new GunLoader((int)CurrentGun.Value[1])); // 2eme chargeur
		CurrentGunLoaders.Add(new GunLoader((int)CurrentGun.Value[1])); // 3eme chargeur

		// chargeur actuel dans l'arme
		CurrentGunLoader = new GunLoader((int)CurrentGun.Value[1]);

		//Debug.Log(new List<string>(GunsAvailable.weapons.Keys));

		GunsDisponible = new List<string>(GunsAvailable.weapons.Keys);
	}

	private int trouver_chargeur_plein()
	{
		// 	pour chaque Chargeur [] :
		// 		si un chargeur.nombre de balle == Arme.nombre maximum de ball possible pour un chargeur:
		for(int i = 0; i < CurrentGunLoaders.Count; ++i) {
			if (CurrentGunLoaders[i].nombre_balle == (int)CurrentGun.Value[1])
				return i;
		}
		return -1;
	}

	private int get_total_balle_chargeurs()
	{
		int total = 0;
		foreach (GunLoader gl in CurrentGunLoaders) {
			total += gl.nombre_balle;
		}
		return total;
	}

	// permet de combiné les chargeur a moitié vide en un chargeur complet
	// traite le cas ou total_balle '>=' maximum et le cas '<'
	private void ajuster_chargeurs_disponible()
	{
		int maximum_balle = (int)CurrentGun.Value[1];
		int total_balle = get_total_balle_chargeurs();

		int nombre_chargeur_possible = total_balle / maximum_balle;
		int nombre_balle_restante = total_balle % maximum_balle;

		CurrentGunLoaders.Clear();

		for(int i = 0; i < nombre_chargeur_possible; ++i)
			CurrentGunLoaders.Add(new GunLoader(maximum_balle));
		
		if (nombre_balle_restante != 0) {
			CurrentGunLoaders.Add(new GunLoader(nombre_balle_restante));
		}
	}

	private bool recharger_balle()
	{
		ajuster_chargeurs_disponible();
		if (CurrentGunLoaders.Count != 0) {
			int chargeur_trouver = trouver_chargeur_plein();
			int maximum_balle = (int)CurrentGun.Value[1];

			if (chargeur_trouver == -1) {
				int temp_nombre_balle = CurrentGunLoader.nombre_balle + CurrentGunLoaders[0].nombre_balle;
				if (temp_nombre_balle >= maximum_balle) { 
					CurrentGunLoaders[0].nombre_balle = temp_nombre_balle - maximum_balle;
					CurrentGunLoader.nombre_balle = maximum_balle;
				}
				else {
					CurrentGunLoader.nombre_balle = temp_nombre_balle;
					CurrentGunLoaders.RemoveAt(0); // dernier chargeur restant
				}
			}
			else {
				if (CurrentGunLoader.nombre_balle == 0) {
					CurrentGunLoader.nombre_balle = CurrentGunLoaders[chargeur_trouver].nombre_balle;
					// supp le chargeur utilisé
					CurrentGunLoaders.RemoveAt(chargeur_trouver);
				}
				else {
					int temp_actuel = CurrentGunLoader.nombre_balle;
					CurrentGunLoader.nombre_balle = CurrentGunLoaders[chargeur_trouver].nombre_balle;
					CurrentGunLoaders[chargeur_trouver].nombre_balle = temp_actuel;
				}
			}
			return true;
		}
		return false;
	}

	public List<string> affichage_balles_HUB()
	{
		return new List<string>{ CurrentGunLoader.nombre_balle.ToString(), 
								 get_total_balle_chargeurs().ToString()
		};
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
