using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SimpleRealMoveCommand : Command {

	Direction 	dir;//Direction past to subsequent pieces
	Square		square;

	public SimpleRealMoveCommand(Square s, int d){ 
		dir = Direction.getDirection(d);
		square = s;
	}

	public List<Action> execute(){ //returns action seqence checking for everything.
		List<Action> result = new List<Action>();

		result.Add(new SimpleRealMoveAction(square, dir, false));
		result.Add(new SimpleRealMoveAction(new Square(square.x+dir.getX(), square.y+dir.getY()), dir, true));

		return result;

	}
}
