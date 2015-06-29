using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SimpleCommandFactory : CommandFactory {

	ManagerHub manager;

	public SimpleCommandFactory(){
		manager = GameObject.Find("manager").GetComponent<ManagerHub>();
	}

	public Command getCommand(string key, Square s, int dir, int mag){
//		if(ManagerHub.gameState=="planning"){//enumerate this state later
//			return new SimpleScratchMoveCommand(s, dir);
//		}else{
			return new SimpleRealMoveCommand(s, dir, mag);
		//}
	}
}
