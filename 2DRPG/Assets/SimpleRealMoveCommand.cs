using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SimpleRealMoveCommand : Command {

	Direction 	dir;//Direction past to subsequent pieces
	Square		square;
	int		 	magnitude;

	public SimpleRealMoveCommand(Square s, int d, int m){ 
		dir = Direction.getDirection(d);
		square = s;
		magnitude = m;
	}

	public List<Action> execute(){ //returns action seqence checking for everything.
		List<Action> result = new List<Action>();

		Square currentSquare = new Square(square.x, square.y);
		for(int i = 0; i<magnitude; i++){
			result.Add(new SimpleRealMoveAction(currentSquare, dir, i==magnitude-1));
			currentSquare=new Square(currentSquare.x+dir.getX(), currentSquare.y+dir.getY());
			//result.Add(new SimpleRealMoveAction(new Square(square.x+dir.getX(), square.y+dir.getY()), dir, true));
		}

		return result;

	}
}
