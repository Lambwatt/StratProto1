using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SimpleScratchMoveCommand : Command {
	
	Direction 	dir;//Direction past to subsequent pieces
	Square		square;
	
	public SimpleScratchMoveCommand(Square s, int d){ 
		dir = Direction.getDirection(d);
		square = s;
	}
	
	public List<Action> execute(){ //returns action seqence checking for everything
		List<Action> result = new List<Action>();
		result.Add(new SimpleRealMoveAction(square, dir));
		return result;
	}
	
}
