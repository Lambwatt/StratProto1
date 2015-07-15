using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SimpleRealReadyCommand : Command {

	Direction 	dir;//Direction past to subsequent pieces
	Square		square;
	
	public SimpleRealReadyCommand(Square s, int d, int m){ 
		dir = Direction.getDirection(d);
		square = s;
		//		magnitude = m;
	}
	
	public List<Action> execute(){ //returns action seqence checking for everything.
		List<Action> result = new List<Action>();
		
		result.Add(new SimpleRealReadyAction(square));
		
		return result;
		
	}
}
