using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SimpleRealMoveAction : Action{

	Square square;
	Direction dir;
//	bool isFirst;
//	bool isLast;

	public SimpleRealMoveAction(Square s, Direction d)//Direction was converted in command
	{
		square = s;
		dir = d;
//		isFirst = f;
//		isLast = l;
	}

	private bool canMove(Board board){
		return board.moveAllowed(square, dir);
	}

	private bool willMove(Board board){
		return board.moveAllowed(square, dir);
	}

	public void checkIfExecutable(Board board){

	}

	public List<Response> execute(Board board){
		
		List<Response> res = new List<Response>();
		
		if(board.moveAllowed(square, dir)){
			
			board.move(square, dir);
			
		}

		return null;
	}

	public void checkForConsequences(Board board){
		
	}
	
	public void applyConsequences(Board board){
		
	}



}
