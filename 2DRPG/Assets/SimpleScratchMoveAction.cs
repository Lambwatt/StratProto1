using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SimpleScratchMoveAction : Action {

	Square square;
	Direction dir;
	
	public SimpleScratchMoveAction(Square s, Direction d){//Direction was converted in command

		square = s;
		dir = d;

	}

	private bool canMove(Board board){
		return board.moveAllowed(square, dir);
	}
	
	private bool willMove(Board board){
		return board.moveAllowed(square, dir);
	}

	public void checkIfExecutable(Board board){

		//Do nothing for now
	}
	
	public List<Response> execute(Board board){
		
		List<Response> res = new List<Response>();

		if(board.moveAllowed(square, dir)){

			board.move(square, dir);

		}

		return null;
	}
	
	public void checkForConsequences(Board board){

		// do nothing
	}
	
	public void applyConsequences(Board board){

		// do nothing
	}
}
