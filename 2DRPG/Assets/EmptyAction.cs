using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EmptyAction : Action {

	public EmptyAction(){}

	public List<Action> checkIfExecutable(Board board, TurnMetaData data){
		return null;
	}
	
	public int execute(Board board, TurnMetaData data){
		return 0; 
	}

	public void checkForConsequences(Board board, TurnMetaData data){}
	
	public int applyConsequences(Board board, TurnMetaData data){
		return 0;
	}
}
