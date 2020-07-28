using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
	avoir un niveau d'abstraction pour les manager
	pour les gere ensembre
 */

public interface IGameManager {
	ManagerStatus status {get;}

	void Startup(NetworkService service);
}
