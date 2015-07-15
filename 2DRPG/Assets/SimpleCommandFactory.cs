using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SimpleCommandFactory : CommandFactory {

	ManagerHub manager;

	public SimpleCommandFactory(){
		manager = GameObject.Find("manager").GetComponent<ManagerHub>();
	}

	public Command getCommand(string key, Square s, int dir, int mag){

		switch(key){
		case "move":
			return new SimpleRealMoveCommand(s, dir, mag);
		case "attack":
			return new SimpleRealAttackCommand(s, dir, mag);
		case "ready":
			return new SimpleRealReadyCommand(s, dir, mag);
		default:
			Debug.Log("recieved order of type: "+key);
			return new SimpleRealMoveCommand(s, 0, 1);
		}
	}
}
